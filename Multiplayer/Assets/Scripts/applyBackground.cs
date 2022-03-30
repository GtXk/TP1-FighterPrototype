using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class applyBackground : MonoBehaviourPun
{
    public GameObject[] backgrounds;
    public int selectedBackground = 0;
    void Start()
    {

        photonView.RPC("addBackground", RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
   public void addBackground()
    {
        selectedBackground = PlayerPrefs.GetInt("SelectedBackground");
        backgrounds[selectedBackground].SetActive(true);
        Debug.Log("Selected:" + selectedBackground);
    }
}
