using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public enum FightState { START, PLAYERONETURN, PLAYERTWOTURN, WON, LOST } //States based on the state of the game.

public class FightSystem : MonoBehaviourPunCallbacks, IPunObservable
{


    public Fighter player1Prefab;
    public Fighter player2Prefab;

    public GameObject player1Character;
    public GameObject player2Character;
    public AnimationDatabase Animationcontroller;
    public List<GameObject> playerCharacters;
    public int ChosenCharacter;
    public int ChosenCharacter2;
    public int rng;
    public int selectedItem;
    public int selectedItem2;
    public Transform player1FightPosition;
    public Transform player2FightPosition;
    public Text MenuText;

    public FightHUD player1HUD;
    public FightHUD player2HUD;

    public Button[] attackButtons;

    public Sprite playerImageG;

    public static FightState state;

    public bool GameStart = false;


    void Start()
    {
        //get values from character select screen
        ChosenCharacter = PlayerPrefs.GetInt("SelectedCharacter");
        ChosenCharacter2 = PlayerPrefs.GetInt("SelectedCharacter2");
        player1Character = playerCharacters[ChosenCharacter];
        player2Character = playerCharacters[ChosenCharacter2];
        OnJoinedRoom(player1Character,player2Character);



    }

    void Update()
    {
        int numberPlayers = PhotonNetwork.PlayerList.Length; //Gets the number of players connected
        if (numberPlayers == 2)
        {
            if (GameStart == false) //If 2 players are connected and the gamestart is false this will setup all the necessary things for the game and enable the gamestart to true.
            {
                SetPlayers();
                state = FightState.START;
                StartCoroutine(SetupFight()); //Enumerators are used to dictate the fight - useful for state of the game and waiting for photon to sent/recieve data.
                GameStart = true;
            }
        }

        if (numberPlayers != 2 && GameStart == true) //incase a player leaves then the other player will win the game.
        {
            state = FightState.WON;
            ENDFight();
        }
       

    }
    //assign player prefabs and assign any item boosts.
    void SetPlayers()
    {
        int fightBoost = PlayerPrefs.GetInt("SelectedItem");
        int fightBoost2 = PlayerPrefs.GetInt("SelectedItem2");
        Fighter[] Fighters = GetAllPlayers(); //All fighter objects tied to the fighter array
        
            player1Prefab = Fighters[1];
        
        if (fightBoost == 1)
        {

            player1Prefab.maxHealth += 10;
            player1Prefab.currentHealth += 10;
        }
        if (fightBoost == 2)
        {

            player1Prefab.accuracyDebuff = 1;
        }


        
        player2Prefab = Fighters[0];
        
        if (fightBoost2 == 1)
        {

            player2Prefab.maxHealth += 10;
            player2Prefab.currentHealth += 10;
        }
        if (fightBoost2 == 2)
        {
            player2Prefab.accuracyDebuff = 1;
        }
        if (fightBoost == 3)
        {

            player2Prefab.damageboost = 5;
        }
        if (fightBoost2 == 3)
        {
            player1Prefab.damageboost = 5;
        }
        


        player1Prefab.setPlayerNum(1); //Prefabs are set a player number to differntiate player 1 or player 2.
        player2Prefab.setPlayerNum(2);
        
    }

    Fighter[] GetAllPlayers() //Finds all the fighter objects
    {
        return GameObject.FindObjectsOfType<Fighter>();
    }

    void SpawnPlayers(GameObject myPlayer, Vector3 position, Quaternion rotation) //Instantiate the objects through the network using photon.
    {
        PhotonNetwork.Instantiate(myPlayer.name, position, rotation);
    }

    public void OnJoinedRoom(GameObject myPlayer, GameObject myPlayer2)
    {
        if (PhotonNetwork.IsMasterClient) //Only the host will be able to call this.
        {
            int totalPlayers = PhotonNetwork.PlayerList.Length;
            if (totalPlayers == 1)
            {
                SpawnPlayers(myPlayer, player1FightPosition.position, Quaternion.Euler(0f, 0f, 0f));
            }
            if (totalPlayers == 2) //Spawns two players.
            {
                SpawnPlayers(myPlayer, player1FightPosition.position, Quaternion.Euler(0f, 0f, 0f));
                SpawnPlayers(myPlayer2, player2FightPosition.position, Quaternion.Euler(0f, 180f, 0f));
            }
        } 
    }

