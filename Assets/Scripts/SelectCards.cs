using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.Linq;

public class SelectCards : MonoBehaviour
{
    /// <summary>
    /// Запуск режима Карточек
    /// </summary>
    public void StartGame()
    {
        //прячем кнопки
        transform.parent.transform.GetChild(transform.parent.childCount - 1).gameObject.GetComponent<SelectButton>().HideAll();

        //запуск режима
        SetHeaderText.Set((Language.Get() == "ru") ? "Карточки" : "Cards");

        //создаем игровое окно
        GameObject cards = Instantiate(Resources.Load("Cards"), GameObject.Find("Game").transform) as GameObject;

        //переходим к игре
        cards.GetComponent<Cards>().en = RU_EN_List.Get("en");
        cards.GetComponent<Cards>().ru = RU_EN_List.Get("ru");

        //задаем заголовок внутри игры Карточки
        cards.transform.Find("Word Panel").transform.Find("Header").GetComponent<Text>().text = 
            (Language.Get() == "ru") ? LevelSectionSub.selectedSubRU : LevelSectionSub.selectedSubRU;
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
