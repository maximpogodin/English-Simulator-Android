using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLevel : MonoBehaviour
{
    public void Set(GameObject window)
    {
        MenuNavigation.FindObjectOfType<MenuNavigation>().sections.name = (Language.Get() == "ru") ? "Разделы" : "Sections";

        MenuNavigation.FindObjectOfType<MenuNavigation>().DisabledPreviousWindow();
        MenuNavigation.FindObjectOfType<MenuNavigation>().windows.Add(window);
        window.SetActive(true);
        SetHeaderText.Set(gameObject.transform.GetChild(0).GetComponent<Text>().text);
        LevelSectionSub.selectedLevelEN = gameObject.name;
        LevelSectionSub.selectedLevelRU = SetHeaderText.Get();

        LoadSections();
    }


    public void LoadSections()
    {
        //очистка
        MenuNavigation.FindObjectOfType<MenuNavigation>().
            ClearMenu(MenuNavigation.FindObjectOfType<MenuNavigation>().
            sections.transform.
            Find("List").transform.
            Find("Viewport").transform.
            Find("Items").transform);

        //загрузка
        string query = "select * from разделы";

        Debug.Log(query);
        CRUD crud = GameObject.Find("Connector").GetComponent<CRUD>();
        crud.OpenDataBase();
        SqliteDataReader reader = crud.Select(query);
        MenuNavigation navigator = GameObject.Find("Menu Navigator").GetComponent<MenuNavigation>();
        Transform sections = navigator.sections.transform;
        while (reader.Read())
        {
            //создаем подразделы
            GameObject section = Instantiate(Resources.Load("Раздел"), sections.Find("List").transform.Find("Viewport").transform.Find("Items")) as GameObject;
            section.name = reader[1].ToString();
            section.transform.Find("Text").GetComponent<Text>().text = (Language.Get() == "ru") ? reader[2].ToString() : reader[1].ToString();

            //задаем им действие
            section.GetComponent<Button>().onClick.AddListener(() => section.GetComponent<SetSections>().Set(navigator.subsections));
        }
        crud.CloseDataBase();
    }
}
