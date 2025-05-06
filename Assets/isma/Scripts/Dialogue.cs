using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private float typingTime = 0.05f;

    private bool didDialogueStart;
    private bool typing = false;
    private int lineIndex;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        if (!didDialogueStart && typing)
        {
            didDialogueStart = true;
            dialogueBox.SetActive(true);
            lineIndex = 0;
            StartCoroutine(ShowLine());
        }
        else if (dialogueText.text == dialogueLines[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            SkipLine();
        }
    }

    public void SkipLine()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[lineIndex];
    }

    public void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialogueBox.SetActive(false);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        typing = true;

        foreach (var ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        typing = false;
    }

    public IEnumerator StartLine(int index)
    {
        dialogueText.text = string.Empty;

        foreach (var ch in dialogueLines[index])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

}
