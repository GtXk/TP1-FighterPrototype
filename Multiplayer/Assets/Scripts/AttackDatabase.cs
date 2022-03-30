using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDatabase : MonoBehaviour
{
    Attack[] allAttacksDatabase = new Attack[50];

    void Start()
    {

    }

    public Attack getAttack(int pos)
    {
        return allAttacksDatabase[pos];
    }

    public void SetUP()
    {
        //attackname, type, damage, accuracy, heal, manacost
        allAttacksDatabase[0] = new Attack("Punch", "Basic", 8, 98, 0, 0);
        allAttacksDatabase[1] = new Attack("Burn", "Fire", 12, 90, 0, 10);
        allAttacksDatabase[2] = new Attack("Fire Nova", "Fire", 22, 50, 0, 20);
        allAttacksDatabase[3] = new Attack("Fire Drain", "Fire", 10, 82, 10, 20);

        allAttacksDatabase[4] = new Attack("Kick", "Basic", 8, 98, 0, 0);
        allAttacksDatabase[5] = new Attack("Water Whip", "Water", 12, 90, 0, 10);
        allAttacksDatabase[6] = new Attack("Tsunami", "Water", 22, 50, 50, 20);
        allAttacksDatabase[7] = new Attack("Water Shield", "Water", 0, 82, 15, 20);

        allAttacksDatabase[8] = new Attack("Dropkick", "Basic", 8, 98, 0, 0);
        allAttacksDatabase[9] = new Attack("Rock Blast", "Earth", 12, 90, 0, 10);
        allAttacksDatabase[10] = new Attack("Earthquake", "Earth", 22, 80, 0, 20);
        allAttacksDatabase[11] = new Attack("Rock Armour", "Earth", 0, 82, 20, 20);

        allAttacksDatabase[12] = new Attack("Headbutt", "Basic", 8, 98, 0, 0);
        allAttacksDatabase[13] = new Attack("Areial Assault", "Air", 12, 90, 0, 10);
        allAttacksDatabase[14] = new Attack("Tornado", "Air", 22, 80, 0, 20);
        allAttacksDatabase[15] = new Attack("Air Wall", "Air", 0, 82, 10, 20);
    }

    void Update()
    {
        
    }
}
