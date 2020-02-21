using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTap : MonoBehaviour
{
    public void Tap()
    {
        Cards cards = transform.parent.transform.parent.transform.parent.GetComponent<Cards>();

        //проверим, верный ли мы нажали символ
        if (gameObject.name == cards.wordTranslate[cards.input.text.Length].ToString().ToUpper())
        {
            //вписываем символ
            if (gameObject.name == "______")
                cards.input.text += " ";
            else
                cards.input.text += gameObject.name;

            //зеленый свет
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Good");

            //передадим вводимое слово
            cards.wordProgress = cards.input.text;

            //переходим к следующему символу
            cards.NextLetter(cards.input.text.Length);
        }
        else//ошибка ввода
        {
            //красный свет
            GetComponent<Animation>().clip.legacy = true;
            GetComponent<Animation>().Play("Error");
        }
    }

    private void Start()
    {
        //задаем кнопкам вирт. клавиатуры действие
        GetComponent<Button>().onClick.AddListener(() => GetComponent<KeyTap>().Tap());
    }
}
