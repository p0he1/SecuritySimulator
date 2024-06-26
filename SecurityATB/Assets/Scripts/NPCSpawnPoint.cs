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
    private QueueManager queueManager;

    public GameObject[] npcSprites;
    public GameObject[] npcSpritesForMiniGames;

    private void Start()
    {
        queueManager = GameObject.FindGameObjectWithTag("Queue Manager").GetComponent<QueueManager>();
        timer = Random.Range(firstNumForRandom, secondNumForRandom);
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
            if(queueManager.firstQueuePlaces.Count != queueManager.npcQueue1.Count && queueManager.secondQueuePlaces.Count != queueManager.npcQueue2.Count)
            {
                GameObject newNPC = Instantiate(NPC, spawnPoints[Random.Range(0, spawnPoints.Length)], Quaternion.identity, null);
                timer = Random.Range(firstNumForRandom, secondNumForRandom);
            }
        }
    }
}
