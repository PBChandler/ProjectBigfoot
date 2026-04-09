using Unity.AI.Navigation;
using UnityEngine;

public class GameChunk : MonoBehaviour
{
    public float playerPresence = 0;
    public float interest = 0;
    public GameObject Player;
    bool playerInBounds = false;
    public NavMeshModifier nmm;

    public void Start()
    {
        nmm = GetComponent<NavMeshModifier>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            playerInBounds = true;
        }
    }

    public void OerExit(Collider other)
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player)
        {
            playerInBounds = false;
        }
    }

    public void Update()
    {
        if(playerInBounds)
        {
            playerPresence += Time.deltaTime * 444;
            nmm.enabled = true;
        }
        else if(playerPresence > 0)
        {
            if(playerPresence < 1)
                nmm.enabled = false;
            playerPresence = Mathf.Clamp(playerPresence - Time.deltaTime, 0, 1000);
        }
        
    }
}
