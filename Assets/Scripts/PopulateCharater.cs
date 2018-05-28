using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using Firebase.Auth;

public class PopulateCharater : MonoBehaviour {




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
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://amazeinggame-a414a.firebaseio.com/");
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
}

