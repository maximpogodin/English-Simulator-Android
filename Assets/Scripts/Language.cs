using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public static string selectedLanguage;

    public static void Set(string language)
    {
        selectedLanguage = language;
    }

    public static string Get()
    {
        return selectedLanguage;
    }
}
