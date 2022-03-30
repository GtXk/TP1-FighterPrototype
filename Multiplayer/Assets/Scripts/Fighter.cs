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
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public string charType;
    public int rng;
   public int fightBoost = 0;
    public int fightBoost2 = 0;
    public int damageboost = 0;
    public int accuracyDebuff = 0;
    public FightSystem script;

    public AttackDatabase myAttackDatabase;
    public AttackSet figherAttackSet;
    public int fighterID;

    public Sprite playerimg;

    public Fighter(string fighterName, string playerName, int fighterID)
    {
        this.FighterName = fighterName;
        this.playerName = playerName;
        this.fighterID = fighterID;
        myAttackDatabase = new AttackDatabase();
        UpdateChar();

    }

    public void Start()
    {
        
           fightBoost = PlayerPrefs.GetInt("SelectedItem");

           fightBoost2 = PlayerPrefs.GetInt("SelectedItem2");
     
        
        UpdateChar();
    }
    
    //set player attacks
    public void UpdateChar()
    {
        myAttackDatabase = new AttackDatabase();
        switch (this.fighterID)
        {
            case 0:
         
                myAttackDatabase.SetUP();
                charType = "Fire";
          
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(0), myAttackDatabase.getAttack(1), myAttackDatabase.getAttack(2), myAttackDatabase.getAttack(3));
                break;
            case 1:
                myAttackDatabase.SetUP();
                this.charType = "Water";
      
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(4), myAttackDatabase.getAttack(5), myAttackDatabase.getAttack(6), myAttackDatabase.getAttack(7));
                break;
            case 2:
                myAttackDatabase.SetUP();
                this.charType = "Earth";
       
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(8), myAttackDatabase.getAttack(9), myAttackDatabase.getAttack(10), myAttackDatabase.getAttack(11));
                break;
            case 3:
                myAttackDatabase.SetUP();
                this.charType = "Air";
           
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(12), myAttackDatabase.getAttack(13), myAttackDatabase.getAttack(14), myAttackDatabase.getAttack(15));
                break;
            default:
                Debug.Log("oh no");
                break;
        }
      
    }

    public void setPlayerNum(int num)
    {
        this.playerNum = num;
    }
    public void Heal(Attack heal)
    {
        int addhealth = heal.getHeal();
        currentHealth += addhealth;
    }
    public void TakeDamage(Attack anyAttack)
    {
        //have master client calculate rng, fixes any any errors related to rng






     
        script = FindObjectOfType<FightSystem>();

        int attack = anyAttack.getDamage();
        int accuracy = anyAttack.getAccuracy();
        rng = script.rng;
        if (accuracyDebuff == 1)
        {
            rng += 10;
        }
        Debug.LogError("calculatedrng: " + rng);
        if (rng > accuracy)
            {

            return;

        }
        else
        {


            if (CounterElement(anyAttack) == true)
            {
                currentHealth -= (attack * 2) + damageboost;

                // photonView.RPC("elemntaldamage", RpcTarget.Others, attack);

            }
            else

                currentHealth -= (attack) + damageboost;


            //photonView.RPC("normaldamage", RpcTarget.Others, attack);


        }
        

    }



    public void manaUsed(Attack anyAttack)
    {
      
        {
            currentMana -= anyAttack.getMana();
        }
    }

    public void manaGained(Attack anyAttack)
    {
        if(anyAttack.getType() == "Basic")
        {
            if (currentMana < maxMana)
            {
                currentMana += 4;
            }
        }
    }

    public bool CounterElement(Attack anyAttack)
    {
        if(this.charType == "Fire" && anyAttack.getType() == "Earth")
        {
            return true;
        }
        else if(this.charType == "Earth" && anyAttack.getType() == "Water")
        {
            return true;
        }
        else if(this.charType == "Water" && anyAttack.getType() == "Air")
        {
            return true;
        }
        else if(this.charType == "Air" && anyAttack.getType() == "Fire")
        {
            return true;
        }
        else
        {
            return false;
        }
    }



   
}
