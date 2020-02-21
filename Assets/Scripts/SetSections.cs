using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.UI;

public class SetSections : MonoBehaviour
{
    public static int id;
    public void Set(GameObject window)
    {
        MenuNavigation.FindObjectOfType<MenuNavigation>().subsections.name = (Language.Get() == "ru") ? "Подразделы" : "Subsections";

        MenuNavigation.FindObjectOfType<MenuNavigation>().DisabledPreviousWindow();
        MenuNavigation.FindObjectOfType<MenuNavigation>().windows.Add(window);
        window.SetActive(true);
        SetHeaderText.Set(gameObject.transform.GetChild(0).GetComponent<Text>().text);
        LevelSectionSub.selectedSectionEN = gameObject.name;
        LevelSectionSub.selectedSectionRU = SetHeaderText.Get();
        LoadSubSections();
    }

    void LoadSubSections()
    {
        //очистка
        MenuNavigation.FindObjectOfType<MenuNavigation>().
            ClearMenu(MenuNavigation.FindObjectOfType<MenuNavigation>().
            subsections.transform.
            Find("List").transform.
            Find("Viewport").transform.
            Find("Items").transform);
    
        //загрузка
        string query = "select уровеньразделподраздел.подраздел, подразделы.перевод from уровеньразделподраздел " +
            "inner join подразделы on подразделы.подраздел = уровеньразделподраздел.подраздел " +
            "where уровень = '" + LevelSectionSub.selectedLevelEN + "' " +
            "and раздел = '" + LevelSectionSub.selectedSectionEN + "'";

        Debug.Log(query);
        CRUD crud = GameObject.Find("Connector").GetComponent<CRUD>();
        crud.OpenDataBase();
        SqliteDataReader reader = crud.Select(query);
        MenuNavigation navigator = GameObject.Find("Menu Navigator").GetComponent<MenuNavigation>();
        Transform subSections = navigator.subsections.transform;
        while (reader.Read())
        {
            //создаем подразделы
            GameObject sub = Instantiate(Resources.Load("Подраздел"), subSections.Find("List").transform.Find("Viewport").transform.Find("Items")) as GameObject;
            sub.name = reader[0].ToString();
            sub.transform.Find("Text").GetComponent<Text>().text = (Language.Get() == "ru") ? reader[1].ToString() : reader[0].ToString();

            //задаем им действие
            sub.GetComponent<Button>().onClick.AddListener(() => sub.GetComponent<SetSubSections>().Set(navigator.words));
        }
        crud.CloseDataBase();
    }


}
