using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuNavigation : MonoBehaviour
{
    public GameObject levels;
    public GameObject sections;
    public GameObject subsections;
    public GameObject words;
    public GameObject game;
    public GameObject keyboardEN;
    public List<GameObject> windows;
    public Dropdown languages;
    public Text headerText;

    private void Start()
    {
        levels.name = (Language.Get() == "ru") ? "Меню" : "Menu";
        windows.Add(levels);

        //список языков
        languages.options.Clear();

        List<string> lang = new List<string>() { "Английский язык/English", "Русский язык/Russian" };
        languages.AddOptions(lang);

        //выберем по-умполчанию Английский
        languages.value = 0;
        Language.Set("en");

        LoadLevels();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Back();
            return;
        }
    }

    /// <summary>
    /// Загрузка списка уровней
    /// </summary>
    public void LoadLevels()
    {
        //очистка меню
        ClearMenu(levels.transform.
            Find("List").transform.
            Find("Viewport").transform.
            Find("Items").transform);

        string query = "select * from уровни";
        CRUD crud = GameObject.Find("Connector").GetComponent<CRUD>();
        crud.OpenDataBase();
        SqliteDataReader reader = crud.Select(query);
        while (reader.Read())
        {
            //создаем уровни
            GameObject level = Instantiate(Resources.Load("Уровень"), levels.transform.Find("List").transform.Find("Viewport").transform.Find("Items")) as GameObject;
            level.name = reader[1].ToString();
            //задание текста на основе выбранного языка
            level.transform.Find("Text").GetComponent<Text>().text = (Language.Get() == "ru") ? reader[2].ToString() : reader[1].ToString();

            //задаем им действие
            level.GetComponent<Button>().onClick.AddListener(() => level.GetComponent<SetLevel>().Set(sections));
        }
        crud.CloseDataBase();

        //шапка приложения
        headerText.text = (Language.Get() == "ru") ? "Меню" : "Menu";
    }

    /// <summary>
    /// Возврат на главную страницу
    /// </summary>
    public void Home()
    {
        while (true)
        {
            if (windows.Count > 1) PreviousWindow(); else break;
        }
    }

    /// <summary>
    /// Выход из приложения
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Кнопка назад
    /// </summary>
    public void Back()
    {
        //если мы внутри теста
        if (windows[windows.Count - 1].name == "Game")
        {
            //оповещение
            GameObject message = Instantiate(Resources.Load("Message"), GameObject.Find("Canvas").transform) as GameObject;
            message.transform.SetAsLastSibling();
            message.GetComponent<Message>().text.text = (Language.Get() == "ru") ?
                "Вы уверены, что хотите прервать прохождение теста?" :
                "Are you sure you want to interrupt the test?";

            message.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => message.GetComponent<Message>().QuitTest());
        }
        else
            PreviousWindow();
    }

    public void PreviousWindow()
    {
        if (windows.Count > 1)
        {
            //если мы в окне перед началом теста
            if (windows[windows.Count - 1].name == "Words")
            {
                //удалим кнопку выбора режима
                for (int i = 0; i < windows[windows.Count - 1].transform.GetChild(1).transform.childCount; i++)
                {
                    Destroy(windows[windows.Count - 1].transform.GetChild(1).transform.GetChild(i).gameObject);
                }
                
            }

            DeletePreviousWindow();

            SetHeaderText.Set(windows[windows.Count - 1].name);
        }
    }

    /// <summary>
    /// Отключение окна
    /// </summary>
    public void DisabledPreviousWindow()
    {
        windows[windows.Count - 1].SetActive(false);
    }

    /// <summary>
    /// Отключает и удаляет окно из очереди
    /// </summary>
    public void DeletePreviousWindow()
    {
        DisabledPreviousWindow();
        windows.RemoveAt(windows.Count - 1);
        windows[windows.Count - 1].SetActive(true);
    }

    /// <summary>
    /// Очистка элемента меню
    /// </summary>
    /// <param name="parent">Объект, в котором удаляются дочерние объекты</param>
    public void ClearMenu(Transform parent)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);
    }

    /// <summary>
    /// Выбор языка
    /// </summary>
    public void SelectLanguage()
    {
        //если выбран английский
        if (languages.value == 0)
            Language.Set("en");
        else
            Language.Set("ru");

        levels.name = (Language.Get() == "ru") ? "Меню" : "Menu";

        //оповещение
        GameObject message = Instantiate(Resources.Load("Message"), GameObject.Find("Canvas").transform) as GameObject;
        message.transform.SetAsLastSibling();
        message.GetComponent<Message>().text.text = (Language.Get() == "ru") ?
        "Вы будете перенаправлены в главное меню" :
        "You will be redirected to the main menu";

        message.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => message.GetComponent<Message>().ClickOK());

    }
}
