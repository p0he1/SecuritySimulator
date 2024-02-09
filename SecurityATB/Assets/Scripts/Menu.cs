using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject setPanel;
    public GameObject skillPanel;
    public GameObject deleteAllPanel;

    public NPCSpawnPoint spawn;

    public void Settings()
    {        
        if(!setPanel.activeSelf)
        {
            foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            {
                npc.GetComponent<NPC>().canMove = false;
            }
            spawn.canTimer = false;
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
            setPanel.SetActive(false);
        }
    }

    public void Shop()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = false;
        }
        spawn.canTimer = false;
        skillPanel.SetActive(true);
    }
    public void exitShop()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npc.GetComponent<NPC>().canMove = true;
        }
        spawn.canTimer = true;
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
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0));
    }
}
