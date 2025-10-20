using UnityEngine;

[CreateAssetMenu(fileName = "DialogueQuest", menuName = "Scriptable Objects/DialogueQuest")]
public class DialogueQuest : ScriptableObject
{
    // set variables
    // Dialogue Variables
    [Header("Dialogue")]
    public Sprite icon;

    [TextArea(3, 10)]
    public string[] sentences;

    // Quest Variables
    [Header("Quest")]
    public bool isQuest;
    public Fish questObjective;
}
