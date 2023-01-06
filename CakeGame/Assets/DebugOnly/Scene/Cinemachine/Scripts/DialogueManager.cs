using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject sentenceUI;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public bool DisplayNextSentence() // true 반환 -> 아직 대화중이다.
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return false;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        return true;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }

    public void ActiveSentence()
    {
        sentenceUI.gameObject.SetActive(true);
    }

    public void DeactiveSentence()
    {
        sentenceUI.gameObject.SetActive(false);
    }
    
}
