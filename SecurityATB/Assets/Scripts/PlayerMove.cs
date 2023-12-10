using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove player;

    public float speed;
    private Rigidbody2D rb2d;
    private Vector2 direction;

    public bool IsPlayerOvertakeThief;

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.F7))
        {
            SavePlayer();
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            LoadPlayer();
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + direction * speed * Time.fixedDeltaTime);
    }

    public void SavePlayer ()
    {
        SaveManager.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveManager.LoadPlayer();
        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        transform.position = position;
    }
}