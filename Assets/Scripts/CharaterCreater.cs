using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterCreater : MonoBehaviour {

    public InputField firstNameField;
    public InputField lastNameField;
    public InputField passwordField;
    public InputField emailField;
    public InputField charaterNameField;

    private string firstName;
    private string lastName;
    private string password;
    private string email;
    private string charaterName;

    private static int EMAIL_ERROR_CODE = 1;
    private static int NO_ERRORS = 0;

    public void CreateCharater()
    {

        int eCode = 0;
        eCode = EmailValidator(emailField.text, eCode);
        if (eCode == NO_ERRORS)        
            
        {
            firstName = firstNameField.text;
            lastName = lastNameField.text;
            password = passwordField.text;
            email = emailField.text;
            charaterName = charaterNameField.text;

            Debug.Log("First Name" + firstName + "Last Name" + lastName);
            Debug.Log("Password" + password + "Email" + email);
            Debug.Log("Charater Name" + charaterName);
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
}
