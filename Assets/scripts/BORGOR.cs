using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BORGOR : ScriptableObject
{
    private string[] ingridiants;
    private int nextIngridiantToView = 0;
    public string getNextIngridiant()
    {
        if (nextIngridiantToView == ingridiants.Length) { return ""; }
        nextIngridiantToView++;
        return ingridiants[nextIngridiantToView-1];
    }
}
