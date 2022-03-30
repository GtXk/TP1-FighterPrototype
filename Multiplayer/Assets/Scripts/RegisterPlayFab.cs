using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseAPI.Account;

public class RegisterPlayFab : MonoBehaviour
{
    // Start is called before the first frame update

    public void GetUserEmail(string value)
    {
        AccountController.controller.GET_USER_EMAIL(value);
    }
    public void GetUserName(string value)
    {
        AccountController.controller.GET_USER_USERNAME(value);
    }
    public void GetUserPassword(string value)
    {
        AccountController.controller.GET_USER_PASSWORD(value);
    }
    public void SignUp()
    {
        AccountController.controller.ON_CLIC_CREATE_ACCOUNT();
    }
   public void LogIn()
    {
        AccountController.controller.LOGIN_ACTION();
    }
}
