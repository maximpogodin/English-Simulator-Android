using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public List<Transform> keys;
    public int letter;
    public string wordTask;
    public string wordTranslate;
    public string wordProgress;
    public List<string> en;//список английских слов EN
    public List<string> ru;//список русских слов RU
    public InputField input;//строка ввода
    public Text gameWord;

    private void Start()
    {
        GameObject keyboard = Instantiate(Resources.Load((Language.Get() == "ru") ? "KeyboardRU" : "KeyboardEN"), transform) as GameObject;
        keyboard.transform.SetAsFirstSibling();

        GetKeys();
        gameWord = transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        input = transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<InputField>();
        NextWord();
    }

    /// <summary>
    /// Переход к следующему слову
    /// </summary>
    public void NextWord()
    {
        if (en.Count <= 0 || ru.Count <= 0)
        {
            EndGame();
            return;
        }

        //очистка поля
        input.text = "";

        //input.ActivateInputField();

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

        HighlighOffForAllKeys();
        NextLetter(0);
    }

    /// <summary>
    /// Следующая буква в слове
    /// </summary>
    public void NextLetter(int nextLetter)
    {
        if (WordIsDone())
            NextWord();
        else
        {
            //letter = nextLetter;
            HighlighOffForAllKeys();
            HighlightNeededKeys(wordTranslate, nextLetter);
        }
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
    /// Готово ли слово
    /// </summary>
    public bool WordIsDone()
    {
        if (wordProgress == wordTranslate.ToUpper())
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Подсветка клавиш клавиатуры на основе отображаемого слова с заданного символа
    /// </summary>
    public void HighlightNeededKeys(string word, int index)
    {
        for (int i = index; i < word.Length; i++)
        {
            for (int j = 0; j < keys.Count; j++)
            {
                if (keys[j].gameObject.name == word[i].ToString().ToUpper())
                {
                    keys[j].gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    /// <summary>
    /// Убрать подсветку со всех клавиш
    /// </summary>
    public void HighlighOffForAllKeys()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            keys[i].gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void GetKeys()
    {
        //получим строки клавиатуры
        GameObject keyboard = transform.GetChild(0).gameObject;
        GameObject row1 = keyboard.transform.Find("ROW1").gameObject;
        GameObject row2 = keyboard.transform.Find("ROW2").gameObject;
        GameObject row3 = keyboard.transform.Find("ROW3").gameObject;
        GameObject row4 = keyboard.transform.Find("ROW4").gameObject;

        foreach (Transform key in row1.transform) { keys.Add(key); }
        foreach (Transform key in row2.transform) { keys.Add(key); }
        foreach (Transform key in row3.transform) { keys.Add(key); }
        foreach (Transform key in row4.transform) { keys.Add(key); }
    }
}
