using Firebase.Auth;
using Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Unity.Editor;
using System;

public class CreateUser : MonoBehaviour {

    public InputField passwordField;
    public InputField emailField;
    private string password;
    private string email;
    private string charaterId;

    private static int EMAIL_ERROR_CODE = 1;
    private static int NO_ERRORS = 0;

    FirebaseAuth auth;

    public void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        DontDestroyOnLoad(auth);
    }

    private void DontDestroyOnLoad(FirebaseAuth auth)
    {
        throw new NotImplementedException();
    }

    public void LogInButton()
    {
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).
            ContinueWith(task => 
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                SceneManager.LoadSceneAsync("ProfileScene");
            });
    }
    public void CreateUserButton()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).
            ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                SceneManager.LoadSceneAsync("ProfileScene");
                charaterId = auth.CurrentUser.UserId;
                Debug.Log("Charater Name" + charaterId);
            });
    }
}