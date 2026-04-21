using System.Net.Mime;
using TMPro;
using UnityEngine;

public class StupidInteractions : MonoBehaviour
{
    public float crawlspeed;
    public AudioClip talkSound;
    public AudioSource playerMouth;
    public TextMeshProUGUI text;
    public string content, loadingContent;
    public bool playerHasReadThis;
    private int i = 0;
    void Start()
    {
        InvokeRepeating("talk", 0f, crawlspeed);
    }

    public void talk()
    {
        if (content != string.Empty)
        {
            if (loadingContent.Length < content.Length)
            {
                loadingContent += content[i];
                i++;
                if (content[i] != '.' && content[i] != '?' && content[i] != '!' && content[i] != ' ')
                {
                    playerMouth.volume = Random.Range(0.3f, 0.6f);
                    playerMouth.pitch = Random.Range(0.9f, 1.1f);
                    playerMouth.PlayOneShot(talkSound);
                }


            }
            else if (!playerHasReadThis)
            {
                Invoke("readTimer", 1.5f);
            }
            else if (playerHasReadThis)
            {
                loadingContent = "";
                i = 0;
                playerHasReadThis = false;
                content = "";
            }
        }
        text.text = loadingContent;
    }

    public void SetContent(string c)
    {
        loadingContent = "";
        content = "";
        text.text = "";
        content = c;
    }
    public void readTimer()
    {
        playerHasReadThis = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
