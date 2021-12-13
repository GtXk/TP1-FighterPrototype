using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum FightState { START, PLAYERONETURN, PLAYERTWOTURN, WON, LOST }

public class FightSystem : MonoBehaviourPunCallbacks, IPunObservable
{


    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform player1FightPosition;
    public Transform player2FightPosition;

    Fighter Player1Unit;
    Fighter Player2Unit;
    public float rng;

    public Text MenuText;

    public FightHUD player1HUD;
    public FightHUD player2HUD;

    public FightState state;

    // Start is called before the first frame update
    void Start()
    {
 
        state = FightState.START;
        StartCoroutine(SetupFight());
    }

    IEnumerator SetupFight()
    {
        GameObject Player1Ready = Instantiate(player1Prefab, player1FightPosition);
        Player1Unit = Player1Ready.GetComponent<Fighter>();

        GameObject Player2Ready = Instantiate(player2Prefab, player2FightPosition);
        Player2Unit = Player2Ready.GetComponent<Fighter>();

        MenuText.text = "The " + Player2Unit.FighterName + " Has arrived";

        player1HUD.setHUD(Player1Unit);
        player2HUD.setHUD(Player2Unit);

        yield return new WaitForSeconds(2f);

        state = FightState.PLAYERONETURN;
        PlayerOneTurn();
    }

    IEnumerator PlayerOneLightAttack()
    {
  
        bool isDead = Player2Unit.TakeDamage(5);

        player2HUD.setHealth(Player2Unit.currentHealth);
        MenuText.text = Player1Unit.FighterName + " attacks!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = FightState.WON;
            ENDFight();
        }
        else
        {
            state = FightState.PLAYERTWOTURN;
            PlayerTwoTurn();
        }
    }

    IEnumerator PlayerTwoLightAttack()
    {
        bool isDead = Player1Unit.TakeDamage(5);

        player1HUD.setHealth(Player1Unit.currentHealth);
        MenuText.text = Player2Unit.FighterName + " attacks!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = FightState.WON;
            ENDFight();
        }
        else
        {
            state = FightState.PLAYERONETURN;
            PlayerOneTurn();
        }
    }
    IEnumerator PlayerOneMediumAttack()
    {
        rng = Random.Range(1.0f, 100.0f);
        if (rng <= 35)
        {
            bool isDead = Player2Unit.TakeDamage(10);

            player2HUD.setHealth(Player2Unit.currentHealth);
            MenuText.text = Player1Unit.FighterName + " attacks!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = FightState.WON;
                ENDFight();
            }
            else
            {
                state = FightState.PLAYERTWOTURN;
                PlayerTwoTurn();
            }
        }
        else
        {
            MenuText.text = Player1Unit.FighterName + " missed!";
            yield return new WaitForSeconds(2f);
            state = FightState.PLAYERTWOTURN;
            PlayerTwoTurn();
        }
    }
    IEnumerator PlayerTwoMediumAttack()
    {
        rng = Random.Range(1.0f, 100.0f);
        if (rng <= 35)
        {

            bool isDead = Player1Unit.TakeDamage(10);

            player1HUD.setHealth(Player1Unit.currentHealth);
            MenuText.text = Player2Unit.FighterName + " attacks!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = FightState.WON;
                ENDFight();
            }
            else
            {
                state = FightState.PLAYERONETURN;
                PlayerOneTurn();
            }
        }
        else
        {
            MenuText.text = Player2Unit.FighterName + " missed!";
            yield return new WaitForSeconds(2f);
            state = FightState.PLAYERONETURN;
            PlayerOneTurn();
        }
    }
    IEnumerator PlayerOneHeavyAttack()
    {

        rng = Random.Range(1.0f, 100.0f);
        if (rng <= 20)
        {
            bool isDead = Player2Unit.TakeDamage(15);

            player2HUD.setHealth(Player2Unit.currentHealth);
            MenuText.text = Player1Unit.FighterName + " attacks!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = FightState.WON;
                ENDFight();
            }
            else
            {
                state = FightState.PLAYERTWOTURN;
                PlayerTwoTurn();
            }
        }
        else
        {
            MenuText.text = Player1Unit.FighterName + " missed!";
            yield return new WaitForSeconds(2f);
            state = FightState.PLAYERTWOTURN;
            PlayerTwoTurn();
        }
    }
    IEnumerator PlayerTwoHeavyAttack()
    {
        rng = Random.Range(1.0f, 100.0f);
        if (rng <= 20)
        {

            bool isDead = Player1Unit.TakeDamage(15);

            player1HUD.setHealth(Player1Unit.currentHealth);
            MenuText.text = Player2Unit.FighterName + " attacks!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = FightState.WON;
                ENDFight();
            }
            else
            {
                state = FightState.PLAYERONETURN;
                PlayerOneTurn();
            }
        }
        else
        {
            MenuText.text = Player2Unit.FighterName + " missed!";
            yield return new WaitForSeconds(2f);
            state = FightState.PLAYERONETURN;
            PlayerOneTurn();
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
        MenuText.text = "Fight! " + Player1Unit.FighterName;
    }

    void PlayerTwoTurn()
    {
        MenuText.text = "Fight! " + Player2Unit.FighterName;
    }

    public void onLightAttackButton()
    {
        
        if (state != FightState.PLAYERONETURN)
        {
            photonView.RPC("player2lightattack", RpcTarget.All);
           
        }
        else
        {
            photonView.RPC("player1lightattack", RpcTarget.All);
          
        }
    }
    public void onMediumAttackButton()
    {

        if (state != FightState.PLAYERONETURN)
        {
            photonView.RPC("player2mediumattack", RpcTarget.All);
           
        }
        else
        {
            photonView.RPC("player1mediumattack", RpcTarget.All);
         
        }
    }
    public void onHeavyAttackButton()
    {

        if (state != FightState.PLAYERONETURN)
        {
            photonView.RPC("player2heavyattack", RpcTarget.All);
       
        }
        else
        {
            photonView.RPC("player1heavyattack", RpcTarget.All);
            
        }
    }
    [PunRPC]
    public void player2lightattack()
    {
        StartCoroutine(PlayerTwoLightAttack());
    }
    [PunRPC]
    public void player2mediumattack()
    {
        StartCoroutine(PlayerTwoMediumAttack());
    }
    [PunRPC]
    public void player2heavyattack()
    {
        StartCoroutine(PlayerTwoHeavyAttack());
    }
    [PunRPC]
    public void player1lightattack()
    {
        StartCoroutine(PlayerOneLightAttack());
    }
    [PunRPC]
    public void player1mediumattack()
    {
        StartCoroutine(PlayerOneMediumAttack());
    }
    [PunRPC]
    public void player1heavyattack()
    {
        StartCoroutine(PlayerOneHeavyAttack());
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
        Player1Unit.Heal(5);

        player1HUD.setHealth(Player1Unit.currentHealth);
        MenuText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        
        state = FightState.PLAYERTWOTURN;
        PlayerTwoTurn();

    }

    IEnumerator PlayerTwoHeal()
    {
        Player2Unit.Heal(5);

        player2HUD.setHealth(Player2Unit.currentHealth);
        MenuText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = FightState.PLAYERONETURN;
        PlayerOneTurn();

    }

    public void onHealButton()
    {
        if (state != FightState.PLAYERONETURN)
        {
            photonView.RPC("player2heal", RpcTarget.All);
            // StartCoroutine(PlayerTwoHeal());
        }
        else
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
