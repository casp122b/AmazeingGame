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

    FirebaseAuth auth;
    DatabaseReference _Ref;

    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://a-maze-inggladiator.firebaseio.com/");
        auth = FirebaseAuth.DefaultInstance;
        _Ref = FirebaseDatabase.DefaultInstance.GetReference(auth.CurrentUser.UserId);
        populateData();
    }

    public void populateData()
    {
        _Ref.Child("firstname").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                firstNameField.text = snapshot.Value.ToString();
            }
        });

        _Ref.Child("lastname").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                lastNameField.text = snapshot.Value.ToString();
            }
        });

        _Ref.Child("charatername").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                charaterNameField.text = snapshot.Value.ToString();
            }
        });
    }

        public void CreateCharater()
    {        
        Charater charater = new Charater(charaterId, firstName, lastName, charaterName);
        firstName = firstNameField.text;
        lastName = lastNameField.text;
        charaterName = charaterNameField.text;
        charaterId = auth.CurrentUser.UserId;
                
        Debug.Log("First Name" + firstName + "Last Name" + lastName);
        Debug.Log("Charater Name" + charaterName);
        Debug.Log("Charater Name" + charaterId);

        _Ref.Child("firstname").SetValueAsync(firstName);
        _Ref.Child("lastname").SetValueAsync(lastName);
        _Ref.Child("charatername").SetValueAsync(charaterName);
        string json = JsonUtility.ToJson(charater);
        _Ref.Child("charaters").Child(charaterId).SetRawJsonValueAsync(json);
    }
}
