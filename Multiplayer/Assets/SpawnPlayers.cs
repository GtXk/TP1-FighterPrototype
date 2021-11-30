using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    int numberPlayers;
    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(173f, 213f, 0f), Quaternion.identity);
        CheckPlayers();
        if (numberPlayers == 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(866, 197, 0f), Quaternion.identity);
        }

        void CheckPlayers()
        {
            numberPlayers = PhotonNetwork.PlayerList.Length;
            //if the number of player is heigher than the number of spawnpoint in the game (in this case 4),
            //spawn the players in round order
            for (int i = 0; i <= numberPlayers; i++)
            {
                if (numberPlayers > 4)
                {
                    numberPlayers -= 4;
                }

            }
        }
    }

   
}

