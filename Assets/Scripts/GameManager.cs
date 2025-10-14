using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // set variables
    // fishing variables
    public FishingAreaTrigger currentFishArea;
    
    // dialogue and quest variables
    RadioDialogue radioDialogue;
    public DialogueQuest[] dialogueQuests;
    private int dialogue_Index = 0;

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

        radioDialogue = RadioDialogue.instance;
    }

    private void Update()
    {
        
    }

    // function to check and manage quests
    public void questManager()
    {
        // check if there are dialogues and/or quests
        if (dialogueQuests != null) 
        {
            // check if it is a quest
            if (dialogueQuests[dialogue_Index].isQuest == true)
            {
                // store the objective to check later
                Fish objective = dialogueQuests[dialogue_Index].questObjective;
            }
            else { return; }
        }
    }

    // function to trigger the dialogue
    public void TriggerDialogue(DialogueQuest dialogue)
    {
        // call the start dialogue function by passing in dialogue
        radioDialogue.StartDialogue(dialogue);
    }
}
