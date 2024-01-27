using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInMaze : MonoBehaviour
{
    public int playerSpeed;
    public bool flewInBushes;
    public bool isCoughtUp;
    public bool canMove;
    public bool plusFive;

    private void Start()
    {
        flewInBushes = false;
        isCoughtUp = false;
        canMove = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), playerSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canMove = false;
        if(collision.gameObject.tag == "bushes")
        {
            flewInBushes = true;
        }

        if(collision.gameObject.tag == "thief")
        {
            isCoughtUp = true;
            plusFive = true;
            PlayerMove.player.IsPlayerOvertakeThief = true;
        }
    }
}
