using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button exitButton;

    public void PlayGame()
    {
        StartCoroutine(ButtonDelay(playButton));
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    IEnumerator ButtonDelay(Button myButton)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
