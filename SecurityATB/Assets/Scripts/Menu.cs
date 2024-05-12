using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject setPanel;
    public GameObject skillPanel;
    public GameObject deleteAllPanel;
    private moneyCounter moneyCount;

    public NPCSpawnPoint spawn;
    private QueueManager queueManager;

    private void Start()
    {
        queueManager = GameObject.FindGameObjectWithTag("Queue Manager").GetComponent<QueueManager>();
        moneyCount = GameObject.FindGameObjectWithTag("moneyCounter").GetComponent<moneyCounter>();
    }

    public void Settings()
    {        
        if(!setPanel.activeSelf)
        {
            foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            {
                npc.GetComponent<NPC>().canMove = false;
            }
            spawn.canTimer = false;
            queueManager.canTimer = false;
            setPanel.SetActive(true);
        }
        else
        {
            foreach(GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            {
                npc.GetComponent<NPC>().canMove = true;
            }
            //npc.speed = 0.05f;
            spawn.canTimer = true;
            queueManager.canTimer = true;
            setPanel.SetActive(false);
        }
    }

    public void Shop()
    {
        if (setPanel.activeSelf) return;
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = false;
        }
        spawn.canTimer = false;
        queueManager.canTimer = false;
        skillPanel.SetActive(true);
    }
    public void exitShop()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = true;
        }
        spawn.canTimer = true;
        queueManager.canTimer = true;
        skillPanel.SetActive(false);
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
}
