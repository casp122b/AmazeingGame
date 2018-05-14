using Firebase.Auth;
using Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Unity.Editor;

public class CreateUser : MonoBehaviour {

    public InputField passwordField;
    public InputField emailField;
    private string password;
    private string email;

    private static int EMAIL_ERROR_CODE = 1;
    private static int NO_ERRORS = 0;

    void Start()
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://a-maze-inggladiator.firebaseio.com/");
    }

    public void ValidateEmail()
    {

        int eCode = 0;
        eCode = EmailValidator(emailField.text, eCode);
        if (eCode == NO_ERRORS)        
            
        {
            password = passwordField.text;
            email = emailField.text;

            Debug.Log("Password" + password + "Email" + email);
        }
        else if (eCode == EMAIL_ERROR_CODE)
        {
            Debug.Log("Invalidate Email");
        }
    }

    private int EmailValidator(string emailcheck, int crnECode)
    {
        if (!emailcheck.Contains("@") && !emailcheck.Contains("."))
        {
            return EMAIL_ERROR_CODE;
        }
            return crnECode;
    }

    public void LogInButton()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).
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
                //ValidateEmail();
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                SceneManager.LoadSceneAsync("ProfileScene");
            });
    }
    public void CreateUserButton()
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).
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
                //ValidateEmail();
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                SceneManager.LoadSceneAsync("ProfileScene");
            });
    }
}