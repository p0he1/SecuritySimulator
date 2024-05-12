using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove player;

    public float speed;
    private Rigidbody2D rb2d;
    private Vector2 direction;
    private Animator playerAnim;
    private SpriteRenderer playerSP;
    public Joystick joystick;

    private void Awake()
    {        
        player = this;
    }

    private void Start()
    {
        playerSP = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        LoadPlayer();
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        direction.x = joystick.Direction.x;
        direction.y = joystick.Direction.y;
    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + direction * speed * Time.fixedDeltaTime);
        if (direction == Vector2.zero) playerAnim.Play("idle");
        else playerAnim.Play("walk");

        if (direction.x > 0) playerSP.flipX = false;
        else if (direction.x < 0) playerSP.flipX = true;
    }

    public void SavePlayer ()
    {
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);
    }

    public void LoadPlayer()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
    }
}