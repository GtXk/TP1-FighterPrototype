using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class itemSelect : MonoBehaviourPun

{
    public GameObject roomPanel;
    public GameObject LobbyMain;
    public GameObject waiting;
    public GameObject backgroundSelector;
    public GameObject skillSelect;
    public int selectedItem;
    public int selectedItem2;

    void Start()
    {
        //variables that store the users choice
        selectedItem = PlayerPrefs.GetInt("SelectedItem", 0);
        selectedItem2 = PlayerPrefs.GetInt("SelectedItem2", 0);
    }

   
  public void onHealButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            selectedItem = 1;
            photonView.RPC("sendItemSelect", RpcTarget.All, selectedItem);
        }
        else
        {
            selectedItem2 = 1;
            photonView.RPC("sendItemSelect2", RpcTarget.All, selectedItem2);
        }
    }
    public void onAccuracyButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            selectedItem = 2;
            photonView.RPC("sendItemSelect", RpcTarget.All, selectedItem);
        }
        else
        {
            selectedItem2 = 2;
            photonView.RPC("sendItemSelect2", RpcTarget.All, selectedItem2);
        }
    }
    public void onDamageButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            selectedItem = 3;
            photonView.RPC("sendItemSelect", RpcTarget.All, selectedItem);
        }
        else
        {
            selectedItem2 = 3;
            photonView.RPC("sendItemSelect2", RpcTarget.All, selectedItem2);
        }
    }
    public void onDoneButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            skillSelect.SetActive(false);
            backgroundSelector.SetActive(true);
        }
        else
        {
            skillSelect.SetActive(false);
            waiting.SetActive(true);


        }
    }

    [PunRPC]
    public void sendItemSelect(int selectedItem)
    {
        PlayerPrefs.SetInt("SelectedItem", selectedItem);
        Debug.Log(selectedItem);

    }
    [PunRPC]
    public void sendItemSelect2(int selectedItem2)
    {
        PlayerPrefs.SetInt("SelectedItem2", selectedItem2);
        Debug.Log(selectedItem2);

    }
}
