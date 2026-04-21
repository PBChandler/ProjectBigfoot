using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BigfootPerception : MonoBehaviour
{
    public float percentageOfBigfootInRawFrame;
    public FirstPersonController fpc;
    public Transform wholeSystemCamera;
    public TextMeshProUGUI creature;
    public Vector3 defaultPos, farPos;
    public float numPixels;
    [SerializeField] RenderTexture actualScreen;

    public RawImage playerHands;
    
    public GameObject flashlight;
    public float defaultFov = 6.2f;
    public Camera minus, just, regular;
    float lastScore = 0;
    public MeshRenderer source;

    public List<float> chess = new List<float>();
    public void Start()
    {
        defaultPos = wholeSystemCamera.localPosition;
        farPos = wholeSystemCamera.localPosition + (wholeSystemCamera.forward*12);
        InvokeRepeating("camupdate", 0f, 0.3f);
    }

    public void Update()
    {
        if(fpc.isZoomed)
        {
            minus.fieldOfView = defaultFov - 2;
            just.fieldOfView = minus.fieldOfView;
            regular.fieldOfView = minus.fieldOfView;
        }
        else
        {
            minus.fieldOfView = defaultFov * 0.66f;
            just.fieldOfView = minus.fieldOfView;
            regular.fieldOfView = minus.fieldOfView;
        }

        if(Input.GetMouseButtonDown(0))
        {
            
            chess.Add(lastScore);
            flashlight.SetActive(true);
            Invoke("resetlight", 0.2f);
        }

    }

    public void resetlight()
    {
        flashlight.SetActive(false);
    }
    public void camupdate()
    {
        Texture2D crown = ToTexture2D(actualScreen);
        crown.Apply();
        numPixels = crown.GetPixels().Length;
        int numBigFoot = 0;
        foreach(Color pixel in crown.GetPixels())
        {
            if(pixel.r > 0)
            {
                numBigFoot++;
            }
        }
        percentageOfBigfootInRawFrame = numBigFoot;
        creature.text = (Mathf.Floor((numBigFoot / numPixels)*100)) + "% BIGFOOT";
        lastScore = Mathf.Floor((numBigFoot/numPixels)*100);
    }

    public Texture2D ToTexture2D(RenderTexture rTex)
    {
        // 1. Create a new Texture2D with matching dimensions
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);

        // 2. Cache current active RT to restore it later
        RenderTexture oldActive = RenderTexture.active;

        // 3. Set the source RenderTexture as active
        RenderTexture.active = rTex;

        // 4. Read pixels into the Texture2D
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();

        // 5. Restore previous active RenderTexture
        RenderTexture.active = oldActive;

        return tex;
    }
}
