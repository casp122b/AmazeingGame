using Firebase.Auth;
using Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharaterCreater : MonoBehaviour {

    public InputField firstNameField;
    public InputField lastNameField;
    public InputField charaterNameField;

    private string firstName;
    private string lastName;
    private string charaterName;

    public void CreateCharater()
    {
            firstName = firstNameField.text;
            lastName = lastNameField.text;
            charaterName = charaterNameField.text;

            Debug.Log("First Name" + firstName + "Last Name" + lastName);
            Debug.Log("Charater Name" + charaterName);

    }
}
