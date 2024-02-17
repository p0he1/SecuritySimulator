using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public List<Vector2> firstQueuePlaces = new List<Vector2>();
    public List<Vector2> secondQueuePlaces = new List<Vector2>();
    public List<GameObject> npcQueue1 = new List<GameObject>();
    public List<GameObject> npcQueue2 = new List<GameObject>();

    private void Start()
    {
        Vector2 firstPlace = new Vector2(-2.12f, -1.2f);
        Vector2 secondPlace = new Vector2(1.1f, -1.2f);
        //firstPlace.x int queue -1 becouse in loop we already make it +1
        for (int i=0; i<4; i++)
        {
            firstQueuePlaces.Add(new Vector2(firstPlace.x, firstPlace.y += 1));
        }
        for (int i=0; i<4; i++)
        {
            secondQueuePlaces.Add(new Vector2(secondPlace.x, secondPlace.y += 1));
        }
    }

    private void FixedUpdate()
    {
    }

    public int Enqueue(NPC npc)
    {
        int index;
        if (npc.cr1or2 == 1)
        {
            if (firstQueuePlaces.Count > npcQueue1.Count)
            {
                npcQueue1.Add(npc.gameObject);
                index = npcQueue1.Count - 1;
            }
            else
            {
                index = 0;
            }
        }
        else if (npc.cr1or2 == 2)
        {
            if (secondQueuePlaces.Count > npcQueue2.Count)
            {
                npcQueue2.Add(npc.gameObject);
                index = npcQueue2.Count - 1;
            }
            else
            {
                index = 0;
            }
        }
        else index = 0;
        return index;
    }

    public void Dequeue(NPC npcScript)
    {
        if (npcScript.cr1or2 == 1 && npcQueue1.Count != 0)
        {
            npcQueue1.RemoveAt(0);
            foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
                if (npc.GetComponent<NPC>().howWalking == NPC.HowWalking.waitingQueue && npc.GetComponent<NPC>().cr1or2 == 1)
                    npc.GetComponent<NPC>().orderInQueue -= 1;
        }
        else if(npcScript.cr1or2 == 2 && npcQueue2.Count != 0)
        {
            npcQueue2.RemoveAt(0);
            foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
                if (npc.GetComponent<NPC>().howWalking == NPC.HowWalking.waitingQueue && npc.GetComponent<NPC>().cr1or2 == 2)
                    npc.GetComponent<NPC>().orderInQueue -= 1;
        }
        
    }
}
