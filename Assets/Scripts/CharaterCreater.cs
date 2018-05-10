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

    public void CreateCharater()
    {
            firstName = firstNameField.text;
            lastName = lastNameField.text;
            charaterName = charaterNameField.text;

            Debug.Log("First Name" + firstName + "Last Name" + lastName);
            Debug.Log("Charater Name" + charaterName);

        DatabaseReference _Ref = FirebaseDatabase.DefaultInstance.GetReference("Charater").Child("FirstName");

        repo.Push(firstName + lastName);
        _Ref.SetValueAsync(firstName);
    }
}
