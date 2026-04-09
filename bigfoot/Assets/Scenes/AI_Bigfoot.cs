using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Bigfoot : MonoBehaviour
{
    public NavMeshAgent nma;
    public Vector3 location;
    public Transform targetpoint;

    public GameObject indicator;
    public float fleeDistance = 5f;
    public GameObject player;

    public List<GameChunk> chunks;
    public LinkedList<GameChunk> recentChunks = new LinkedList<GameChunk>();
    public GameChunk currentChunk;

    public bool chasingPlayer;
    void Start()
    {
        InvokeRepeating("keepOnTheMove", 0f, 4f);

        
    }

    public void keepOnTheMove()
    {
        if(chasingPlayer) return;
        location = pickNewLocation().transform.position;
        //nma.SetDestination(location);
        NavMeshPath nmp = new NavMeshPath();

       


        nma.SetDestination(location);

    }
    public void Casteljau(NavMeshPath path, Vector3 p1, Vector2 p2, Vector3 p3, Vector3 p4, int iterations)
    {
        float stepping = 1.0f / iterations;

        for (float x = 0.0f; x <= 1.0f; x += stepping)
        {
            var ap1 = Vector3.Lerp(p1, p2, x);
            var ap2 = Vector3.Lerp(p2, p3, x);
            var ap3 = Vector3.Lerp(p3, p4, x);

            var bp1 = Vector3.Lerp(ap1, ap2, x);
            var bp2 = Vector3.Lerp(ap2, ap3, x);

            var p = Vector3.Lerp(bp1, bp2, x);

            // Place a vertex at p

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, new Vector3(location.x, transform.position.y, location.z)) < 5)
        {
            Debug.Log("picking new location");
            keepOnTheMove();
        }
        else
        {
            if (currentChunk.playerPresence > 0)
            {
                keepOnTheMove();
            }
        }
        

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < fleeDistance) {
            Vector3 fleeDirection = (transform.position - player.transform.position).normalized;
            Vector3 targetDestination = (transform.position + fleeDirection * fleeDistance)*4;
            nma.SetDestination(targetDestination);
            chasingPlayer = true;
        }
        else
        {
            chasingPlayer = false;
        }
        indicator.transform.position = nma.destination;
    }

    public GameChunk pickNewLocation()
    {
        GameChunk prop = chunks[Random.Range(0, chunks.Count)];
        for (int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i].playerPresence <= prop.playerPresence && chunks[i].interest >= prop.interest)
            {
                if (recentChunks.Contains(chunks[i])) continue;


                prop = chunks[i];
            }

        }
        if (recentChunks.Count > 3)
        {
            recentChunks.RemoveLast();
        }
        recentChunks.AddFirst(prop);
        currentChunk = prop;
        return prop;
    }
}
