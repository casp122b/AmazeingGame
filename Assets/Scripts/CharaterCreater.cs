using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class CharaterCreater : MonoBehaviour {

    public InputField firstNameField;
    public InputField lastNameField;
    public InputField charaterNameField;

    private string firstName;
    private string lastName;
    private string charaterName;

    public FirebaseRepo repo;
    //private DatabaseReference _Ref;

    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://a-maze-inggladiator.firebaseio.com/");
        
    }

    public void CreateCharater(string charaterId, string firstName, string lastName, string charaterName)
    {

            firstName = firstNameField.text;
            lastName = lastNameField.text;
            charaterName = charaterNameField.text;

            Charater charater = new Charater(firstName, lastName, charaterName);
            Debug.Log("First Name" + firstName + "Last Name" + lastName);
            Debug.Log("Charater Name" + charaterName);

             string json = JsonUtility.ToJson(charater);
            DatabaseReference _Ref = FirebaseDatabase.DefaultInstance.GetReference("Charater");

            _Ref.Child("charaters").Child(charaterId).SetRawJsonValueAsync(json);
    }
}
