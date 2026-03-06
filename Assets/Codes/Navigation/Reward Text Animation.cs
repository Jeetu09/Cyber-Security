using UnityEngine;

public class RewardTextAnimation : MonoBehaviour
{
    public Animator animator;

    [SerializeField]
    private SceneMan sceneMan;

    public GameObject reportUI;

    public void animationPay()
    {
        if (sceneMan.SceneCounter == 0f)
        {
            animator.Play("RewadtopDown", 0, 0f);
        }
        else if (sceneMan.SceneCounter == 1f)
        {
            reportUI.SetActive(true);
        }
    }
}