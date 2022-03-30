using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSet : MonoBehaviour
{
    Attack[] attackList = new Attack[4];

    public AttackSet(Attack one, Attack two, Attack three, Attack four) //each character will have a attack set which will hold the characters 4 attacks (including the heal).
    {
        attackList[0] = one;
        attackList[1] = two;
        attackList[2] = three;
        attackList[3] = four;
      
    }

    public Attack getAttack(int num)
    {
        return attackList[num];
    }

    public void setAttack(Attack attack, int num)
    {
        attackList[num] = attack;
    }

}