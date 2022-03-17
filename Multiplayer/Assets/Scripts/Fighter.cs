using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fighter : MonoBehaviourPun
{
    public int playerNum;
    public string FighterName;
    public string playerName;
    public int dmg; //remove later on.
    public int maxHealth;
    public int currentHealth;

    public AttackDatabase myAttackDatabase;
    public AttackSet figherAttackSet;
    public int fighterID;

    public Sprite playerimg;

    public Fighter(string fighterName, string playerName, int fighterID)
    {
        this.FighterName = fighterName;
        this.playerName = playerName;
        //this.fighterImage = fighterImage;
        this.fighterID = fighterID;
        myAttackDatabase = new AttackDatabase();
        UpdateChar();

    }

    public void Start()
    {
        UpdateChar();
    }

    public void UpdateChar()
    {
        myAttackDatabase = new AttackDatabase();
        switch (this.fighterID)
        {
            case 0:
                this.maxHealth = 100;
                Debug.Log(this.maxHealth);
                //myAttackDatabase.getAttack(1)
                myAttackDatabase.SetUP();
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(0), myAttackDatabase.getAttack(1), myAttackDatabase.getAttack(2), myAttackDatabase.getAttack(3));
                break;
            case 1:
                this.maxHealth = 110;
                Debug.Log(this.maxHealth);
                //myAttack = new Attack("Slap1", "Fire", 10, 90, 0, 20);
                break;
            case 2:
                this.maxHealth = 120;
                Debug.Log(this.maxHealth);
                //myAttack = new Attack("Slap1", "Fire", 10, 90, 0, 20);
                break;
            case 3:
                this.maxHealth = 130;
                Debug.Log(this.maxHealth);
                //myAttack = new Attack("Slap1", "Fire", 10, 90, 0, 20);
                break;
            default:
                Debug.Log("oh no");
                break;
        }
        /*
        switch (this.fighterID)
        {
            case 0:
                this.maxHealth = 100;
                Debug.Log(this.maxHealth);
                figherAttackSet.setAttack(new Attack("Slap1", "Fire", 10, 90, 0, 20), 0);
                figherAttackSet.setAttack(new Attack("Slap1", "Fire", 10, 90, 0, 20), 1);
                figherAttackSet.setAttack(new Attack("Slap1", "Fire", 10, 90, 0, 20), 2);
                figherAttackSet.setAttack(new Attack("Slap1", "Fire", 10, 90, 0, 20), 3);
                break;
            case 1:
                this.maxHealth = 110;
                Debug.Log(this.maxHealth);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(0), 0);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(1), 1);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(2), 2);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(3), 3);
                break;
            case 2:
                this.maxHealth = 120;
                Debug.Log(this.maxHealth);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(0), 0);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(1), 1);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(2), 2);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(3), 3);
                break;
            case 3:
                this.maxHealth = 130;
                Debug.Log(this.maxHealth);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(0), 0);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(1), 1);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(2), 2);
                figherAttackSet.setAttack(myAttackDatabase.getAttack(3), 3);
                break;
            default:
                Debug.Log("oh no");
                break;
        }
        */
    }

    public void setPlayerNum(int num)
    {
        this.playerNum = num;
    }

    public bool TakeDamage(Attack anyAttack)
    {

        /*
        int rng = Random.Range(0, 100);
        if(rng > anyAttack.getAccuracy())
        {
            return false;
        }
        else
        */
        //{
            currentHealth -= anyAttack.getDamage();
        //}
        
        //photonView.RPC("healthchange", RpcTarget.All, currentHealth);
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }

    /*
    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        //photonView.RPC("healthchange", RpcTarget.All, currentHealth);
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    */

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;


    }
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {

    //    }
    //    else if (stream.IsReading)
    //    {

    //    }
    //}
    //[PunRPC]
    //void healthchange(int healthValue)
    //{

    //    currentHealth = healthValue;
    //}
}
