using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public enum TypeOfClient { justClient, thief, user, userAfterScene, thiefOnCams }
    public TypeOfClient typeOfClient;

    public GameObject visualCue;

    public float speed;
    public Vector2[] points;
    private Vector2 position;
    private int currentPos;

    public int chanseForRobber;
    public int chanseForRobberWithIlligalSubst;
    public int chanseForRobberOnCams;
    public bool canMove;
    public float stopTimer;
    public bool canTimer;
    private SpriteRenderer spriteRenderer;

    public bool dialogueJustNow;
    public bool playerInRange;
    public TextAsset dialogueWithOrdinaryThief;
    public TextAsset dialogueWithUser;
    public TextAsset userDialogAndPolice;
    public TextAsset userDialogWihoutPolice;
    public TextAsset dialWithThiefOnCams;
    private DialogueManager dialogueManager;
    private moneyCounter moneyCount;
    private bool talkOnlyOnce;

    public MiniGames miniGames;

    private void Start()
    {
        talkOnlyOnce = true;
        canMove = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogueManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueManager>();
        moneyCount = GameObject.FindGameObjectWithTag("moneyCounter").GetComponent<moneyCounter>();
        miniGames = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MiniGames>();

        WhatsTypeOfClient();
        stopTimer = 0.7f;
        if (chanseForRobber == 8) spriteRenderer.color = Color.red;
        else if (chanseForRobberWithIlligalSubst == 20) spriteRenderer.color = Color.black;
        else if (miniGames.thiefOnCams)
        {
            typeOfClient = TypeOfClient.thiefOnCams;
            spriteRenderer.color = miniGames.randomColor;
            miniGames.thiefOnCams = false;
        }
        else if (typeOfClient == TypeOfClient.userAfterScene) spriteRenderer.color = Color.black;
        else if(typeOfClient == TypeOfClient.justClient) spriteRenderer.color = Color.green;
        canTimer = false;
        playerInRange = false;
    }

    private void FixedUpdate()
    {
        if (canMove && typeOfClient != TypeOfClient.userAfterScene)
        {
            if (position == points[currentPos] && currentPos < points.Length - 1) currentPos++;
            else if (position == points[points.Length - 1]) Destroy(gameObject);
            transform.position = Vector2.MoveTowards(transform.position, points[currentPos], speed);
        }
        if(typeOfClient == TypeOfClient.thief)
        {
            if (canTimer) stopTimer -= Time.deltaTime;

            if (stopTimer <= 0)
            {
                canMove = false;
            }

            if (dialogueJustNow)
            {
                if (dialogueManager.dialogueText.text == "You: Okay, you can go, just don't forget anything in your pockets/backpack anymore" && position != points[points.Length - 1])
                {
                    dialogueManager.dialogueIsPlaying = false;
                    dialogueManager.dialoguePanel.SetActive(false);
                    spriteRenderer.color = Color.blue;
                    canTimer = false;
                    stopTimer = 1;
                    canMove = true;
                    visualCue.SetActive(false);
                    dialogueJustNow = false;
                    dialogueManager.dialogueText.text = "";
                    moneyCount.numberUAH += 5;
                }
                else if (dialogueManager.dialogueText.text == "You: I still have to call the police")
                {
                    dialogueManager.dialogueIsPlaying = false;
                    dialogueManager.dialoguePanel.SetActive(false);
                    spriteRenderer.color = Color.cyan;
                    canTimer = false;
                    stopTimer = 1;
                    canMove = true;
                    visualCue.SetActive(false);
                    dialogueJustNow = false;
                    dialogueManager.dialogueText.text = "";
                    moneyCount.numberUAH += 5;
                }
                else if (dialogueManager.dialogueText.text == "\"Client run away\"")
                {
                    PlayerPrefs.SetInt("money", moneyCount.numberUAH+=5);
                    PlayerMove.player.SavePlayer();
                    SceneManager.LoadScene(1);
                }
            }
        }

        if(typeOfClient == TypeOfClient.user)
        {
            if (dialogueJustNow)
            {
                if (dialogueManager.dialogueText.text == "Client: ...")
                {
                    int i = UnityEngine.Random.Range(1, 5);
                    if (i != 1)
                    {
                        PlayerPrefs.SetInt("money", moneyCount.numberUAH+=5);
                        PlayerMove.player.SavePlayer();
                        SceneManager.LoadScene(2);
                    }

                    else if (i == 1)
                    {
                        PlayerPrefs.SetInt("money", moneyCount.numberUAH+=5);
                        PlayerMove.player.SavePlayer();
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }

        if(typeOfClient == TypeOfClient.userAfterScene)
        {
            if(dialogueManager.dialogueText.text == "You: I need to call the police")
            {
                spriteRenderer.color = Color.gray;
                canMove = true;
                dialogueManager.dialoguePanel.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
                dialogueManager.dialogueIsPlaying = false;
                moneyCount.numberUAH += 5;
            }
            else if(dialogueManager.dialogueText.text == "You: Okay, now you can go")
            {
                spriteRenderer.color = Color.white;
                canMove = true;
                dialogueManager.dialoguePanel.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
                dialogueManager.dialogueIsPlaying = false;
                moneyCount.numberUAH += 5;
            }

            if(canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, points[points.Length - 1], speed);               
                if (position == points[points.Length - 1]) Destroy(gameObject);
            }
        }

        if (typeOfClient == TypeOfClient.thiefOnCams)
        {
            if (dialogueManager.dialogueText.text == "You: You are under arrest" || dialogueManager.dialogueText.text == "You: I need to call the police")
            {
                dialogueManager.dialogueIsPlaying = false;
                dialogueManager.dialoguePanel.SetActive(false);
                spriteRenderer.color = Color.cyan;
                canMove = true;
                visualCue.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
                moneyCount.numberUAH += 5;
            }
            else if (dialogueManager.dialogueText.text == "You: OK, you can go")
            {
                dialogueManager.dialogueIsPlaying = false;
                dialogueManager.dialoguePanel.SetActive(false);
                spriteRenderer.color = Color.cyan;
                canMove = true;
                visualCue.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
            }
        }

        position = transform.position;
    }

    private void Update()
    {
        if(typeOfClient == TypeOfClient.thief && talkOnlyOnce)
        {
            if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                visualCue.SetActive(true);
                dialogueJustNow = true;
                if (Input.GetKeyDown(KeyCode.E) )
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogueWithOrdinaryThief);
                    talkOnlyOnce = false;
                }
            }
            else if (chanseForRobber == 8)
            {
                visualCue.SetActive(false);
            }
        }

        if(typeOfClient == TypeOfClient.user && talkOnlyOnce)
        {
            if (chanseForRobberWithIlligalSubst == 20 && !canMove && playerInRange)
            {
                visualCue.SetActive(true);
                dialogueJustNow = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogueWithUser);
                    talkOnlyOnce = false;
                }
            }
            else if (chanseForRobberWithIlligalSubst == 20)
            {
                visualCue.SetActive(false);
            }
        }

        if (typeOfClient == TypeOfClient.thiefOnCams && talkOnlyOnce)
        {
            if (playerInRange)
            {
                visualCue.SetActive(true);
                dialogueJustNow = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    canMove = false;
                    DialogueManager.GetInstance().EnterDialogueMode(dialWithThiefOnCams);
                    talkOnlyOnce = false;
                }
            }
            else
            {
                visualCue.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(typeOfClient == TypeOfClient.thief)
        {
            if (collision.gameObject.tag == "Exit")
            {
                canTimer = true;
            }

            if (collision.gameObject.tag == "Player" && !canMove)
            {
                playerInRange = true;
            }

        }
        if(typeOfClient == TypeOfClient.user)
        {
            if (collision.gameObject.tag == "Dog Trigger")
            {
                canMove = false;
            }
        }
        
        if(typeOfClient == TypeOfClient.thiefOnCams)
        {
           if (collision.gameObject.tag == "Player")
           {
               playerInRange = true;
           } 
        }       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !canMove)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !canMove)
        {
            playerInRange = false;
        }
    }

    private void WhatsTypeOfClient()
    {
        typeOfClient = TypeOfClient.justClient;
        chanseForRobber = UnityEngine.Random.Range(7, 8 + 1);
        if(chanseForRobber == 8) typeOfClient = TypeOfClient.thief;
        if (chanseForRobber == 8 && PlayerPrefs.GetInt("level_2", 1) == PlayerPrefs.GetInt("price_2", 0))
        {
            chanseForRobberWithIlligalSubst = UnityEngine.Random.Range(0, 20 + 1);
            if (chanseForRobberWithIlligalSubst == 20)
            {
                chanseForRobber = 0;
                typeOfClient = TypeOfClient.user;
            }
        }
        if(chanseForRobber == 8 && PlayerPrefs.GetInt("level_5", 1) == PlayerPrefs.GetInt("price_5", 0))
        {
            chanseForRobberOnCams = UnityEngine.Random.Range(0, 10 + 1);
            if (chanseForRobberOnCams == 10)
            {
                miniGames.warningThiefOnCams = true;
                Destroy(gameObject);
            }
        }

        if(PlayerPrefs.GetInt("isWithPolice", 0) == 1)
        {
            canMove = false;
            typeOfClient = TypeOfClient.userAfterScene;
            DialogueManager.GetInstance().EnterDialogueMode(userDialogAndPolice);
            PlayerPrefs.SetInt("isWithPolice", 0);
        }
        else if(PlayerPrefs.GetInt("isWithPolice", 0) == 2)
        {
            canMove = false;
            typeOfClient = TypeOfClient.userAfterScene;
            DialogueManager.GetInstance().EnterDialogueMode(userDialogWihoutPolice);
            PlayerPrefs.SetInt("isWithPolice", 0);
        }
    }
}
