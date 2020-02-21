using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerTapNO : MonoBehaviour
{
    public void Tap()
    {
        Test2 test2 = transform.parent.transform.parent.transform.parent.GetComponent<Test2>();

        //проверим, верный ли мы определили перевод
        if (test2.gameTranslate.text == test2.wordTranslate)
        {
            //красный свет, ответ не верный
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Error");

            //следующее слово
            test2.NextWord();
        }
        else//ошибка выбора
        {
            //зеленый свет, ответ верный
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Good");

            //следующее слово
            test2.NextWord();
        }
    }

    private void Start()
    {
        //задаем кнопкам действие
        GetComponent<Button>().onClick.AddListener(() => GetComponent<AnswerTapNO>().Tap());
    }
}