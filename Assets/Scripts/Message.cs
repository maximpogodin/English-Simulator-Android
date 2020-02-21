using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    //текст сообщения
    public Text text;
    MenuNavigation menuNavigation;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => ClickBackground());
        menuNavigation = GameObject.Find("Menu Navigator").GetComponent<MenuNavigation>();
        text = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {

    }

    public void ClickOK()
    {
        //анимация скрытия
        GetComponent<Animator>().SetTrigger("Hide");

        //удаление сообщения
        Destroy(gameObject, 0.2f);

        //возвращаемся домой
        menuNavigation.Home();
        menuNavigation.LoadLevels();
    }

    public void QuitTest()
    {
        MenuNavigation menuNav = MenuNavigation.FindObjectOfType<MenuNavigation>();

        //удалить все объекты внутри окна
        for (int i = 0; i < menuNav.windows[menuNav.windows.Count - 1].transform.childCount; i++)
        {
            Destroy(menuNav.windows[menuNav.windows.Count - 1].transform.GetChild(i).gameObject);
        }

        //анимация скрытия
        GetComponent<Animator>().SetTrigger("Hide");

        //удаление сообщения
        Destroy(gameObject, 0.2f);

        menuNav.PreviousWindow();
        menuNav.PreviousWindow();

        //возвращаемся на окно назад
        menuNav.DeletePreviousWindow();
    }

    public void ClickBackground()
    {
        //анимация скрытия
        GetComponent<Animator>().SetTrigger("Hide");

        //удаление сообщения
        Destroy(gameObject, 0.2f);
    }
}
