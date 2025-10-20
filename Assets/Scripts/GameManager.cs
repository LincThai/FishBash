using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // set variables
    // fishing variables
    public FishingAreaTrigger currentFishArea;
    
    // dialogue and quest variables
    RadioDialogue radioDialogue;
    Inventory inventory;
    public DialogueQuest[] dialogueQuests;
    private int dialogue_Index = 0;
    public Fish objective;
    public float longWaittime = 5;
    public float shortWaitTime = 2;
    public bool dialogueFinished = false;

    private Coroutine activeCoroutine;

    private void Awake()
    {
        // singleton to ensure there is only one gamemanager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // connect the dialogue system to the game manager
        radioDialogue = RadioDialogue.instance;

        // connect the inventory system to the game manager
        inventory = FindAnyObjectByType<Inventory>();

        if (dialogueQuests != null)
        {
            dialogue_Index = 0;
            // Wait then start the first dialogue
            StartCoroutine(DialogueWait(2));
        }
    }

    private void Update()
    {
        if (dialogueQuests != null)
        {
            if (dialogueFinished)
            {
                if (objective == null)
                {
                    // stops the coroutine from being called again
                    if (activeCoroutine != null)
                    {
                        return;
                    }
                    // start the coroutine but also store it for a check to stop repetition of the coroutine
                    activeCoroutine = StartCoroutine(DialogueWait(longWaittime));
                }
            }
        }
    }

    // function to check and manage quests
    public void questManager()
    {
        // check if it is a quest
        if (dialogueQuests[dialogue_Index].isQuest == true)
        {
            // store the objective to check later
            objective = dialogueQuests[dialogue_Index].questObjective;
            // start the dialogue
            TriggerDialogue(dialogueQuests[dialogue_Index]);
        }
        // even if there is no quest it will trigger the dialogue
        else { TriggerDialogue(dialogueQuests[dialogue_Index]); return; }
    }

    public void SubmitQuest(Fish fish)
    {
        // check if the fish and the objective are the same
        if (objective == fish)
        {
            // remove the fish from the list
            inventory.RemoveFish(fish);

            // empty the objective
            objective = null;

            // start the wait for the next dialogue
            StartCoroutine(DialogueWait(shortWaitTime));
        }
        else { return; }
    }

    IEnumerator DialogueWait(float delay)
    {
        Debug.Log("Waiting Till next Call");
        // wait till the next dialogue
        yield return new WaitForSeconds(delay);
        // call quest manager
        questManager();
    }

    // function to trigger the dialogue
    public void TriggerDialogue(DialogueQuest dialogue)
    {
        // reset the bool for finished dialogue
        dialogueFinished = false;
        // call the start dialogue function by passing in dialogue
        radioDialogue.StartDialogue(dialogue);
        // increase the value of dialogue index
        dialogue_Index++;
    }
}
