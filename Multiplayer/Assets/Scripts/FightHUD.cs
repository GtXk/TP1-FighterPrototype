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

    public void setHUD(Fighter fighter)
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
        photonView.RPC("setHealthPhoton", RpcTarget.All, Health); //HealthSlider.value
    }

    public void setMana(int mana)
    {
        ManaSlider.value = mana;
        photonView.RPC("setManaPhoton", RpcTarget.All, mana); //HealthSlider.value
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
    void setHealthPhoton(int health)
    {
        HealthSlider.value = health;
    }

    [PunRPC]
    void setManaPhoton(int mana)
    {
        ManaSlider.value = mana;
       
    }
}



