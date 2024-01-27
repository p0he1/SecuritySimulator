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
        LoadPlayer();
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        if(IsPlayerOvertakeThief)
        {
            Debug.Log("p");
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
        PlayerPrefs.SetFloat("x", transform.position.x);
        PlayerPrefs.SetFloat("y", transform.position.y);
        PlayerPrefs.SetFloat("z", transform.position.z);
    }

    public void LoadPlayer()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
    }
}