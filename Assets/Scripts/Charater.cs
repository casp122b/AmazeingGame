using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater {

    string CharaterId, FirstName, LastName, CharaterName;

    public Charater()
    {
    }

    public Charater(string charaterId, string firstName, string lastName, string charaterName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.CharaterName = charaterName;
        this.CharaterId = charaterId;
    }
}
