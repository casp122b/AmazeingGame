using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour {

    string CharaterId, FirstName, LastName, CharaterName;

    public Charater()
    {
    }

    public Charater(string charaterId, string FirstName, string LastName, string CharaterName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.CharaterName = CharaterName;
        this.CharaterId = charaterId;
    }
}
