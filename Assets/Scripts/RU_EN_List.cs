using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RU_EN_List : MonoBehaviour
{
    static List<string> en;//список английских слов EN
    static List<string> ru;//список русских слов RU

    /// <summary>
    /// Задать списки RU-EN
    /// </summary>
    public static void Set(List<string> RU, List<string> EN)
    {
        ru = RU;
        en = EN;
    }

    /// <summary>
    /// Получить список RU-EN
    /// </summary>
    /// <param name="ru_en">Выбор списка из RU-EN</param>
    /// <returns></returns>
    public static List<string> Get(string ru_en)
    {
        if (ru_en == "en")
            return en;
        else
            return ru;
    }

    /// <summary>
    /// Очистка списков RU-EN
    /// </summary>
    public static void Clear()
    {
        en.Clear();
        ru.Clear();
    }
}
