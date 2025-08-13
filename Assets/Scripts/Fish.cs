using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Scriptable Objects/Fish")]
public class Fish : ScriptableObject
{
    // set variables
    public string fishName;
    // sizes up the text space im the inspector
    [TextArea(1, 10)]
    public string description;
    public Sprite fishSprite;
}
