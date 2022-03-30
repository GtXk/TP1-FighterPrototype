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
            SendLeaderboard(1); //Adds 1 win to the leaderboard database
            lostCanvas.gameObject.SetActive(false); 
            winCanvas.gameObject.SetActive(true); //enables winner screen
        }
        if(GetPlayerState() == FightState.LOST)
        {
            winCanvas.gameObject.SetActive(false);
            lostCanvas.gameObject.SetActive(true); //enables loser screen
        }
    }

    public FightState GetPlayerState() //returns the state of the fightsystem - different to each player - at the end of the game one player will have a "LOST" and the other a "WIN" state.
    {
        return FightSystem.state;
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    //update leaderboards
    public void SendLeaderboard(int score) //Increments the score of the "Wins" for the leaderboarder (uploads to the database)
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
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError); //After it is sent it either runs the "OnLeaderBoardUpdate" or "OnError" function. "OnError" only is called when there is a error.
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult Wins) //If the SendLeaderBoard is successful this function is called
    {
        GetAccountInfo();
        
        Debug.Log("Successfully sent"); //Informs that the leaderboard has been updated.


    }
    void OnError(PlayFabError error) //If an error has occured with SendLeaderboard()
    {
        Debug.Log("Coudnt update leaderboard"); //allows to somewhat pinpoint the error.
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
