using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject visualCue;

    public float speed;
    public Vector2[] points;
    private Vector2 position;
    private int currentPos;

    public int chanseForRobber;
    public bool canMove = true;
    public float stopTimer;
    public bool canTimer;
    private SpriteRenderer spriteRenderer;

    public bool dialogue;
    public bool playerInRange;
    public TextAsset inkJSON;
    private DialogueManager dialogueManager;

    private void Start()
    {
        chanseForRobber = UnityEngine.Random.Range(4, 8 + 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        dialogueManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueManager>();

        stopTimer = 0.7f;
        spriteRenderer.color = chanseForRobber == 8 ? Color.red : Color.green;
        canTimer = false;
        playerInRange = false;
    }

    private void FixedUpdate()
    {     
        if (canMove)
        {
            if (position == points[currentPos] && currentPos < points.Length - 1) currentPos++;
            else if (position == points[points.Length - 1]) Destroy(gameObject);
            transform.position = Vector2.MoveTowards(transform.position, points[currentPos], speed);
        }
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

        if (canTimer) stopTimer -= Time.deltaTime;

        if (stopTimer <= 0)
        {
            canMove = false;
        }

        if (chanseForRobber == 8)
        {
            if (dialogueManager.dialogueText.text == "You: Okay, you can go, just don't forget anything in your pockets/backpack anymore")
            {
                dialogueManager.dialogueIsPlaying = false;
                dialogueManager.dialoguePanel.SetActive(false);
                spriteRenderer.color = Color.blue;
                canTimer = false;
                stopTimer = 1;
                canMove = true;
                visualCue.SetActive(false);
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
            }
        }
        
        position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Exit" && chanseForRobber == 8)
        {
            canTimer = true;
        }

        if(collision.gameObject.tag == "Player" && !canMove)
        {
            playerInRange = true;
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
}
