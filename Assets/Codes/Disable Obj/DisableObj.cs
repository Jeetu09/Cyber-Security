using UnityEngine;

public class DisableObj : MonoBehaviour
{
    public GameObject Bubble;
  
    public void DisableBubble()
    {
        Bubble.SetActive(false);
    }
}
