using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedOn : MonoBehaviour
{
    // Todo are we even using this?
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ClickMe()
    {
        DebuggerOnScreen.Mouse = "Clicked via ClickedOn!";
    }
}