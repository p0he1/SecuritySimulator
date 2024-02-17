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
    //public Queue<GameObject> waitQueue = new Queue<GameObject>();

    private void Start()
    { 
        warningPanel.SetActive(false);
        if (PlayerPrefs.GetInt("isWithPolice", 0) != 0)
        {
            GameObject newNPC = Instantiate(NPC, spawnForUser, Quaternion.identity, null);
        }
        newTimer = true;
        radio.SetActive(PlayerPrefs.GetInt("level_5", 0) == PlayerPrefs.GetInt("price_5", 1));
        dog.SetActive(PlayerPrefs.GetInt("level_2", 0) == PlayerPrefs.GetInt("price_2", 1));
    }

    private void FixedUpdate()
    {
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
}