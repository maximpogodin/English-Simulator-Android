using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public bool modesShowed = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HideAll();
            return;
        }
    }

    /// <summary>
    /// Скрыть кнопку выбора режима
    /// </summary>
    public void HideSelectButton()
    {
        GetComponent<Animator>().SetInteger("Show", -1);
    }

    public void HideTest1Button()
    {

    }

    /// <summary>
    /// Показать выбор режима
    /// </summary>
    public void ShowModes()
    {
        if (modesShowed)
        {
            HideModes();
        }
        else
        {
            ///создадим префабы кнопок
            CreateModes();
            modesShowed = true;
        }
    }

    /// <summary>
    /// Скрыть выбор режима
    /// </summary>
    public void HideModes()
    {
        //пряем кнопки и уничтожаем их
        for (int i = 0; i < 3; i++)
        {
            transform.parent.GetChild(i).GetComponent<Animator>().SetTrigger("Hide");
            Destroy(transform.parent.GetChild(i).gameObject, 0.5f);
        }

        modesShowed = false;
    }

    /// <summary>
    /// Спрятать все кнопки
    /// </summary>
    public void HideAll()
    {
        HideModes();
        HideSelectButton();
        Destroy(gameObject, 0.75f);
    }

    /// <summary>
    /// Создание кнопок выбора режима
    /// </summary>
    public void CreateModes()
    {
        MenuNavigation navigator = GameObject.Find("Menu Navigator").GetComponent<MenuNavigation>();

        ///создание карточек
        GameObject buttonCards = Instantiate(Resources.Load("Button Cards"), transform.parent) as GameObject;
        buttonCards.transform.SetAsFirstSibling();

        //кнопка действия для карточек
        buttonCards.GetComponent<Button>().onClick.AddListener(() =>
        buttonCards.GetComponent<SelectCards>().OpenGame(navigator.game));

        ///создание test 1
        GameObject buttonTest1 = Instantiate(Resources.Load("Button Test 1"), transform.parent) as GameObject;
        buttonTest1.transform.SetSiblingIndex(1);

        //кнопка действия для тест 1
        buttonTest1.GetComponent<Button>().onClick.AddListener(() =>
        buttonTest1.GetComponent<SelectTest1>().OpenGame(navigator.game));

        //создание test 2
        GameObject buttonTest2 = Instantiate(Resources.Load("Button Test 2"), transform.parent) as GameObject;
        buttonTest2.transform.SetSiblingIndex(2);

        //кнопка действия для тест 2
        buttonTest2.GetComponent<Button>().onClick.AddListener(() =>
        buttonTest2.GetComponent<SelectTest2>().OpenGame(navigator.game));
    }
}
