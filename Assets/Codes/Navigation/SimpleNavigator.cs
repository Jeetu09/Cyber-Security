using UnityEngine;
using UnityEngine.UI;

public class EdgeArrowNavigator : MonoBehaviour
{
    public Transform sofa;
    public Transform player;

    public Image arrowUI;
    public GameObject redFlag;

    public float edgeOffset = 80f;
    public float reachDistance = 3f;

    void Start()
    {
        redFlag.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, sofa.position);

        // If player reached sofa
        if (distance <= reachDistance)
        {
            arrowUI.gameObject.SetActive(false);
            redFlag.SetActive(true);
            return;
        }
        else
        {
            redFlag.SetActive(false);
            arrowUI.gameObject.SetActive(true);
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(sofa.position);

        if (screenPos.z < 0)
        {
            screenPos *= -1;
        }

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Vector3 dir = (screenPos - screenCenter).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrowUI.rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        float x = Mathf.Clamp(screenPos.x, edgeOffset, Screen.width - edgeOffset);
        float y = Mathf.Clamp(screenPos.y, edgeOffset, Screen.height - edgeOffset);

        arrowUI.rectTransform.position = new Vector3(x, y, 0);
    }
}