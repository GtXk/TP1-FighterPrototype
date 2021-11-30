using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fighter : MonoBehaviourPun
{

    public string FighterName;
    public int dmg;
    public int maxHealth;
    public int currentHealth;

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
