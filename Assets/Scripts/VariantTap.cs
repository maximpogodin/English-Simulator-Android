using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariantTap : MonoBehaviour
{
    public void Tap()
    {
        Test1 test1 = transform.parent.transform.parent.transform.parent.GetComponent<Test1>();

        //проверим, верный ли мы выбрали вариант ответа
        if (gameObject.name == test1.wordTranslate)
        {
            //зеленый свет
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Good");

            //следующее слово
            test1.NextWord();
        }
        else//ошибка выбора
        {
            //красный свет
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Error");
        }
    }

    private void Start()
    {
        //задаем кнопкам действие
        GetComponent<Button>().onClick.AddListener(() => GetComponent<VariantTap>().Tap());
    }
}
