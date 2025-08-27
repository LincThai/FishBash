using UnityEngine;
using UnityEngine.UI;

public class EnemyFishBashed : MonoBehaviour
{
    // set variables
    // enemy Variables
    [Header("Editable Variables")]
    public int enemyMaxHealth = 3;
    public int enemyCurrentHealth;
    public int enemyDamage = 1;
    public int enemyAttackRate = 2;
    public int enemyChargeTime = 2;

    // references
    [Header("References")]
    public GameObject fishBashUI;
    public PlayerFishBash playerToBash;
    public FishingAreaTrigger fishingAreaTrigger;
    public Image fishSprite;
    private Fish currentFish;

    private void OnEnable()
    {
        // assigning the current fish to bash in the minigame from the trigger
        currentFish = fishingAreaTrigger.catchableFish;
        fishSprite.sprite = currentFish.fishSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyAttack()
    {
        // apply damage to player using the player's take damage function
        playerToBash.PlayerTakeDamage(enemyDamage);

        // animate the attack

        // play the sound
    }

    public void EnemyTakeDamage(int damage)
    {
        // reduces enemies health
        enemyCurrentHealth -= damage;

        // play enemy damaged sound and animation

        if (enemyCurrentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        // close the minigame/UI
        fishBashUI.SetActive(false);

        // update the fishing area trigger
        fishingAreaTrigger.numOfFish -= 1;
    }

}
