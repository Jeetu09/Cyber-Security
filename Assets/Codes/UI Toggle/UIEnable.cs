using UnityEngine;

public class UIEnable : MonoBehaviour
{
    public GameObject EmailUI;
    public GameObject Email;


    void Start()
    {
        Email.SetActive(false);
    }
    public void EmailButton()
    {
        EmailUI.SetActive(true);
    }

    public void email()
    {
        Email.SetActive(true);
    }

}
