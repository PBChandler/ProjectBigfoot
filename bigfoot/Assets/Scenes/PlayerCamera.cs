using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera viewFinder;
    public Camera onlySeesBigfoot;

    public Texture2D onlyBigFootView;
    public Texture2D playerView;
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TakePhoto();
        }
    }
    public void TakePhoto()
    {
        Debug.Log("taking a photo");
        RenderTexture photo = viewFinder.activeTexture;
        photo.Create();
        
    }

}
