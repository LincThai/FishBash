using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Scriptable Objects/Fish")]
public class Fish : ScriptableObject
{
    // set variables
    public string fishName;
    // sizes up the text space im the inspector
    [TextArea(1, 10)]
    public string description;
    // UI/Animation
    public Sprite fishSprite;
    public AnimatorOverrideController animationOverrideController;
    // Combat Data
    public int MaxHealth = 3;
    public int damage = 1;
    public float attackMaxCooldown = 3;
    public float attackMinCooldown = 1;
    public float attackChargeTime = 2;

}
