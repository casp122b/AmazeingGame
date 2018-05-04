using Firebase.Auth;
using Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateUser : MonoBehaviour {

    public InputField passwordField;
    public InputField emailField;
    private string password;
    private string email;

    private static int EMAIL_ERROR_CODE = 1;
    private static int NO_ERRORS = 0;

    public void CreateCharater()
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

    public void ButtonPressed()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).
            ContinueWith((GameObject) => 
            {
                SceneManager.LoadSceneAsync("ProfileScene");
            });
    }
    public void CreateUserButton()
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).
            ContinueWith((GameObject) => 
            {
                CreateCharater();
            SceneManager.LoadSceneAsync("ProfileScene");
            });
    }
}
