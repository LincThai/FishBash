using UnityEngine;

[CreateAssetMenu(fileName = "DialogueQuest", menuName = "Scriptable Objects/DialogueQuest")]
public class DialogueQuest : ScriptableObject
{
    // set variables
    public Sprite icon;

    [TextArea(3, 10)]
    public string[] sentences;
}
