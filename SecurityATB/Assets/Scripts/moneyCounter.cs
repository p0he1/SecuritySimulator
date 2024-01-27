using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moneyCounter : MonoBehaviour
{
    public int numberUAH;
    public TextMeshProUGUI numberTextUAH;

    private void Start()
    {
        numberUAH = PlayerPrefs.GetInt("money", 0);
    }

    private void FixedUpdate()
    {
        numberTextUAH.text = numberUAH.ToString();

        if (DialogueManager.GetInstance().plus5)
        {
            numberUAH += 5;
            DialogueManager.GetInstance().plus5 = false;
        }
    }
}
