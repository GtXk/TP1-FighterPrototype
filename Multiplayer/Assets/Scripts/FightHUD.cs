using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FightHUD : MonoBehaviourPun, IPunObservable
{
    public Text FighterText;
    public Slider HealthSlider;
    public Slider ManaSlider;

    public void setHUD(Fighter fighter) //This function sets up the hud by getting all the information of the fighter.
    {
        FighterText.text = fighter.FighterName;
        HealthSlider.maxValue = fighter.maxHealth;
        HealthSlider.value = fighter.currentHealth;
        ManaSlider.maxValue = fighter.maxMana;
        ManaSlider.value = fighter.currentMana;
    }

    public void setHealth(int Health)
    {
        HealthSlider.value = Health;
        photonView.RPC("setHealthPhoton", RpcTarget.All, Health); //Updates all clients including the host to update the Health bar slider
    }

    public void setMana(int mana)
    {
        ManaSlider.value = mana;
        photonView.RPC("setManaPhoton", RpcTarget.All, mana);//Updates all clients including the host to update the Mana bar slider
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           
        }
        else if (stream.IsReading)
        {
           
        }
    }
    
    [PunRPC]
    void setHealthPhoton(int health) //Functions which is called above by photonView.RPC("setHealthPhoton", RpcTarget.All, Health);
    {
        HealthSlider.value = health;
    }

    [PunRPC]
    void setManaPhoton(int mana) //Functions which is called above by photonView.RPC("setManaPhoton", RpcTarget.All, mana);
    {
        ManaSlider.value = mana;
       
    }
}



