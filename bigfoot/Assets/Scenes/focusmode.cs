using UnityEngine;

public class focusmode : MonoBehaviour
{
    public FirstPersonController fpc;
    public AudioSource src;
    bool silent = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fpc.isCrouched)
        {
            if(src.volume > 0.1 && !silent)
            {
                src.volume -= Time.deltaTime;
            }
            else
            {
                src.volume = 0.1f;
                silent = true;
            }
        }
        else if(silent)
        {
            if(src.volume < 1)
            {
                src.volume += Time.deltaTime * 3;
            }
            else
            {
                silent = false;
            }
        }
    }
}
