using UnityEngine;
using UnityEngine.Events;
public class StupidInteractable : MonoBehaviour
{
   public bool hasAlreadyBeenInteractedWith;
    public UnityEvent mEvent;
   public void OnInteract()
    {
        if(hasAlreadyBeenInteractedWith == true) return;
        hasAlreadyBeenInteractedWith = true;
        mEvent.Invoke();
    }
}
