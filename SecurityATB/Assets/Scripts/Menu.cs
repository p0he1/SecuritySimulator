using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject setPanel;
    public GameObject skillPanel;
    public GameObject deleteAllPanel;
    private moneyCounter moneyCount;

    public NPCSpawnPoint spawn;
    private QueueManager queueManager;
    public GameObject joystick;

    public bool skillTreeActive;

    [SerializeField] AudioListener playerEar;
    public Toggle soundToggle;
    public bool isSoundOn;

    private void Start()
    {
        if (PlayerPrefs.GetInt("isSoundOn", 1) == 1)
        {
            AudioListener.pause = false;
            soundToggle.isOn = true;
        }
        else
        {
            AudioListener.pause = true;
            soundToggle.isOn = false;
        }
        queueManager = GameObject.FindGameObjectWithTag("Queue Manager").GetComponent<QueueManager>();
        moneyCount = GameObject.FindGameObjectWithTag("moneyCounter").GetComponent<moneyCounter>();
    }

    private void FixedUpdate() => soundToggle.onValueChanged.AddListener(delegate { ToggleClicked(); });

    public void Settings()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = false;
            npc.GetComponent<NPC>().isTimeOn = false;
        }
        spawn.canTimer = false;
        queueManager.canTimer = false;
        setPanel.SetActive(true);
        joystick.SetActive(false);
    }

    public void CloseSettings()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = true;
            npc.GetComponent<NPC>().isTimeOn = true;
        }
        //npc.speed = 0.05f;
        spawn.canTimer = true;
        queueManager.canTimer = true;
        setPanel.SetActive(false);
        joystick.SetActive(true);
    }

    public void Shop()
    {

        if (setPanel.activeSelf) return;
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = false;
            npc.GetComponent<NPC>().isTimeOn = false;
        }
        spawn.canTimer = false;
        queueManager.canTimer = false;
        skillPanel.SetActive(true);
        joystick.SetActive(false);
        skillTreeActive = true;
    }
    public void exitShop()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = true;
            npc.GetComponent<NPC>().isTimeOn = true;
        }
        spawn.canTimer = true;
        queueManager.canTimer = true;
        skillPanel.SetActive(false);
        joystick.SetActive(true);
    }

    public void deleteAllChoisePanel()
    {
        deleteAllPanel.SetActive(true);
    }

    public void deleteAllYes()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void deleteAllNo()
    {
        deleteAllPanel.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerMove.player.SavePlayer();
        PlayerPrefs.SetInt("money", moneyCount.numberUAH);
    }

    /*public void TurnOffSound(bool variable)
    {
        AudioListener.pause = variable;
    }*/

    public void ToggleClicked()
    {
        //isSoundOn = soundToggle.isOn;
        AudioListener.pause = !soundToggle.isOn;
        PlayerPrefs.SetInt("isSoundOn", soundToggle.isOn ? 1 : 0);
    }
}
