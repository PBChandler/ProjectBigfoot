using UnityEngine;

public class BigfootBeam : MonoBehaviour
{
    public int numbeams;
    public GameObject player;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireBeams();
        }
        
    }
    public void FireBeams()
    {
        
    }
}
