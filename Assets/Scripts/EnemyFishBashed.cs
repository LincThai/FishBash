using System.Collections;
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
    public float enemyAttackRate = 2f;
    public float enemyChargeTime = 2f;

    private float nextAttackTime;

    // references
    [Header("References")]
    public GameObject fishBashUI;
    public PlayerFishBash playerToBash;
    public FishingAreaTrigger fishingAreaTrigger;
    public Image fishSprite;
    public GameObject enemyHealthBar;
    private Fish currentFish;

    private void OnEnable()
    {
        // assigning the current fish to bash in the minigame from the trigger
        currentFish = fishingAreaTrigger.catchableFish;
        fishSprite.sprite = currentFish.fishSprite;

        enemyCurrentHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // for the rate of attack
        if (Time.time >= nextAttackTime)
        {
            // call the attack function
            EnemyAttack();

            // update for next attack
            nextAttackTime = Time.time + 1f / enemyAttackRate;
        }
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
        Debug.Log("Enemy Health = " + enemyCurrentHealth);

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
