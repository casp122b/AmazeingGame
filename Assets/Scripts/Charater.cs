using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour {

    string FirstName, LastName, CharaterName;

    public Charater()
    {
    }

    public Charater(string FirstName, string LastName, string CharaterName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.CharaterName = CharaterName;
    }
}
