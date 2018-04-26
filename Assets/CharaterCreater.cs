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

    public void CreateCharater()
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
}
