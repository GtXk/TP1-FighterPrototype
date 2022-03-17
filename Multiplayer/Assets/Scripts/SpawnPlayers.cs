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
        
        CheckPlayers();
        if (numberPlayers == 1)
        {


            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(173f, 213f, 0f), Quaternion.identity);
        }
        
        else if (numberPlayers == 2)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(866f, 197f, 0f), Quaternion.Euler(0f, 180f, 0f));
        }

        void CheckPlayers()
        {
            numberPlayers = PhotonNetwork.PlayerList.Length;
            //if the number of player is heigher than the number of spawnpoint in the game (in this case 2),
            //spawn the players in round order
            for (int i = 0; i <= numberPlayers; i++) //what is this?
            {
                if (numberPlayers > 4)
                {
                    numberPlayers -= 4; // so just takes away number of players? -4 ?
                }

            }
        }
        
    }

   
}