    IEnumerator SetupFight()
    {
        player1HUD.setHUD(player1Prefab);
        player2HUD.setHUD(player2Prefab);
        yield return new WaitForSeconds(2f); //Dekay is used to make sure everything is setup - loading part of the game - photon sometimes might not have enough time to relay information.

        state = FightState.PLAYERONETURN;
        SetUpButtons(); //Setup all the attack buttons with their respective names only.
        PlayerOneTurn();

    }
    IEnumerator myAttack(Fighter attacker, Fighter defender, FightHUD defendersHud, FightHUD attackersHud, int playeri, int attackID) //its taking defenders and attackers hud - this could be improved. 
    {   // This myAttack function calculates all the things that occuring during when a player attacks another player (namely the attacker and the defender - who is taking the damage).
        if ((attacker.currentMana - attacker.figherAttackSet.getAttack(attackID).getMana()) < 0) //If state to determine if u have enough mana for a special attack
        {
            MenuText.text = "You dont have enough mana";
            disableAllButtons(); //Disables the attack buttons
            yield return new WaitForSeconds(2f); 
            enableAllButtons();
            StartCoroutine(StateChanger(attacker, defender.playerNum)); //uses playeri and playlist local photon function


        }
        else if ((attacker.currentMana - attacker.figherAttackSet.getAttack(attackID).getMana()) >= 0)
        {

            if (attackID == 3) 
            {
                Debug.Log("Heal path");
                attacker.Heal(attacker.figherAttackSet.getAttack(attackID));
                attacker.manaUsed(attacker.figherAttackSet.getAttack(attackID));
                string animToplay = attacker.figherAttackSet.getAttack(attackID).getName();
                Animationcontroller = FindObjectOfType<AnimationDatabase>();
                Animationcontroller.playAnim(animToplay);
                attackersHud.setMana(attacker.currentMana);
                attackersHud.setHealth(attacker.currentHealth);
                MenuText.text = attacker.FighterName + " Uses " + attacker.figherAttackSet.getAttack(attackID).getName();
                yield return new WaitForSeconds(2f);
                StartCoroutine(StateChanger(defender, attacker.playerNum));
                yield return new WaitForSeconds(2f);

            }
            else
            {
                defender.TakeDamage(attacker.figherAttackSet.getAttack(attackID));
                attacker.manaUsed(attacker.figherAttackSet.getAttack(attackID));
                if (PhotonNetwork.IsMasterClient)

                {
                    rng = Random.Range(0, 100);

                    photonView.RPC("shareRng", RpcTarget.All, rng);
                }
                string animToplay = attacker.figherAttackSet.getAttack(attackID).getName();
                Animationcontroller = FindObjectOfType<AnimationDatabase>();
                Animationcontroller.playAnim(animToplay);
                attackersHud.setMana(attacker.currentMana);
                defendersHud.setHealth(defender.currentHealth);
                MenuText.text = attacker.FighterName + " Uses " + attacker.figherAttackSet.getAttack(attackID).getName();
                yield return new WaitForSeconds(2f);
                StartCoroutine(StateChanger(defender, attacker.playerNum));
                yield return new WaitForSeconds(2f);
            }
        }
    }
    [PunRPC]
    public void shareRng(int calculatedRNG)
    {
        rng = calculatedRNG;
        Debug.LogError("Shared:" + rng);
        

    }



    [PunRPC]
    public IEnumerator StateChanger(Fighter defender, int playerNum)
    {
        yield return null;
        if (defender.currentHealth <= 0)
        {
            if(state == FightState.PLAYERONETURN)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    state = FightState.WON;
                    ENDFight();
                }
                else
                {
                    state = FightState.LOST;
                    ENDFight();
                }
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    state = FightState.LOST;
                    ENDFight();
                }
                else
                {
                    state = FightState.WON;
                    ENDFight();
                }
            }
            
        }
        else
        {
            if(playerNum == 1)
            {
                state = FightState.PLAYERTWOTURN;
                PlayerTwoTurn();
            }
            if(playerNum == 2)
            {
                state = FightState.PLAYERONETURN;
                PlayerOneTurn();
            }
        }
    }

    void ENDFight() //Decides on what happens to the winner and loser
    {
        if(state == FightState.WON)
        {
            MenuText.text = "You killed him! You Won... I guess?";
            SceneManager.LoadScene("WinEnd");
        }
        else if (state == FightState.LOST)
        {
            MenuText.text = "You dead my dude.. You lost";
            SceneManager.LoadScene("WinEnd");
        }
    }
    void SetUpButtons() //This functions only works for the local clients - Updates the button names with the attack names.
    {
        if (PhotonNetwork.PlayerList[0].IsLocal) 
        {
            for (int i = 0; i < attackButtons.Length; i++)
            {
                attackButtons[i].GetComponentInChildren<Text>().text = player1Prefab.figherAttackSet.getAttack(i).getName();
            }
        }
        else if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            for (int i = 0; i < attackButtons.Length; i++)
            {
                attackButtons[i].GetComponentInChildren<Text>().text = player2Prefab.figherAttackSet.getAttack(i).getName();
            }
        }
    }

    void PlayerOneTurn() //Indicates whether its playerone turn
    {
        MenuText.text = "Fight! " + player1Prefab.FighterName;
    }

    void PlayerTwoTurn() //Indicates whether its playertwo turn
    {
        MenuText.text = "Fight! " + player2Prefab.FighterName;
    }

    public void onLightAttackButton()
    {
        buttonAttack(0);
    }
    public void onMediumAttackButton()
    {
        buttonAttack(1);
    }
    public void onHeavyAttackButton()
    {
        buttonAttack(2);
    }
    public void onHealButton()
    {
        buttonAttack(3);
    }

    void buttonAttack(int attackID)
    {
        if (state == FightState.PLAYERTWOTURN)
        {
            if(PhotonNetwork.PlayerList[1].IsLocal)
            {
                photonView.RPC("playerAttackPhoton", RpcTarget.All, 2, attackID);
            }
        }
        else if (state == FightState.PLAYERONETURN)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                photonView.RPC("playerAttackPhoton", RpcTarget.All, 1, attackID);
            }
        }
    }
   

    [PunRPC]
    public void playerAttackPhoton(int playeri, int attackID)
    {
        if(playeri == 1)
        {
            StartCoroutine(myAttack(player1Prefab, player2Prefab, player2HUD, player1HUD, playeri, attackID));
        }
        if(playeri == 2)
        {
            StartCoroutine(myAttack(player2Prefab, player1Prefab, player1HUD, player2HUD, playeri, attackID));
        }

    }

    public void disableAllButtons()
    {
        foreach(Button button in attackButtons)
        {
            button.interactable = false;
        }
    }

    public void enableAllButtons()
    {
        foreach (Button button in attackButtons)
        {
            button.interactable = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else
        {

        }

    }

  
}
