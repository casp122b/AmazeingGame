using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{

    public void CreateProfileBtn(string newGame)
    {
        SceneManager.LoadScene(newGame);
    }

    public void StartGameBtn(string newGame)
    {
        SceneManager.LoadScene(newGame);
    }

    public void QuitGameBtn()
    {
        Application.Quit();
    }

    public void BackBtn(string MenuScene)
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void SettingsBtn(string SettingsScene)
    {
        SceneManager.LoadScene(SettingsScene);
    }

    public void WebForumBtn()
    {
        Application.OpenURL("https://amazeinggame-a414a.firebaseapp.com/");
    }
}
