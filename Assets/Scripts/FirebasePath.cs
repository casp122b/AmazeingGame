using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Firebase/Path")]
public class FirebasePath : ScriptableObject
{

    public string BasePath, DBVersion, ObjectTypeName;
    public bool IsPlural;

    private string FullPath { get { return DBVersion + "/" + BasePath + "/" + ObjectTypeName + (IsPlural ? "s" : ""); } }

    public Firebase.Database.DatabaseReference GetReferenceFromRoot (Firebase.Database.DatabaseReference root)
    {
        var objectTypeName = ObjectTypeName + (IsPlural ? "s" : "");
        return root.Child(DBVersion).Child(BasePath).Child(ObjectTypeName);
    }
}
