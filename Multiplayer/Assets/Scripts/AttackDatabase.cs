using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    Attack[] allAttacksDatabase = new Attack[50];

    void Start()
    {
        //allAttacksDatabase[0] = new Attack("Supreme", "Fire", 10, 90, 0, 20);
        //allAttacksDatabase[1] = new Attack("Slap2", "Fire", 20, 90, 0, 20);
        //allAttacksDatabase[2] = new Attack("Slap3", "Fire", 30, 90, 0, 20);
        //allAttacksDatabase[3] = new Attack("Slap4", "Fire", 40, 90, 0, 20);
    }

    public Attack getAttack(int pos)
    {
        return allAttacksDatabase[pos];
    }

    public void SetUP()
    {
        allAttacksDatabase[0] = new Attack("Supreme", "Fire", 10, 100, 0, 20);
        allAttacksDatabase[1] = new Attack("Slap2", "Fire", 20, 95, 0, 20);
        allAttacksDatabase[2] = new Attack("Slap3", "Fire", 30, 90, 0, 20);
        allAttacksDatabase[3] = new Attack("Slap4", "Fire", 40, 85, 0, 20);

        allAttacksDatabase[4] = new Attack("Supreme1", "Fire", 10, 100, 0, 20);
        allAttacksDatabase[5] = new Attack("Slap22", "Fire", 20, 95, 0, 20);
        allAttacksDatabase[6] = new Attack("Slap33", "Fire", 30, 90, 0, 20);
        allAttacksDatabase[7] = new Attack("Slap44", "Fire", 40, 85, 0, 20);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
