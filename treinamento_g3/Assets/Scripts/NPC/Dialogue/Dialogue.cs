using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{

    public string name;
    // Dialogue Type:
    //      -regular dialogue: just text
    //      -shop dialogue: last sentence is shown with options to see items
    //      and exit shop
    public enum DialogueType
    {
        RegularDialogue, ShopDialogue
    }
    public DialogueType type;

    [TextArea(3, 10)]
    public string[] sentences;
}
