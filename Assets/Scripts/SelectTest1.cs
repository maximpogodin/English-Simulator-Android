using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTest1 : MonoBehaviour
{
    /// <summary>
    /// Запуск режима Тест 1
    /// </summary>
    public void StartGame()
    {
        //прячем кнопки
        transform.parent.transform.GetChild(transform.parent.childCount - 1).gameObject.GetComponent<SelectButton>().HideAll();

        //запуск режима
        SetHeaderText.Set((Language.Get() == "ru") ? "Тестирование 1" : "Test 1");

        //переходим к игре, сохраняем список
        GameObject test1 = Instantiate(Resources.Load("Test 1"), GameObject.Find("Game").transform) as GameObject;

        //получаем список слов
        test1.GetComponent<Test1>().en = RU_EN_List.Get("en");
        test1.GetComponent<Test1>().ru = RU_EN_List.Get("ru");

        //задаем заголовок внутри игры Test 1
        test1.transform.Find("Word Panel").transform.Find("Header").GetComponent<Text>().text 
            = (Language.Get() == "ru") ? LevelSectionSub.selectedSubRU : LevelSectionSub.selectedSubRU;
    }

    public void OpenGame(GameObject window)
    {
        MenuNavigation.FindObjectOfType<MenuNavigation>().DisabledPreviousWindow();
        MenuNavigation.FindObjectOfType<MenuNavigation>().windows.Add(window);
        window.SetActive(true);

        //стартуем
        StartGame();
    }
}
