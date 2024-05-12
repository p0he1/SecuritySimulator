using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class sceneWithWrongCard : MonoBehaviour
{
    //FR - for real
    public int chanseFRWrongCard;

    //C - client
    /*    public int nameNumOnCId;
        public int lastNameNumOnCId;    
        public int nameNumOnCard;
        public int lastNameNumOnCard;
        public int clientIdSprite;*/

    public int numberOfNpcSprite;
    public string[] names;
    public string[] lastNames;
    public string[] uaNames;
    public string[] uaLastNames;
    public GameObject[] npcs;

    public Image idPhoto;
    public Image clientPhoto;

    public TextMeshProUGUI nameTextOnCId;
    public TextMeshProUGUI lastNameTextOnCId;
    public TextMeshProUGUI nameTextOnCard;
    public TextMeshProUGUI lastNameTextOnCard;

    public TextMeshProUGUI cardNum;
    public TextMeshProUGUI idCardNum;

    public GameObject panel;
    public TextMeshProUGUI textPanel;

    private void Start()
    {
        chanseFRWrongCard = UnityEngine.Random.Range(0, 1 + 1);
        numberOfNpcSprite = PlayerPrefs.GetInt("numberOfSprite", 0);
        if (chanseFRWrongCard != 1)
        {
            //nameNumOnCard = nameNumOnCId = UnityEngine.Random.Range(0, 9+1);
            //lastNameNumOnCard = lastNameNumOnCId = UnityEngine.Random.Range(0, 9+1);
            idPhoto.sprite = clientPhoto.sprite = npcs[numberOfNpcSprite].GetComponent<SpriteRenderer>().sprite;
            if(PlayerPrefs.GetInt("LocaleKey", 0) == 0)
            {
                if (numberOfNpcSprite < 15)
                {
                    nameTextOnCard.text = nameTextOnCId.text = names[UnityEngine.Random.Range(0, 14 + 1)];
                    lastNameTextOnCard.text = lastNameTextOnCId.text = lastNames[UnityEngine.Random.Range(0, 14 + 1)];
                }
                else if (numberOfNpcSprite >= 15)
                {
                    nameTextOnCard.text = nameTextOnCId.text = names[UnityEngine.Random.Range(15, 29 + 1)];
                    lastNameTextOnCard.text = lastNameTextOnCId.text = lastNames[UnityEngine.Random.Range(15, 29 + 1)];
                }
            }
            else if(PlayerPrefs.GetInt("LocaleKey", 0) == 1)
            {
                if (numberOfNpcSprite < 15)
                {
                    nameTextOnCard.text = nameTextOnCId.text = uaNames[UnityEngine.Random.Range(0, 14 + 1)];
                    lastNameTextOnCard.text = lastNameTextOnCId.text = uaLastNames[UnityEngine.Random.Range(0, 14 + 1)];
                }
                else if (numberOfNpcSprite >= 15)
                {
                    nameTextOnCard.text = nameTextOnCId.text = uaNames[UnityEngine.Random.Range(15, 29 + 1)];
                    lastNameTextOnCard.text = lastNameTextOnCId.text = uaLastNames[UnityEngine.Random.Range(15, 29 + 1)];
                }
            }
            
            
        }
        else
        {
            idPhoto.sprite = npcs[numberOfNpcSprite].GetComponent<SpriteRenderer>().sprite;
            clientPhoto.sprite = npcs[UnityEngine.Random.Range(0, 29 + 1)].GetComponent<SpriteRenderer>().sprite;
            if (PlayerPrefs.GetInt("LocaleKey", 0) == 0)
            {
                if (numberOfNpcSprite < 15)
                {
                    nameTextOnCard.text = names[UnityEngine.Random.Range(0, 14 + 1)];
                    lastNameTextOnCard.text = lastNames[UnityEngine.Random.Range(0, 14 + 1)];
                }
                else if (numberOfNpcSprite >= 15)
                {
                    nameTextOnCard.text = names[UnityEngine.Random.Range(15, 29 + 1)];
                    lastNameTextOnCard.text = lastNames[UnityEngine.Random.Range(15, 29 + 1)];
                }
                nameTextOnCId.text = names[UnityEngine.Random.Range(0, 29 + 1)];
                lastNameTextOnCId.text = lastNames[UnityEngine.Random.Range(0, 29 + 1)];
            }
            else if (PlayerPrefs.GetInt("LocaleKey", 0) == 1)
            {
                if (numberOfNpcSprite < 15)
                {
                    nameTextOnCard.text = uaNames[UnityEngine.Random.Range(0, 14 + 1)];
                    lastNameTextOnCard.text = uaLastNames[UnityEngine.Random.Range(0, 14 + 1)];
                }
                else if (numberOfNpcSprite >= 15)
                {
                    nameTextOnCard.text = uaNames[UnityEngine.Random.Range(15, 29 + 1)];
                    lastNameTextOnCard.text = uaLastNames[UnityEngine.Random.Range(15, 29 + 1)];
                }
                nameTextOnCId.text = uaNames[UnityEngine.Random.Range(0, 29 + 1)];
                lastNameTextOnCId.text = uaLastNames[UnityEngine.Random.Range(0, 29 + 1)];
            }
        }
        cardNum.text = $"**{UnityEngine.Random.Range(1000, 10000)}";
        idCardNum.text = $"{UnityEngine.Random.Range(100000, 999999)}";
    }

    public void RightAnswer()
    {
        panel.SetActive(true);
        textPanel.text = PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? "Right answer!\n+5" : "Правильна відповідь!\n+5";
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) + 5);
    }    
    public void WrongAnswer()
    {
        panel.SetActive(true);
        textPanel.text = PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? "Wrong answer!" : "Неправильна відповідь!";
    }  
    public void Arrest()
    {
        PlayerPrefs.SetInt("isCardThief", 1);
        if (chanseFRWrongCard == 1) RightAnswer();
        else WrongAnswer();
    }

    public void Release()
    {
        PlayerPrefs.SetInt("isCardThief", 2);
        if (chanseFRWrongCard != 1) RightAnswer();
        else WrongAnswer();
    }
    
    public void Back()
    {        
        SceneManager.LoadScene(0);
    }
}


