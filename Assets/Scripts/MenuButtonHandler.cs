using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour {

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
}
