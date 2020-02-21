using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerTapYES : MonoBehaviour
{
    public void Tap()
    {
        Test2 test2 = transform.parent.transform.parent.transform.parent.GetComponent<Test2>();

        //ответ верный
        if (test2.gameTranslate.text == test2.wordTranslate)
        {
            //зеленый свет
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Good");

            //следующее слово
            test2.NextWord();
        }
        else//ответ не верный
        {
            //красный свет
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Error");

            //следующее слово
            test2.NextWord();
        }
    }

    private void Start()
    {
        //задаем кнопкам действие
        GetComponent<Button>().onClick.AddListener(() => GetComponent<AnswerTapYES>().Tap());
    }
}
