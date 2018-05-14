using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using Firebase.Auth;

public class CharaterCreater : MonoBehaviour {

    public InputField firstNameField;
    public InputField lastNameField;
    public InputField charaterNameField;

    private string firstName;
    private string lastName;
    private string charaterName;
    private string charaterId;

    public FirebaseRepo repo;
    //private DatabaseReference _Ref;

    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://a-maze-inggladiator.firebaseio.com/");
        
    }

    public void CreateCharater()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        firstName = firstNameField.text;
        lastName = lastNameField.text;
        charaterName = charaterNameField.text;
        charaterId = auth.CurrentUser.UserId;

        Charater charater = new Charater(charaterId, firstName, lastName, charaterName);
        Debug.Log("First Name" + firstName + "Last Name" + lastName);
        Debug.Log("Charater Name" + charaterName);

        string json = JsonUtility.ToJson(charater);
        DatabaseReference _Ref = FirebaseDatabase.DefaultInstance.GetReference("Charater");

        _Ref.Child("charaters").Child(charaterId).SetRawJsonValueAsync(json);
    }
}
