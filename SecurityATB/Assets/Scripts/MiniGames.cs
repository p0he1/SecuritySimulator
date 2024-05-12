using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGames : MonoBehaviour
{
    public Vector2 spawnForUser;
    public GameObject NPC;
    public GameObject dog;
    public bool thiefOnCams;
    public bool warningThiefOnCams;
    public bool canTimer;
    public bool newTimer;
    public float timer;

    public Color randomColor;    
    public GameObject radio;
    public GameObject radioVisalCue;
    public GameObject warningPanel;
    public Image jacketColor;

    public Vector2 lastPointFirstQueue = new Vector2(-2.12f, 2.8f);
    public Vector2 lastPointSecondQueue = new Vector2(1.1f, 2.8f);

    public GameObject cashier1;
    public GameObject cashier2;
    public float frequencyOfCashierAlarm;
    private bool constForRandom;
    private int randomInt;
    public float cashierAlarmTimer;
    private const int maxAlarmTimer = 4;
    public GameObject[] timersFill;
    public GameObject[] emptysToLocateTimers;
    //second alarm needs only for sprite
    //public GameObject stopAlarmButton;
    public GameObject cardScroll;

    private moneyCounter MoneyCounter;

    private void Start()
    {
        MoneyCounter = GameObject.FindGameObjectWithTag("moneyCounter").GetComponent<moneyCounter>();
        foreach (GameObject clock in timersFill) clock.SetActive(false);
        constForRandom = true;
        warningPanel.SetActive(false);
        frequencyOfCashierAlarm = UnityEngine.Random.Range(50, 60 + 1);
        cashierAlarmTimer = 4;
        if (PlayerPrefs.GetInt("isWithPolice", 0) != 0)
        {
            GameObject newNPC = Instantiate(NPC, spawnForUser, Quaternion.identity, null);
            PlayerMove.player.transform.position = new Vector2(spawnForUser.x+1, spawnForUser.y);
        }
        if (PlayerPrefs.GetInt("isCardThief", 0) != 0)
        {
            if(PlayerPrefs.GetInt("1or2queue", 0) == 1)
            {
                GameObject newNPC = Instantiate(NPC, new Vector2(-2.9f, -1.2f), Quaternion.identity, null);
            }
            else if(PlayerPrefs.GetInt("1or2queue", 0) == 2)
            {
                GameObject newNPC = Instantiate(NPC, new Vector2(0.33f, -1.2f), Quaternion.identity, null);
            }
        }
        newTimer = true;
        radio.SetActive(PlayerPrefs.GetInt("level_5", 0) == PlayerPrefs.GetInt("price_5", 1));
        dog.SetActive(PlayerPrefs.GetInt("level_2", 0) == PlayerPrefs.GetInt("price_2", 1));
    }

    private void FixedUpdate()
    {
        //===thief from cameras==
        if (newTimer)
        {
            timer = UnityEngine.Random.Range(2, 3);
            newTimer = false;
        }
        if (warningThiefOnCams && canTimer) timer -= Time.deltaTime;
        if (timer <= 0)
        {
            thiefOnCams = true;
            newTimer = true;
            warningThiefOnCams = false;
            canTimer = false;
        }
        if(warningThiefOnCams) radioVisalCue.SetActive(true);
        else radioVisalCue.SetActive(false);
        //=======================

        //==cash alarm===========
        frequencyOfCashierAlarm -= Time.deltaTime;
        if(frequencyOfCashierAlarm<=0)
        {
            if (constForRandom)
            {
                randomInt = UnityEngine.Random.Range(0, 1 + 1);
                constForRandom = false;
                
            }
            timersFill[randomInt].SetActive(true);
            timersFill[randomInt].GetComponent<Image>().fillAmount = cashierAlarmTimer / maxAlarmTimer;
            cashierAlarmTimer -= Time.deltaTime;
            
            if(Vector2.Distance(PlayerMove.player.transform.position, emptysToLocateTimers[randomInt].transform.position) <= 1 && cashierAlarmTimer > 0)
            {
                cardScroll.SetActive(true);
                //stopAlarmButton.SetActive(true);
            }
            else if(cashierAlarmTimer <= 0)
            {
                timersFill[randomInt].SetActive(false);
                cardScroll.SetActive(false);
                frequencyOfCashierAlarm = UnityEngine.Random.Range(50, 60 + 1);
                cashierAlarmTimer = 4;
                constForRandom = true;
            }
        }
        //=======================
    }

    public void timerOn()
    {       
        if (warningThiefOnCams)
        {
            canTimer = true;
            randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            warningPanel.SetActive(true);
            jacketColor.color = randomColor;
        }
    }

    public void RemoveThePanel()
    {
        warningPanel.SetActive(false);
    }
    
    public void UpdateMiniGames()
    {
        radio.SetActive(PlayerPrefs.GetInt("level_5", 0) == PlayerPrefs.GetInt("price_5", 1));
        dog.SetActive(PlayerPrefs.GetInt("level_2", 0) == PlayerPrefs.GetInt("price_2", 1));
    }

    public void StopAlarmScroll()
    {
        if(cardScroll.GetComponent<Scrollbar>().value == 1)
        {
            MoneyCounter.numberUAH += 5;
            frequencyOfCashierAlarm = UnityEngine.Random.Range(50, 60 + 1);
            cashierAlarmTimer = 4;
            constForRandom = true;
            cardScroll.SetActive(false);
            timersFill[randomInt].SetActive(false);
            cardScroll.GetComponent<Scrollbar>().value = 0;
        }       
    }
}