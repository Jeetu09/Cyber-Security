using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnim : MonoBehaviour
{
    public float AnimationSpeed = 0.05f;
    public TextMeshProUGUI AnimatedTextOne;
    public TextMeshProUGUI AnimatedTextTwo;

    Coroutine currentAnim;

    public void ButtonOne()
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(TypeText(AnimatedTextOne));
    }

    public void ButtonTwo()
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(TypeText(AnimatedTextTwo));
    }

    IEnumerator TypeText(TextMeshProUGUI text)
    {
        if (text == null) yield break;

        string fullText = text.text;
        text.text = "";

        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(AnimationSpeed);
        }
    }
}
