using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneWithUser : MonoBehaviour
{
    public GameObject subst;
    public RectTransform substTransform;
    private Image substSprite;
    public Vector2[] spawnForSubst;
    public Vector2[] targetForSubst;
    public int randomInt;
    public int speed;

    public bool foundIt;
    public bool itMoving;

    public GameObject panel;

    private void Start()
    {
        randomInt = UnityEngine.Random.Range(0, 9);
        substSprite = subst.GetComponent<Image>();
        substSprite.color = new Color(1, 1, 1, 0);
        substTransform.localPosition = spawnForSubst[randomInt];

    }
    private void FixedUpdate()
    {
        if(foundIt)
        {
            for(int i = 0, j = 2; i<2; i++, j--)
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<RectTransform>()[i].SetSiblingIndex(j);
            }
            substSprite.color = new Color(1, 1, 1, 1);
            foundIt = false;

        }

        if(itMoving)
        {
            substTransform.localPosition = Vector2.MoveTowards(substTransform.localPosition, targetForSubst[randomInt], speed);
        }
        
        if(new Vector2(substTransform.localPosition.x, substTransform.localPosition.y) == targetForSubst[randomInt])
        {
            panel.SetActive(true);
        }
    }

    public void clickOnSubst()
    {
        foundIt = true;
        itMoving = true;
    }

    public void backWithPlus()
    {
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) + 5);
        PlayerPrefs.SetInt("isWithPolice", 1);
        SceneManager.LoadScene(0);
    }

    public void backWithoutPlus()
    {
        PlayerPrefs.SetInt("isWithPolice", 2);
        SceneManager.LoadScene(0);
    }

    public void ClickButton(GameObject but)
    {
        but.SetActive(false);
    }
}
