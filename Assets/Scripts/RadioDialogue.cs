using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RadioDialogue : MonoBehaviour
{
    // set variables
    // singleton variable
    public static RadioDialogue instance;
    // text variable
    public Queue<string> sentences;

    // references
    public TMP_Text dialogueText;
    public GameObject textBox;
    public Image icon;
    public Animator animator;

    // inputs
    private InputAction nextAction;

    private void Awake()
    {
        // a singleton to make sure there is only one of these in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        nextAction = InputSystem.actions.FindAction("NextSentence");
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (nextAction.IsPressed())
        {
            // start the next sentence
            DisplayNextSentence();
        }
    }

    public void StartDialogue(DialogueQuest dialogue)
    {
        // play open animation
        animator.SetBool("isOpen", true);

        // clears the queue
        sentences.Clear();

        // gets the dialogue in an dialogue class/scriptable object
        foreach (string sentence in dialogue.sentences) 
        {
            // assign it to a new element in the queue array
            sentences.Enqueue(sentence);
        }

        // call a method to display the next sentence
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            // call function to end the dialogue and stop this function
            EndDialogue();
            return;
        }

        // create a string variable to store the sentence taken from the queue
        string sentence = sentences.Dequeue();
        // stops all the coroutines running currently on this script
        StopAllCoroutines();
        // start the TypeSentence coroutine which will type out the sentence in the text object
        StartCoroutine(TypeSentence(sentence));
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

    public void EndDialogue()
    {
        Debug.Log("End Dialogue");
        // play close animation
        animator.SetBool("isOpen", false);
        // set the the bool for when the dialogue has been completed to true
        GameManager.instance.dialogueFinished = true;
    }

}
