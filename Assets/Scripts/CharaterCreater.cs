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
    //DatabaseReference _Ref;

    private void Awake()
    {
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;
        Firebase.Storage.StorageReference storage_ref = storage.GetReferenceFromUrl("gs://<a-maze-inggladiator.appspot.com>");
        //App.DefaultInstance.SetEditorDatabaseUrl("https://a-maze-inggladiator.firebaseio.com/");
        auth = FirebaseAuth.DefaultInstance;
        //_Ref = FirebaseDatabase.DefaultInstance.GetReference("Charater");
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

        //_Ref.SetValueAsync(firstName);
        //string json = JsonUtility.ToJson(charater);
        //_Ref.Child("charaters").Child(charaterId).SetRawJsonValueAsync(json);
    }
}
