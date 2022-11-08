using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //List of sentences to display in the current dialogue.
    private Queue<string> sentences;
    public Dialogue currentDialogue;

    [Header("Animators")]
    public Animator animator;
    public Animator triggerAnimator;


    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Choices Menu")]
    public GameObject choices;
    public List<GameObject> choiceButtons;
    public List<TextMeshProUGUI> choiceText;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;

        animator.SetBool("isOpen", true);
        triggerAnimator.SetBool("isOpen", false);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            //If we have children, we need to show the choices menu.
            if (currentDialogue.choices != null)
            {
                choices.SetActive(true);
                animator.SetBool("choicesOpen", true);
                //Set the buttons
                for (int i = 0; i < currentDialogue.children.Length; i++)
                {
                    choiceButtons[i].SetActive(true);
                    choiceText[i].text = currentDialogue.choices[i];
                }

                for (int j = currentDialogue.children.Length; j < choiceButtons.Count; j++)
                {
                    choiceButtons[j].SetActive(false);
                    choiceText[j].text = "";
                }
                return;
            }
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Called when a choice is selected. We then load the response for that choice.
    public void ChoiceClick(int index)
    {
        //Turn the choices off. 
        choices.SetActive(false);
        animator.SetBool("choicesOpen", false);

        //Get the response for that choice, and show dialogue with that.
        //We need to access the value at that index, and find the key for it. 
        //We're currently on the dialogue that tells us the children. 
        //So our new dialogue is whatever has the key for the child of that index. 
        ProcessNextDialogue(index);
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void ProcessNextDialogue(int index)
    {
        string newKey = currentDialogue.children[index];
        Dialogue nextDialogue = FindObjectOfType<DialogueLoader>().SetDialogue(newKey);

        currentDialogue = nextDialogue;
        StartDialogue(nextDialogue);
    }

    public void EndDialogue()
    {
        //We need to do a check for children. 
        if (currentDialogue.children != null)
        {
            //There's a new piece in the chain, and we need to process it.
            ProcessNextDialogue(0);
        }
        else
        {
            currentDialogue = null;
            animator.SetBool("isOpen", false);

            StartCoroutine(DelaySceneLoad());
        }
    }

    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(2f);
        //this.GetComponent<LoadScene>().Load();
    }
}