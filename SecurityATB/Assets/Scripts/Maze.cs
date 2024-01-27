using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Maze : MonoBehaviour
{
    public Vector2[] currentPositions;
    public float thiefSpeed;
    public int i;
    private bool canMove;

    public playerInMaze playerInMaze;
    public TextMeshProUGUI conclusionText;
    public GameObject dialoguePanel;

    public bool startOfGame;
    public Button startButton;
    public int money;

    private void Start()
    {
        canMove = true;
        money = PlayerPrefs.GetInt("money");
    }

    private void Update()
    {
        if(!startOfGame)
        {
            return;
        }

        Vector2 position = transform.position;
        if (i < currentPositions.Length)
        {
            if (position != currentPositions[i] && canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPositions[i], thiefSpeed);
            }
            else
            {
                i++;
            }
        }

        if(playerInMaze.flewInBushes == true || playerInMaze.isCoughtUp == true)
        {
            canMove = false;
            dialoguePanel.SetActive(true);
            if(playerInMaze.flewInBushes == true)
            {
                conclusionText.text = "You flew to the bushes";
            }
            else if(playerInMaze.isCoughtUp == true)
            {
                conclusionText.text = "You cought!";
                PlayerPrefs.SetInt("money", money + 5);
            }
        }
    }

    public void backButton()
    {
        SceneManager.LoadScene(0);
    }

    public void StartOfGame()
    {
        if(!startOfGame)
        {
            startOfGame = true;
            playerInMaze.canMove = true;
            startButton.gameObject.SetActive(false);
        }
    }
}
