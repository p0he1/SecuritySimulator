using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public enum TypeOfClient { justClient, thief, user, userAfterScene, thiefOnCams, withWrongCard/*WWC*/,  afterCheckWWC}
    public TypeOfClient typeOfClient;

    public GameObject visualCue;
    public GameObject redVisualCue;
    public GameObject jacket;
    private GameObject speakButton;

    public enum HowWalking {justWalking, waitingQueue}
    public HowWalking howWalking;
    public float speed;
    public int stopOrNotStop;
    //CR - cash register
    public int cr1or2;
    public Vector2[] firstPoints;
    public Vector2[] secondPoints;
    public Vector2[] exits;
    private Vector2 position;
    private int currentPos;
    public float stopTimerCR;
    public bool canTimerCR;
    private Vector3 lastFramePos;

    public int chanseForRobber;
    public int chanseForRobberWithIlligalSubst;
    public int chanseForRobberOnCams;
    public int chanseForWrongCard;
    public bool canMove;
    //MD - magnetic doors
    private float stopTimerMD;
    private bool canTimerMD;
    private SpriteRenderer spriteRenderer;
    private NPCSpawnPoint npcSpawn;
    public int whichSprite;
    private Animator npcAnim;

    public bool dialogueJustNow;
    public bool playerInRange;
    public TextAsset[] dialoguesWithOrdinaryThiefs;
    public TextAsset dialogueWithUser;
    public TextAsset userDialogAndPolice;
    public TextAsset userDialogWihoutPolice;
    public TextAsset dialWithThiefOnCams;
    public TextAsset[] uaDialoguesWithThiefs;
    public TextAsset uaDialogueWithUser;
    public TextAsset uaUserAndPolice;
    public TextAsset uaUserNoPolice;
    public TextAsset uaDialogueThiefOnCams;
    private DialogueManager dialogueManager;
    private moneyCounter moneyCount;
    private bool talkOnlyOnce;

    private MiniGames miniGames;

    private QueueManager queueManager;
    public bool onceInQueue;
    public int orderInQueue;

    public GameObject[] police;
    private bool isArrest;
    private Animator dog;

    private void Start()
    {
        talkOnlyOnce = true;
        canMove = true;
        onceInQueue = true;
        canTimerMD = false;
        playerInRange = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        queueManager = GameObject.FindGameObjectWithTag("Queue Manager").GetComponent<QueueManager>();
        dialogueManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueManager>();
        moneyCount = GameObject.FindGameObjectWithTag("moneyCounter").GetComponent<moneyCounter>();
        miniGames = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MiniGames>();
        npcSpawn = GameObject.FindGameObjectWithTag("NPC start").GetComponent<NPCSpawnPoint>();
        speakButton = GameObject.FindGameObjectWithTag("Speak Button");

        stopOrNotStop = UnityEngine.Random.Range(1, 5 + 1);
        cr1or2 = UnityEngine.Random.Range(1, 2 + 1);
        firstPoints[3] = exits[UnityEngine.Random.Range(0, 1 + 1)];
        secondPoints[3] = exits[UnityEngine.Random.Range(1, 2 + 1)];
        whichSprite = UnityEngine.Random.Range(0, 29 + 1);
        WhatsTypeOfClient();
        stopTimerMD = 0.7f;
        stopTimerCR = 2f;
        lastFramePos = transform.position;
        npcAnim = GetComponent<Animator>();
        if (typeOfClient == TypeOfClient.thiefOnCams || typeOfClient == TypeOfClient.user || typeOfClient == TypeOfClient.userAfterScene)
        {
            npcAnim.runtimeAnimatorController = npcSpawn.npcSpritesForMiniGames[whichSprite].GetComponent<Animator>().runtimeAnimatorController;
            spriteRenderer.sprite = npcSpawn.npcSpritesForMiniGames[whichSprite].GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            spriteRenderer.sprite = npcSpawn.npcSprites[whichSprite].GetComponent<SpriteRenderer>().sprite;
            npcAnim.runtimeAnimatorController = npcSpawn.npcSprites[whichSprite].GetComponent<Animator>().runtimeAnimatorController;
        }
    }

    private void FixedUpdate()
    {
        if (typeOfClient != TypeOfClient.userAfterScene && howWalking == HowWalking.justWalking)
        {
            if (stopOrNotStop != 5 && currentPos == 1) howWalking = HowWalking.waitingQueue;
            if (canMove)
            {
                if(cr1or2 == 1)
                {
                    if (position == firstPoints[currentPos] && currentPos < firstPoints.Length - 1)
                    {
                        currentPos++;
                    }
                    else if (position == firstPoints[firstPoints.Length - 1]) Destroy(gameObject);
                    transform.position = Vector2.MoveTowards(transform.position, firstPoints[currentPos], speed);
                }
                else if(cr1or2 == 2)
                {
                    if (position == secondPoints[currentPos] && currentPos < secondPoints.Length - 1)
                    {
                        currentPos++;
                    }
                    else if (position == secondPoints[secondPoints.Length - 1]) Destroy(gameObject);
                    transform.position = Vector2.MoveTowards(transform.position, secondPoints[currentPos], speed);
                }
            }
        }
        else if(howWalking == HowWalking.waitingQueue)
        {
            spriteRenderer.sortingOrder = orderInQueue * -1;
            if(onceInQueue)
            {
                orderInQueue = queueManager.Enqueue(gameObject.GetComponent<NPC>());
                onceInQueue = false;
            }
            if (position == queueManager.firstQueuePlaces[0] || position == queueManager.secondQueuePlaces[0])
            {
                canTimerCR = true;
            }
            if (canTimerCR)
            {
                stopTimerCR -= Time.deltaTime;
                canMove = false;
                if (stopTimerCR <= 0)
                {
                    if(typeOfClient == TypeOfClient.withWrongCard)
                    {
                        redVisualCue.SetActive(true);
                    }
                    else
                    {
                        canMove = true;
                        queueManager.Dequeue(gameObject.GetComponent<NPC>());
                        howWalking = HowWalking.justWalking;
                        currentPos = 2;
                        canTimerCR = false;
                        return;
                    }
                }
            }
            if(canMove)
            {
                if (cr1or2 == 1) transform.position = Vector2.MoveTowards(transform.position, queueManager.firstQueuePlaces[orderInQueue], speed);
                else if (cr1or2 == 2) transform.position = Vector2.MoveTowards(transform.position, queueManager.secondQueuePlaces[orderInQueue], speed);
            }
        }

        if(typeOfClient == TypeOfClient.thief)
        {
            if (canTimerMD) stopTimerMD -= Time.deltaTime;

            if (stopTimerMD <= 0)
            {
                canMove = false;
            }

            if (dialogueJustNow)
            {
                if (dialogueManager.dialogueText.text == "You: Okay, you can go, just don't forget anything in your pockets/backpack anymore"
                    || dialogueManager.dialogueText.text == "You: Understood, you are free to leave, but please be mindful not to leave anything behind in your pockets or backpack."
                    || dialogueManager.dialogueText.text == "You: OK, you may go, but please remember not to leave anything in your pockets or backpack."
                    || dialogueManager.dialogueText.text == "Ви: Добре, ви можете йти, тільки не забувайте нічого в ваших кишенях"
                    || dialogueManager.dialogueText.text == "Ви: Добре, ви можете йти, тільки не забувайте більше нічого у себе."
                    || dialogueManager.dialogueText.text == "Ви: Зрозуміло, ви можете йти, але не залишайте нічого більше в рюкзаку.")
                {
                    dialogueManager.dialogueIsPlaying = false;
                    dialogueManager.dialoguePanel.SetActive(false);
                    canTimerMD = false;
                    stopTimerMD = 1;
                    canMove = true;
                    visualCue.SetActive(false);
                    dialogueJustNow = false;
                    dialogueManager.dialogueText.text = "";
                    moneyCount.numberUAH += 5;
                }
                else if (dialogueManager.dialogueText.text == "You: I still have to call the police"
                    || dialogueManager.dialogueText.text == "You: I still must inform the police."
                    || dialogueManager.dialogueText.text == "I still have to contact the police."
                    || dialogueManager.dialogueText.text == "Ви: Я все ще маю викликати поліцію"
                    || dialogueManager.dialogueText.text == "Ви: Я все ще маю проінформувати поліцію."
                    || dialogueManager.dialogueText.text == "Ви: Я все ще маю зконтактувати з поліцією")
                {
                    dialogueManager.dialogueIsPlaying = false;
                    dialogueManager.dialoguePanel.SetActive(false);
                    canTimerMD = false;
                    stopTimerMD = 1;
                    canMove = true;
                    visualCue.SetActive(false);
                    dialogueJustNow = false;
                    dialogueManager.dialogueText.text = "";
                    moneyCount.numberUAH += 5;
                    isArrest = true;
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
                if (dialogueManager.dialogueText.text == "Client: ..."
                    || dialogueManager.dialogueText.text == "Клієнт: ...")
                {
                    int i = UnityEngine.Random.Range(1, 5);
                    if (i != 1)
                    {
                        PlayerPrefs.SetInt("money", moneyCount.numberUAH+=5);
                        //we must spawn player behind user
                        PlayerPrefs.SetFloat("x", miniGames.spawnForUser.x+1);
                        PlayerPrefs.SetFloat("y", miniGames.spawnForUser.y);
                        PlayerPrefs.SetFloat("z", transform.position.z);
                        SceneManager.LoadScene(2);
                    }

                    else if (i == 1)
                    {
                        PlayerPrefs.SetInt("money", moneyCount.numberUAH+=5);
                        //we must spawn player behind user
                        PlayerPrefs.SetFloat("x", miniGames.spawnForUser.x + 1);
                        PlayerPrefs.SetFloat("y", miniGames.spawnForUser.y);
                        PlayerPrefs.SetFloat("z", transform.position.z);
                        SceneManager.LoadScene(1);
                    }
                }
            }
        }

        if(typeOfClient == TypeOfClient.userAfterScene)
        {
            if(dialogueManager.dialogueText.text == "You: I need to call the police"
                || dialogueManager.dialogueText.text == "Ви: Я маю викликати поліцію.")
            {
                isArrest = true;
                canMove = true;
                dialogueManager.dialoguePanel.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
                dialogueManager.dialogueIsPlaying = false;
                moneyCount.numberUAH += 5;
            }
            else if(dialogueManager.dialogueText.text == "You: Okay, now you can go"
                || dialogueManager.dialogueText.text == "Ви: Добре, тепер ви можете йти.")
            {
                canMove = true;
                dialogueManager.dialoguePanel.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
                dialogueManager.dialogueIsPlaying = false;
                moneyCount.numberUAH += 5;
            }

            if(canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, firstPoints[firstPoints.Length - 1], speed);               
                if (position == firstPoints[firstPoints.Length - 1]) Destroy(gameObject);
            }
        }

        if (typeOfClient == TypeOfClient.thiefOnCams)
        {
            if (dialogueManager.dialogueText.text == "You: You are under arrest" || dialogueManager.dialogueText.text == "You: I need to call the police"
                || dialogueManager.dialogueText.text == "Ви: Ви під арештом." || dialogueManager.dialogueText.text == "Ви: Я маю викликати поліцію.")
            {
                dialogueManager.dialogueIsPlaying = false;
                dialogueManager.dialoguePanel.SetActive(false);
                isArrest = true;
                canMove = true;
                visualCue.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
                moneyCount.numberUAH += 5;
            }
            else if (dialogueManager.dialogueText.text == "You: OK, you can go" || dialogueManager.dialogueText.text == "Ви: Добре, ви можете йти.")
            {
                dialogueManager.dialogueIsPlaying = false;
                dialogueManager.dialoguePanel.SetActive(false);
                canMove = true;
                visualCue.SetActive(false);
                dialogueJustNow = false;
                dialogueManager.dialogueText.text = "";
            }
        }

        if (isArrest) underArrest();

        if (transform.position != lastFramePos)
        {
            npcAnim.Play("walk");

            if (transform.position.x > lastFramePos.x) spriteRenderer.flipX = false;
            else if (transform.position.x < lastFramePos.x) spriteRenderer.flipX = true;

            lastFramePos = transform.position;            
        }
        else npcAnim.Play("idle");

        position = transform.position;
    }

    private void Update()
    {
        if(typeOfClient == TypeOfClient.thief && talkOnlyOnce)
        {
            if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                visualCue.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    dialogueJustNow = true;
                    talkOnlyOnce = false;
                    DialogueManager.GetInstance().EnterDialogueMode(PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? dialoguesWithOrdinaryThiefs[UnityEngine.Random.Range(0, 2 + 1)] : uaDialoguesWithThiefs[UnityEngine.Random.Range(0, 2 + 1)]);
                }
            }
            else if (chanseForRobber == 8)
            {
                visualCue.SetActive(false);
            }
        }

        if(typeOfClient == TypeOfClient.user && talkOnlyOnce)
        {
            if (!canMove && playerInRange)
            {
                visualCue.SetActive(true);
                dialogueJustNow = true;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    DialogueManager.GetInstance().EnterDialogueMode(PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? dialogueWithUser : uaDialogueWithUser);
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
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    canMove = false;
                    DialogueManager.GetInstance().EnterDialogueMode(PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? dialWithThiefOnCams : uaDialogueThiefOnCams);
                    talkOnlyOnce = false;
                }
            }
            else
            {
                visualCue.SetActive(false);
            }
        }

        if (typeOfClient == TypeOfClient.withWrongCard)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange)
                {
                PlayerPrefs.SetInt("numberOfSprite", whichSprite);
                if (position == queueManager.firstQueuePlaces[0]) PlayerPrefs.SetInt("1or2queue", 1);
                if (position == queueManager.secondQueuePlaces[0]) PlayerPrefs.SetInt("1or2queue", 2);
                redVisualCue.SetActive(true);
                PlayerPrefs.SetInt("money", moneyCount.numberUAH);
                PlayerMove.player.SavePlayer();
                SceneManager.LoadScene(3);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(typeOfClient == TypeOfClient.thief && howWalking != HowWalking.waitingQueue)
        {
            if (collision.gameObject.tag == "Exit")
            {
                canTimerMD = true;
            }

            if (collision.gameObject.tag == "Player" && !canMove)
            {
                playerInRange = true;
            }

        }
        if(typeOfClient == TypeOfClient.user && howWalking != HowWalking.waitingQueue)
        {
            if (collision.gameObject.tag == "Dog Trigger")
            {
                canMove = false;
                dog.Play("angry");
            }
            if (!canMove && collision.gameObject.tag == "Player") playerInRange = true;
        }
        if (typeOfClient == TypeOfClient.thiefOnCams)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInRange = true;
            }
        }
        if(typeOfClient == TypeOfClient.withWrongCard && redVisualCue.activeSelf)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInRange = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (typeOfClient == TypeOfClient.thief)
        {
            if (collision.gameObject.tag == "Player" && !canMove && howWalking != HowWalking.waitingQueue)
            {
                playerInRange = true;
            }
        }
        if (typeOfClient == TypeOfClient.user && howWalking != HowWalking.waitingQueue)
        {
            if (collision.gameObject.tag == "Dog Trigger")
            {
                canMove = false;
            }
            if (!canMove && collision.gameObject.tag == "Player") playerInRange = true;
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
        if (PlayerPrefs.GetInt("isWithPolice", 0) == 1)
        {
            canMove = false;
            typeOfClient = TypeOfClient.userAfterScene;
            whichSprite = 1;
            DialogueManager.GetInstance().EnterDialogueMode(PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? userDialogAndPolice : uaUserAndPolice);
            PlayerPrefs.SetInt("isWithPolice", 0);
            return;
        }
        else if (PlayerPrefs.GetInt("isWithPolice", 0) == 2)
        {
            canMove = false;
            whichSprite = 1;
            typeOfClient = TypeOfClient.userAfterScene;
            DialogueManager.GetInstance().EnterDialogueMode(PlayerPrefs.GetInt("LocaleKey", 0) == 0 ? userDialogWihoutPolice : uaUserNoPolice);
            PlayerPrefs.SetInt("isWithPolice", 0);
            return;
        }

        if (PlayerPrefs.GetInt("isCardThief", 0) == 1)
        {
            PlayerPrefs.SetInt("isCardThief", 0);
            transform.position = queueManager.secondQueuePlaces[0];
            isArrest = true;
            whichSprite = PlayerPrefs.GetInt("numberOfSprite", 0);
            typeOfClient = TypeOfClient.afterCheckWWC;
            currentPos = 3;
            return;
        }
        else if (PlayerPrefs.GetInt("isCardThief", 0) == 2)
        {
            PlayerPrefs.SetInt("isCardThief", 0);
            whichSprite = PlayerPrefs.GetInt("numberOfSprite", 0);
            typeOfClient = TypeOfClient.afterCheckWWC;
            currentPos = 3;
            return;
        }


        typeOfClient = TypeOfClient.justClient;
        chanseForRobber = UnityEngine.Random.Range(0, 8 + 1);
        if(chanseForRobber == 8) typeOfClient = TypeOfClient.thief;
        if (chanseForRobber == 8 && PlayerPrefs.GetInt("level_2", 1) == PlayerPrefs.GetInt("price_2", 0))
        {
            chanseForRobberWithIlligalSubst = UnityEngine.Random.Range(0, 20 + 1);
            if (chanseForRobberWithIlligalSubst == 20)
            {
                chanseForRobber = 0;
                whichSprite = 1;
                typeOfClient = TypeOfClient.user;
                dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Animator>();
                return;
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
        if(PlayerPrefs.GetInt("level_10", 1) == PlayerPrefs.GetInt("price_10", 0) && PlayerPrefs.GetInt("level_11", 1) == PlayerPrefs.GetInt("price_11", 0)) 
        chanseForWrongCard = UnityEngine.Random.Range(0, 15 + 1);
        if (chanseForWrongCard == 15)
        {
            typeOfClient = TypeOfClient.withWrongCard;
            return;
        }

        if (miniGames.thiefOnCams)
        {
            typeOfClient = TypeOfClient.thiefOnCams;
            jacket.SetActive(true);
            jacket.GetComponent<SpriteRenderer>().color = miniGames.randomColor;
            miniGames.thiefOnCams = false;
            whichSprite = 0;
            return;
        }
    }

    private void underArrest()
    {
        foreach(GameObject cop in police)
        {
            cop.SetActive(true);
            if(transform.position != lastFramePos)
            {
                cop.GetComponent<Animator>().Play("walk");
            }
            else
            {
                cop.GetComponent<Animator>().Play("idle");
            }
        }
    }

/*    public void TurnOnDial()
    {
        dialogueJustNow = true;
        DialogueManager.GetInstance().EnterDialogueMode(dialoguesWithOrdinaryThiefs[UnityEngine.Random.Range(0, 2 + 1)]);
        talkOnlyOnce = false;
    }*/
}