using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class skinManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public GameObject[] skins;
    public GameObject chooseSkill;
    public GameObject roomPanel;
    public GameObject LobbyMain;
    public GameObject skillSelect;
    public GameObject backgroundSelector;
    public int selectedCharacter;
    public int selectedCharacter2;
    public int testselectedCharacter2;
    public TextMeshProUGUI buttontext;
    private void Awake()
    {
    selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
     selectedCharacter2 = PlayerPrefs.GetInt("SelectedCharacter2", 0);
        foreach (GameObject player in skins)
           player.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            skins[selectedCharacter].SetActive(true);
        }
        else
        {
            skins[selectedCharacter2].SetActive(true);
        }
        photonView.RPC("sendCharacterSelect", RpcTarget.All, selectedCharacter);
        photonView.RPC("sendCharacterSelect2", RpcTarget.All, selectedCharacter2);
    }


    public void NextOption()
    {
        if (PhotonNetwork.IsMasterClient)
        {


            skins[selectedCharacter].SetActive(false);
            selectedCharacter++;
            if (selectedCharacter == skins.Length)
                selectedCharacter = 0;

            skins[selectedCharacter].SetActive(true);
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
            photonView.RPC("sendCharacterSelect", RpcTarget.All, selectedCharacter);
        }
        else
        {
         skins[selectedCharacter2].SetActive(false);
            selectedCharacter2++;
            if (selectedCharacter2 == skins.Length)
                selectedCharacter2 = 0;

            skins[selectedCharacter2].SetActive(true);
            PlayerPrefs.SetInt("SelectedCharacter2", selectedCharacter2);
            photonView.RPC("sendCharacterSelect2", RpcTarget.All, selectedCharacter2);
        }


    }
    public void BackOption()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            skins[selectedCharacter].SetActive(false);
            selectedCharacter--;
            if (selectedCharacter == -1)
                selectedCharacter = skins.Length - 1;

            skins[selectedCharacter].SetActive(true);
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
            photonView.RPC("sendCharacterSelect", RpcTarget.All, selectedCharacter);


        }
        else
        {
            skins[selectedCharacter2].SetActive(false);
            selectedCharacter2--;
            if (selectedCharacter2 == -1)
                selectedCharacter2 = skins.Length - 1;

            skins[selectedCharacter2].SetActive(true);
            PlayerPrefs.SetInt("SelectedCharacter2", selectedCharacter2);
            photonView.RPC("sendCharacterSelect2", RpcTarget.All, selectedCharacter2);

        }

    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {


            chooseSkill.SetActive(true);



        }
        else
        {
            chooseSkill.SetActive(false);


        }
    }

    public void onChooseSkill()
    {
        if (selectedCharacter == selectedCharacter2)
        {
            buttontext.text = "Please Change character";
        }
        else
        {
            photonView.RPC("choseSkillScreen", RpcTarget.All);
        }
        



    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        roomPanel.SetActive(false);
        backgroundSelector.SetActive(false);
        LobbyMain.SetActive(true);
    }
    public void onselectbutton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("sendCharacterSelect", RpcTarget.All, selectedCharacter);

        }
        else
        {
            photonView.RPC("sendCharacterSelect2", RpcTarget.All, selectedCharacter2);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {


        }
        else
        {

        }

    }
    [PunRPC]
    public void loadLevel()
    {
        PhotonNetwork.LoadLevel("Game");
        
    }

    [PunRPC]
    public void sendCharacterSelect(int selectedCharacter)
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
       Debug.Log(selectedCharacter);

    }
    [PunRPC]
    public void sendCharacterSelect2(int selectedCharacter2)
    {
        PlayerPrefs.SetInt("SelectedCharacter2", selectedCharacter2);
     Debug.Log(selectedCharacter2);

    }
    [PunRPC]
    public void choseSkillScreen()
    {
        roomPanel.SetActive(false);
        skillSelect.SetActive(true);

    }
}
