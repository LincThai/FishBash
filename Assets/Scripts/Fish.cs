using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Scriptable Objects/Fish")]
public class Fish : ScriptableObject
{
    // set variables
    public string name;
    public string description;
    public Sprite fishSprite;
}
