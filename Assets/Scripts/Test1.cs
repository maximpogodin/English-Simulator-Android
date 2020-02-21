using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Test1 : MonoBehaviour
{
    public List<string> en;//список английских слов EN
    public List<string> ru;//список русских слов RU
    public List<Transform> variantsBtn;//список кнопок для вариантов ответа
    public Text gameWord;
    string wordTask;
    public string wordTranslate;
    public string[] temp;//временный список слов для формирования вариантов ответа

    void Start()
    {
        GetVariantsBtn();
        gameWord = transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();

        //выбор языка
        temp = (Language.Get() == "ru") ? ru.ToArray() : en.ToArray();

        NextWord();
    }

    /// <summary>
    /// Следующее слово
    /// </summary>
    public void NextWord()
    {
        if (en.Count <= 0 || ru.Count <= 0)
        {
            EndGame();
            return;
        }

        //будем выбирать слова из списков случайным образом без повторений
        //проверим какой выбран язык
        int ind = 0;
        ind = (Language.Get() == "ru") ? Random.Range(0, en.Count - 1) : Random.Range(0, ru.Count - 1);

        wordTask = (Language.Get() == "ru") ? en[ind] : ru[ind];//слово задание
        wordTranslate = (Language.Get() == "ru") ? ru[ind] : en[ind];//слово перевод для задания
        gameWord.text = wordTask;

        //удаляем слово из списков с целью избежания повторений в игре
        ru.RemoveAt(ind);
        en.RemoveAt(ind);

        //отобразим 4 варианта ответа
        DisplayVariants();
    }

    /// <summary>
    /// Конец игры
    /// </summary>
    public void EndGame()
    {
        MenuNavigation.FindObjectOfType<MenuNavigation>().PreviousWindow();
        MenuNavigation.FindObjectOfType<MenuNavigation>().PreviousWindow();

        //удаляем окно
        Destroy(gameObject, 0f);
    }


    /// <summary>
    /// Строит варианты ответа для 4-ёх кнопок
    /// </summary>
    public void DisplayVariants()
    {
        string[] mas = new string[4];//массив невторяющихся слов

        //добавляем сразу правильный ответ в начало
        mas[0] = wordTranslate;

        //добавляем в массив еще 3 слова без повторений
        for (int i = 1; i < mas.Length; i++)
        {
            int idx = Random.Range(0, temp.Length - 1);
            if (!mas.Contains(temp[idx]))
            {
                mas[i] = temp[idx];
            }
            else i--;
        }

        //перемешиваем массив
        Shuffle(mas);

        //выводим варианты на кнопки
        for (int i = 0; i < mas.Length; i++)
        {
            variantsBtn[i].Find("Text").GetComponent<Text>().text = mas[i];
            variantsBtn[i].name = mas[i];
        }
    }

    /// <summary>
    /// Перемешка массива
    /// </summary>
    /// <param name="arr">Массив</param>
    public void Shuffle(string[] arr)
    {
        System.Random rand = new System.Random();

        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);
            string tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }
    }

    public void GetVariantsBtn()
    {
        //получим кнопки для вариантов ответа
        GameObject answerVariants = transform.Find("AnswerVariants").gameObject;
        GameObject row1 = answerVariants.transform.Find("ROW1").gameObject;
        GameObject row2 = answerVariants.transform.Find("ROW2").gameObject;

        //получим список кнопок
        variantsBtn = new List<Transform>();

        foreach (Transform btn in row1.transform) { variantsBtn.Add(btn); }
        foreach (Transform btn in row2.transform) { variantsBtn.Add(btn); }
    }
}
