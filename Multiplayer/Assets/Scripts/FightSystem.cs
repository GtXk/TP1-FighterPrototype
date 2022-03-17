using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum FightState { START, PLAYERONETURN, PLAYERTWOTURN, WON, LOST }

public class FightSystem : MonoBehaviourPunCallbacks, IPunObservable
{


    public Fighter player1Prefab;
    public Fighter player2Prefab;
    public GameObject player3Prefab;
    public GameObject player1Character;
    public GameObject player2Character;

    public Fighter Lol;

    public Transform player1FightPosition;
    public Transform player2FightPosition;

    //Fighter Player1Unit;
    //Fighter Player2Unit;


    public Text MenuText;

    public FightHUD player1HUD;
    public FightHUD player2HUD;

    public Button[] attackButtons;

    public Sprite playerImageG;

    public FightState state;

    public bool GameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        OnJoinedRoom(player1Character,player2Character);
        //GameObject Player3Ready = PhotonNetwork.Instantiate(player3Prefab, new Vector3(173f, 213f, 0f), Quaternion.identity);
        //Player1Unit = Player3Ready.GetComponent<Fighter>();

        //state = FightState.START;
        //StartCoroutine(SetupFight());
    }

    void Update()
    {
        int numberPlayers = PhotonNetwork.PlayerList.Length;
        if (numberPlayers == 2)
        {
            if (GameStart == false)
            {
                SetPlayers();
                state = FightState.START;
                StartCoroutine(SetupFight());
                GameStart = true;
            }
        }

        if (numberPlayers != 2 && GameStart == true)
        {
            state = FightState.WON;
            ENDFight();
        }
        
    }

    void SetPlayers()
    {
        Fighter[] Fighters = GetAllPlayers();
        //if(Fighters.Length == 2)
        //{
        player1Prefab = Fighters[0];
        player2Prefab = Fighters[1];

        player1Prefab.setPlayerNum(1);
        player2Prefab.setPlayerNum(2);
        //}
    }

    Fighter[] GetAllPlayers()
    {
        return GameObject.FindObjectsOfType<Fighter>();
    }

    void SpawnPlayers(GameObject myPlayer, Vector3 position)
    {
        PhotonNetwork.Instantiate(myPlayer.name, position, Quaternion.identity);
    }

    public void OnJoinedRoom(GameObject myPlayer, GameObject myPlayer2)
    {
        int totalPlayers = PhotonNetwork.PlayerList.Length;
        if (totalPlayers == 1)
        {
            SpawnPlayers(myPlayer,player1FightPosition.position); 
        }
        if(totalPlayers == 2)
        {
            SpawnPlayers(myPlayer2,player2FightPosition.position);
        }
    }

    IEnumerator SetupFight()
    {

        //GameObject Player1Ready = Instantiate(player1Prefab, player1FightPosition.position, Quaternion.identity);
        //Player1Unit = Player1Ready.GetComponent<Fighter>();

        //GameObject Player2Ready = Instantiate(player2Prefab, player2FightPosition.position, Quaternion.identity);
        //Player2Unit = Player2Ready.GetComponent<Fighter>();

       // GameObject Player1Ready = player1Prefab;
        //Player1Unit = Player1Ready.GetComponent<Fighter>();
        //Player1Unit.figherAttackSet.setAttack(new Attack("Slap1", "Fire", 10, 90, 0, 20), 0);
        //Debug.Log(Player1Unit.figherAttackSet.getAttack(0).getName());
        /*
        Player1Unit = new Fighter("John", "player", 0);
        Player1Unit.maxHealth = 100;
        Player1Unit.currentHealth = 100;
        */


        //GameObject Player2Ready = player2Prefab;
        //Player2Unit = Player2Ready.GetComponent<Fighter>();
        /*
        Player2Unit = new Fighter("John2", "player2", 0);
        Player2Unit.maxHealth = 100;
        Player2Unit.currentHealth = 100;
        */

        //MenuText.text = "The " + player2Prefab.FighterName + " Has arrived";

        player1HUD.setHUD(player1Prefab);
        player2HUD.setHUD(player2Prefab);

        yield return new WaitForSeconds(2f);

        //GameObject.Find(attackButtons[i].name).GetComponentInChildren<Text>().text = "la di da";
        //Debug.Log(Player1Unit.figherAttackSet.getAttack(0).getName());
        //Debug.Log(Player2Unit.currentHealth);
        //Debug.Log(Player2Unit.maxHealth);
        /*
        for(int i = 0; i < attackButtons.Length; i++)
        {
            GameObject.Find(attackButtons[i].name).GetComponentInChildren<Text>().text = player1Prefab.figherAttackSet.getAttack(i).getName(); //authoriy is own?
        }
        */


        state = FightState.PLAYERONETURN;
        PlayerOneTurn();
    }
    IEnumerator myAttack(Fighter attacker, Fighter defender, FightHUD defendersHud, int attackID)
    {
        bool isDead = defender.TakeDamage(attacker.figherAttackSet.getAttack(attackID));

        defendersHud.setHealth(defender.currentHealth);
        MenuText.text = attacker.FighterName + " attacks!";

        StartCoroutine(StateChanger(isDead, attacker.playerNum));
        yield return null;
    }

    [PunRPC]
    public IEnumerator StateChanger(bool isDead, int playerNum)
    {
        //yield return new WaitForSeconds(2f);
        yield return null;
        if (isDead)
        {
            state = FightState.WON;
            ENDFight();
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



    void ENDFight()
    {
        //maybe a coroutine
        if(state == FightState.WON)
        {
            MenuText.text = "You killed him! You Won... I guess?";
        }
        else if (state == FightState.LOST)
        {
            MenuText.text = "You dead my dude.. You lost";
        }
    }

    void PlayerOneTurn()
    {
        MenuText.text = "Fight! " + player1Prefab.FighterName;
    }

    void PlayerTwoTurn()
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

    void buttonAttack(int attackID)
    {
        if (state == FightState.PLAYERTWOTURN)
        {
            if(PhotonNetwork.PlayerList[1].IsLocal)
            {
                photonView.RPC("playerAttack", RpcTarget.All, 2, attackID);
            }

        }
        else if (state == FightState.PLAYERONETURN)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                photonView.RPC("playerAttack", RpcTarget.All, 1, attackID);
            }
        }
    }

    [PunRPC]
    public void playerAttack(int playeri, int attackID)
    {
        if(playeri == 1)
        {
            StartCoroutine(myAttack(player1Prefab, player2Prefab, player2HUD, attackID));
        }
        if(playeri == 2)
        {
            StartCoroutine(myAttack(player2Prefab, player1Prefab, player1HUD, attackID));
        }
        //StartCoroutine(myAttack());
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

    IEnumerator PlayerOneHeal()
    {
        player1Prefab.Heal(5);

        player1HUD.setHealth(player1Prefab.currentHealth);
        MenuText.text = "You feel renewed strength!";

        yield return null;

        
        state = FightState.PLAYERTWOTURN;
        PlayerTwoTurn();

    }

    IEnumerator PlayerTwoHeal()
    {
        player2Prefab.Heal(5);

        player2HUD.setHealth(player2Prefab.currentHealth);
        MenuText.text = "You feel renewed strength!";

        yield return null;

        state = FightState.PLAYERONETURN;
        PlayerOneTurn();

    }

    public void onHealButton()
    {
        if (state == FightState.PLAYERTWOTURN)
        {
            photonView.RPC("player2heal", RpcTarget.All);
            // StartCoroutine(PlayerTwoHeal());
        }
        else if (state == FightState.PLAYERONETURN)
        {
            photonView.RPC("player1heal", RpcTarget.All);
            // StartCoroutine(PlayerOneHeal());
        }

    }
    [PunRPC]
    public void player2heal()
    {
        StartCoroutine(PlayerTwoHeal());
    }
    [PunRPC]
    public void player1heal()
    {
        StartCoroutine(PlayerOneHeal());
    }
}
