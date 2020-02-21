using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTest2 : MonoBehaviour
{
    /// <summary>
    /// Запуск режима Тест 2
    /// </summary>
    public void StartGame()
    {
        //прячем кнопки
        transform.parent.transform.GetChild(transform.parent.childCount - 1).gameObject.GetComponent<SelectButton>().HideAll();

        //запуск режима
        SetHeaderText.Set((Language.Get() == "ru") ? "Тестирование 2" : "Test 2");

        //переходим к игре, сохраняем список
        GameObject test2 = Instantiate(Resources.Load("Test 2"), GameObject.Find("Game").transform) as GameObject;

        //получаем список слов
        test2.GetComponent<Test2>().en = RU_EN_List.Get("en");
        test2.GetComponent<Test2>().ru = RU_EN_List.Get("ru");

        //задаем заголовок внутри игры Test 1
        test2.transform.Find("Word Panel").transform.Find("Header").GetComponent<Text>().text 
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
