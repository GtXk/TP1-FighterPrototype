using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class FightHUD : MonoBehaviourPun, IPunObservable
{
    public Text FighterText;
    public Slider HealthSlider;

    public void setHUD(Fighter fighter)
    {
        FighterText.text = fighter.FighterName;
        HealthSlider.maxValue = fighter.maxHealth;
        HealthSlider.value = fighter.currentHealth;
    }

    public void setHealth(int Health)
    {
        HealthSlider.value = Health;
        photonView.RPC("test", RpcTarget.All, HealthSlider.value);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           // stream.SendNext(HealthSlider.value);
        }
        else if (stream.IsReading)
        {
           // HealthSlider.value = (float)stream.ReceiveNext();
        }
    }
    [PunRPC]
    void test(float sliderValue)
    {
        HealthSlider.value = sliderValue;
    }
}



