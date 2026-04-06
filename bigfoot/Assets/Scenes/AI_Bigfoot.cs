using UnityEngine;
using UnityEngine.AI;

public class AI_Bigfoot : MonoBehaviour
{
    public NavMeshAgent nma;
    public Vector3 location;
    public Transform targetpoint;
    void Start()
    {
         location = transform.position + new Vector3(Random.Range(100, -100),Random.Range(100, -100),Random.Range(100, -100) );
            nma.SetDestination(targetpoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(nma.destination != targetpoint.transform.position)
            nma.SetDestination(targetpoint.transform.position);
    }
}
