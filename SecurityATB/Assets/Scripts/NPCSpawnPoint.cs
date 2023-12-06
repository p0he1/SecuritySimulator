using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{
    public Vector2[] spawnPoints;
    public GameObject NPC;
    private float timer;

    private void Start()
    {
        timer = Random.Range(2, 6);
    }

    private void FixedUpdate()
    {
         timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject newNPC = Instantiate(NPC, spawnPoints[Random.Range(0, spawnPoints.Length)], Quaternion.identity, null);
            timer = Random.Range(2, 6);
        }
    }
}
