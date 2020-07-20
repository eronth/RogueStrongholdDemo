using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class DebuggerOnScreen
{
    #region UI Text Objects
    // Debugger text for tracking whatever you want.
    private static Text ExtraText = GameObject.Find("ExtraDebugText").GetComponent<Text>();
    public static string Extra
    {
        get { return ExtraText.text; }
        set { ExtraText.text = value; }
    }

    // Debugger text for tracking mouse inputs and position.
    private static Text MouseText = GameObject.Find("MouseDebugText").GetComponent<Text>();
    public static string Mouse
    {
        get { return MouseText.text; }
        set { MouseText.text = value; }
    }

    // Debugger text for tracking characters positions.
    private static Text PositionText = GameObject.Find("PositionDebugText").GetComponent<Text>();
    public static string Position
    {
        get { return PositionText.text; }
        set { PositionText.text = value; }
    }
    #endregion
}
