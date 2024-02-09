using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{
    public Vector2[] spawnPoints;
    public GameObject NPC;
    private float timer;
    public float firstNumForRandom;
    public float secondNumForRandom;
    public bool canTimer;

    private void Start()
    {
        timer = Random.Range(firstNumForRandom, secondNumForRandom);//0.5f;
        canTimer = true;
    }

    private void FixedUpdate()
    {
        if(canTimer)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            GameObject newNPC = Instantiate(NPC, spawnPoints[Random.Range(0, spawnPoints.Length)], Quaternion.identity, null);
            timer = Random.Range(firstNumForRandom, secondNumForRandom);//0.5f;
        }
    }
}
