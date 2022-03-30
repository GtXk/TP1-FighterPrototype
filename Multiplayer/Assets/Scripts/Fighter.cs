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
    public string charType;

    public int maxHealth;
    public int maxMana;
    public int currentHealth;
    public int currentMana;
    public int rng;

    public int fightBoost = 0;
    public int fightBoost2 = 0;
    public int damageboost = 0;
    public int accuracyDebuff = 0;
    public FightSystem script;

    public AttackDatabase myAttackDatabase;
    public AttackSet figherAttackSet; //enables the character to have 1 attack set which contains 4 attacks.
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
    
    public void UpdateChar() //Depending on the type of character - the character will recieve different types of attacks from the attack database which will put put into a attackset.
    {
        myAttackDatabase = new AttackDatabase();
        myAttackDatabase.SetUP(); //sets up the database with all of the attacks
        switch (this.fighterID) //each character has a unique id on which it can be differentiated - this enables things like giving it the appropriate attacks.
        {
            case 0: 
                charType = "Fire"; //gives the character their elemental type.
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(0), myAttackDatabase.getAttack(1), myAttackDatabase.getAttack(2), myAttackDatabase.getAttack(3));
                break;
            case 1:
                this.charType = "Water";
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(4), myAttackDatabase.getAttack(5), myAttackDatabase.getAttack(6), myAttackDatabase.getAttack(7));
                break;
            case 2:
                this.charType = "Earth";
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(8), myAttackDatabase.getAttack(9), myAttackDatabase.getAttack(10), myAttackDatabase.getAttack(11));
                break;
            case 3:
                this.charType = "Air";
                figherAttackSet = new AttackSet(myAttackDatabase.getAttack(12), myAttackDatabase.getAttack(13), myAttackDatabase.getAttack(14), myAttackDatabase.getAttack(15));
                break;
            default:
                Debug.Log("Character unique id not found"); //This is for debugging - incase a characters unique id cannot be found then some of the problems can be identified.
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
        int attack = anyAttack.getDamage(); //this is most likely not required however due to time constraints - tests whether it works without it have not been conducted. - instead of int attack = anyAttack.getDamage() can be used instead.
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
                float temp = (float)(attack * 1.5); 
                //Mathf.RoundToInt(temp)
                currentHealth -= Mathf.RoundToInt(temp) + damageboost; //damage and health are ints and therefore a conversion to the nearest int is required.
                Debug.LogError("the temp value is " + temp);           // since the fight system only uses int and this is the only occasion a float has been used.
            }
            else
            {
                currentHealth -= (attack) + damageboost;
            }
        }
    }

    public void manaUsed(Attack anyAttack) // calculates if the move is basic and then gives the character some mana. (only way to generate mana which can be used for powerful moves)
    {
        if (anyAttack.getType() == "Basic")
        {
            if (currentMana < maxMana) //makes sure the mana doesnt go over the maximum mana.
            {
                currentMana -= anyAttack.getMana(); //basic attacks have negative mana and therefore - - 10 mana would equal + 10 for each basic attack.
            }
        }
        else
        {
            currentMana -= anyAttack.getMana();
        }
    }

    public bool CounterElement(Attack anyAttack) //Gets the character type and the type of attack and sees if the attack will deal extra damage.
    {
        if(this.charType == "Fire" && anyAttack.getType() == "Earth")   //Air -> Water -> Earth -> Fire -> Air  this shows how each element beats other element.
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
            return false; //if none of the elements counter then returns false.
        }
    }



   
}
