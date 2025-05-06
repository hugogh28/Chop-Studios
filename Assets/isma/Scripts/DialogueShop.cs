using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueShop : MonoBehaviour
{
    [SerializeField] private AudioClip dialogueVoice;
    [SerializeField] private float typingTime;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private AudioSource audioSource;
    private bool didDialogueStart;
    private bool skipped = false;
    private int lineIndex;
    private int extIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    /*
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
    */
    
    public void StartRead(int index)
    {
        if (!didDialogueStart)
        {
            didDialogueStart = true;
            skipped = false;
            dialogueBox.SetActive(true);
            StartCoroutine(StartLine(index));
        }
    }

    public IEnumerator StartLine(int index)
    {
        dialogueText.text = string.Empty;

        extIndex = index;

        foreach (var ch in dialogueLines[extIndex])
        {
            dialogueText.text += ch;
            audioSource.PlayOneShot(dialogueVoice);
            yield return new WaitForSeconds(typingTime);
        }
        didDialogueStart = false;
        
        if(!skipped)
        {
            yield return new WaitForSeconds(2.0f);
        }

        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }
    
    public void SkipLine()
    {
        StopAllCoroutines();
        didDialogueStart = false;
        skipped = true;
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }
}
