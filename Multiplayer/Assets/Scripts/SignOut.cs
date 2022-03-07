using System.Collections.Generic;
using PlayFab.ClientModels;
using System.Collections;
using UnityEditor;
using UnityEngine;
using PlayFab;
using DatabaseAPI.Account;
using UnityEngine.SceneManagement;

public class SignOut : MonoBehaviour
{
    // Start is called before the first frame update
    public void LogOut()
    {
        AccountController.controller.LOG_OUT();
    }
}
