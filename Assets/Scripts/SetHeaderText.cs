using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHeaderText : MonoBehaviour
{
    static Text headerText;
    public static void Set(string text)
    {
        headerText = GameObject.Find("Header Text").GetComponent<Text>();
        headerText.text = text;
    }

    public static string Get()
    {
        headerText = GameObject.Find("Header Text").GetComponent<Text>();
        return headerText.text;
    }
}
