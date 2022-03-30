using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BackgroundChange : MonoBehaviourPunCallbacks, IPunObservable
{
    
    public GameObject[] backgrounds;
    public int selectedBackground= 0;
    void Start()
    {
        selectedBackground = PlayerPrefs.GetInt("SelectedBackground", 0);
        foreach (GameObject image in backgrounds)
            image.SetActive(false);
        backgrounds[selectedBackground].SetActive(true);
    }

    public void NextOption()
    {
        if (PhotonNetwork.IsMasterClient)
        {


            backgrounds[selectedBackground].SetActive(false);
            selectedBackground++;
            if (selectedBackground == backgrounds.Length)
                selectedBackground = 0;

            backgrounds[selectedBackground].SetActive(true);
            PlayerPrefs.SetInt("SelectedBackground", selectedBackground);
            photonView.RPC("sendBackground", RpcTarget.All, selectedBackground);
        }
    

    }
    public void backOption()
    {
        if (PhotonNetwork.IsMasterClient)
        {


            backgrounds[selectedBackground].SetActive(false);
            selectedBackground--;
            if (selectedBackground == -1)
                selectedBackground = backgrounds.Length-1;

            backgrounds[selectedBackground].SetActive(true);
            PlayerPrefs.SetInt("SelectedBackground", selectedBackground);
            photonView.RPC("sendBackground", RpcTarget.All, selectedBackground);
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

    public void OnClickPlayButton()
    {

       
        photonView.RPC("sendBackground", RpcTarget.All, selectedBackground);
        photonView.RPC("loadLevel", RpcTarget.All);
    }
   
    [PunRPC]
    public void loadLevel()
    {
        PhotonNetwork.LoadLevel("Game");

    }
    [PunRPC]
    public void sendBackground(int selectedBackground)
    {
        PlayerPrefs.SetInt("SelectedBackground", selectedBackground);
        Debug.Log(selectedBackground);

    }
}
