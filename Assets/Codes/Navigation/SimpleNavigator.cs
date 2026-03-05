using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ArrowTarget
{
    public Transform target;
    public Transform player;

    public Image arrowUI;
    public GameObject redFlag;

    public float edgeOffset = 80f;
    public float reachDistance = 3f;
}

public class SimpleNavigator : MonoBehaviour
{
    public List<ArrowTarget> targets = new List<ArrowTarget>();

    int currentTargetIndex = 0;

    void Start()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].redFlag != null)
                targets[i].redFlag.SetActive(false);

            if (targets[i].arrowUI != null)
                targets[i].arrowUI.gameObject.SetActive(i == 0); // only element 0 active
        }
    }

    void Update()
    {
        if (currentTargetIndex < 0 || currentTargetIndex >= targets.Count)
            return;

        ArrowTarget t = targets[currentTargetIndex];

        float distance = Vector3.Distance(t.player.position, t.target.position);

        if (distance <= t.reachDistance)
        {
            t.arrowUI.gameObject.SetActive(false);
            t.redFlag.SetActive(true);
            return;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(t.target.position);

        if (screenPos.z < 0)
            screenPos *= -1;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Vector3 dir = (screenPos - screenCenter).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        t.arrowUI.rectTransform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        float x = Mathf.Clamp(screenPos.x, t.edgeOffset, Screen.width - t.edgeOffset);
        float y = Mathf.Clamp(screenPos.y, t.edgeOffset, Screen.height - t.edgeOffset);

        t.arrowUI.rectTransform.position = new Vector3(x, y, 0);
    }

    // Call this to switch target
    public void SetTarget(int index)
    {
        if (index < 0 || index >= targets.Count) return;

        // disable previous arrow
        targets[currentTargetIndex].arrowUI.gameObject.SetActive(false);
        targets[currentTargetIndex].redFlag.SetActive(false);

        // change target
        currentTargetIndex = index;

        // enable new arrow
        targets[currentTargetIndex].arrowUI.gameObject.SetActive(true);
    }
}