using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.UI;

public class SetSubSections : MonoBehaviour
{
    public void Set(GameObject window)
    {
        MenuNavigation.FindObjectOfType<MenuNavigation>().words.name = (Language.Get() == "ru") ? "Словарь" : "Words";

        MenuNavigation.FindObjectOfType<MenuNavigation>().DisabledPreviousWindow();
        MenuNavigation.FindObjectOfType<MenuNavigation>().windows.Add(window);
        window.SetActive(true);
        SetHeaderText.Set(gameObject.transform.GetChild(0).GetComponent<Text>().text);
        LevelSectionSub.selectedSubEN = gameObject.name;
        LevelSectionSub.selectedSubRU = SetHeaderText.Get();
        LoadWords();
    }

    void Clear()
    {
        MenuNavigation.FindObjectOfType<MenuNavigation>().
            ClearMenu(MenuNavigation.FindObjectOfType<MenuNavigation>().
            words.transform.
            Find("List").transform.
            Find("Viewport").transform.
            Find("Items").transform);
    }

    void LoadWords()
    {
        //очистка
        Clear();

        //списки для формирования игр
        List<string> en = new List<string>();//список английских слов EN
        List<string> ru = new List<string>();//список русских слов RU

        CRUD crud = GameObject.Find("Connector").GetComponent<CRUD>();

        string query = "select EN, RU from перевод where подраздел = (select № from уровеньразделподраздел " +
            "where уровень = '" + LevelSectionSub.selectedLevelEN + "' " +
            "and раздел = '" + LevelSectionSub.selectedSectionEN + "' " +
            "and подраздел = '" + LevelSectionSub.selectedSubEN + "')";

        Debug.Log(query);
        crud.OpenDataBase();
        SqliteDataReader reader = crud.Select(query);
        MenuNavigation navigator = GameObject.Find("Menu Navigator").GetComponent<MenuNavigation>();
        Transform words = navigator.words.transform;
        while (reader.Read())
        {
            //создаем список слов
            GameObject sub = Instantiate(Resources.Load("Слова"), words.Find("List").transform.Find("Viewport").transform.Find("Items")) as GameObject;
            sub.name = reader[0].ToString() + " – " + reader[1].ToString();
            sub.transform.Find("Text").GetComponent<Text>().text = sub.name;

            en.Add(reader[0].ToString());
            ru.Add(reader[1].ToString());

            //возможно добавим озвучивание слов
            //sub.GetComponent<Button>().onClick.AddListener(() => sub.GetComponent<SetSubSections>().Set(navigator.game));
        }
        crud.CloseDataBase();

        //создание кнопки Выбор Режима
        Instantiate(Resources.Load("Button Select Mode"), words.transform.Find("Select Button Panel").transform);

        //сохраняем списки слов
        //RU_EN_List.Clear();
        RU_EN_List.Set(ru, en);
    }
}
