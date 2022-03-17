using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack// : MonoBehaviour
{
    public string name;
    public string type;

    public int damage;
    public int accuracy;
    public int heal;
    public int powerPoints;

    public string getName()
    {
        return name;
    }

    public string getType()
    {
        return type;
    }

    public int getDamage()
    {
        return damage;
    }

    public int getAccuracy()
    {
        return accuracy;
    }

    public int getHeal()
    {
        return heal;
    }
    public int getPowerPoints()
    {
        return powerPoints;
    }

    public Attack(string name, string type, int damage, int accuracy, int heal, int powerPoints)
    {
        this.name = name;
        this.type = type;
        this.damage = damage;
        this.accuracy = accuracy;
        this.heal = heal;
        this.powerPoints = powerPoints;
    }
}

