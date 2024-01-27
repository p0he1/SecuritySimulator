using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject setPanel;
    public GameObject skillPanel;

    public void Settings()
    {        
        if(!setPanel.activeSelf)
        {
            setPanel.SetActive(true);
        }
        else
        {
            setPanel.SetActive(false);
        }
    }

    public void Shop()
    {
        skillPanel.SetActive(true);
        //SkillTree.skillTree.buttonClicked = true;
    }
    public void exitShop()
    {
        skillPanel.SetActive(false);
    }

}
