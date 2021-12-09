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

    IEnumerator PlayerOneAttack()
    {
  
        bool isDead = Player2Unit.TakeDamage(Player1Unit.dmg);

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

    IEnumerator PlayerTwoAttack()
    {
        bool isDead = Player1Unit.TakeDamage(Player2Unit.dmg);

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

    public void onAttackButton()
    {
        
        if (state != FightState.PLAYERONETURN)
        {
            photonView.RPC("test", RpcTarget.All);
           // StartCoroutine(PlayerTwoAttack());
        }
        else
        {
            photonView.RPC("test2", RpcTarget.All);
           // StartCoroutine(PlayerOneAttack());
        }
    }
    [PunRPC]
    public void test()
    {
        StartCoroutine(PlayerTwoAttack());
    }
    [PunRPC]
    public void test2()
    {
        StartCoroutine(PlayerOneAttack());
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
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = FightState.PLAYERTWOTURN;
        StartCoroutine(PlayerTwoTurn());
    }

    IEnumerator PlayerTwoHeal()
    {
        Player2Unit.Heal(5);

        player2HUD.setHealth(Player2Unit.currentHealth);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = FightState.PLAYERONETURN;
        StartCoroutine(PlayerOneTurn());
    }

    public void onHealButton()
    {
        if (state != FightState.PLAYERONETURN)
        {
            photonView.RPC("test", RpcTarget.All);
            // StartCoroutine(PlayerTwoHeal());
        }
        else
        {
            photonView.RPC("test2", RpcTarget.All);
            // StartCoroutine(PlayerOneHeal());
        }

    }
}
