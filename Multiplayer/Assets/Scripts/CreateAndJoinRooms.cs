using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using DatabaseAPI.Account;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public Text userIdText;
    public static string userID;
    public static string userName;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject lobbyMain;
    public GameObject roomPanel;
  


    private void Start()
    {
        if(PhotonNetwork.InRoom == true)
        {
            PhotonNetwork.LeaveRoom();
        }

        StartCoroutine(UpdateAllStats());
        GetLeaderboard();
    }


    IEnumerator UpdateAllStats()
    {

        yield return new WaitForSeconds(1);
        if (AccountController.userID == null)
        {
            userIdText.text = "Connected anonymously..."; // Display this if the user doesn't have an id, generally with an anonymous connection on mobile devices
        }
        else
        {
            userIdText.text = "Connected to user " + AccountController.userName; // display the user ID in playfab servers; you can also type userName instead of userID to get the username
        }
    }
    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }
    void Successs(GetAccountInfoResult result)
    {
        userID = result.AccountInfo.PlayFabId;
        userName = result.AccountInfo.Username;
        Debug.Log(userID);
        Debug.Log("test");
    }
    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }
    public void CreateRoom()
    {

        PhotonNetwork.CreateRoom(createInput.text);
  
    }


    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(joinInput.text);


    }
    public  void JoinRandom()
    {
       
        PhotonNetwork.JoinRandomOrCreateRoom();
    }


    public override void OnJoinedRoom()

    {
        lobbyMain.SetActive(false);
        roomPanel.SetActive(true);
    }
    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Wins",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult Wins)
    {
        Debug.Log("Successfully sent");

    }
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Wins",
            StartPosition = 0,
            MaxResultsCount = 6


        };
        PlayFabClientAPI.GetLeaderboard(request, onLeaderboardGet, OnError);
    }
    void onLeaderboardGet(GetLeaderboardResult Wins)
    {
        foreach (var item in Wins.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();


        }
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error");
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
    public void OnClickMenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
