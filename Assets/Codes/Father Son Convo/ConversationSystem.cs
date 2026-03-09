using System.Collections;
using UnityEngine;
using TMPro;

public class ConversationSystem : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public GameObject image;
        public TMP_Text dialogueText;
        [TextArea] public string sentence;
    }

    public Dialogue[] dialogues;
    public float typingSpeed = 0.05f;
    public float nextDelay = 1f;

    int currentIndex = 0;

    void Start()
    {
        foreach (Dialogue d in dialogues)
        {
            d.image.SetActive(false);
        }

        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        while (currentIndex < dialogues.Length)
        {
            Dialogue d = dialogues[currentIndex];

            d.image.SetActive(true);
            d.dialogueText.text = "";

            foreach (char letter in d.sentence.ToCharArray())
            {
                d.dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(nextDelay);

            currentIndex++;
        }

        DialogueEnded();
    }

    public void DialogueEnded()
    {
        //Debug.Log("Dialogue Finished");
    }
}