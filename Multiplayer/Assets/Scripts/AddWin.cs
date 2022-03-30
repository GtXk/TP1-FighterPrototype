using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using DatabaseAPI.Account;
using UnityEngine.SceneManagement;
public class AddWin : MonoBehaviour
{
    public static string userID;
    public static string userName;

    public Canvas winCanvas;
    public Canvas lostCanvas;

    private void Start()
    {
        if (GetPlayerState() == FightState.WON)
        {
            SendLeaderboard(1);
            lostCanvas.gameObject.SetActive(false);
            winCanvas.gameObject.SetActive(true);
        }
        if(GetPlayerState() == FightState.LOST)
        {
            winCanvas.gameObject.SetActive(false);
            lostCanvas.gameObject.SetActive(true);
        }
    }

    public FightState GetPlayerState()
    {
        return FightSystem.state;
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    //update leaderboards
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
        GetAccountInfo();
        
        Debug.Log("Successfully sent");


    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error");
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
        Debug.Log(userName);
     
        Debug.Log("test");
    }
    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }

}
