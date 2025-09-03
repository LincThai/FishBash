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

    private float nextAttackTime = 5f;

    // references
    [Header("References")]
    public GameObject fishBashUI;
    public Image enemySprite;
    public HealthBar enemyHealthBar;
    public PlayerFishBash playerToBash;
    public FishingAreaTrigger fishingAreaTrigger;

    private Fish currentFish;

    private void OnEnable()
    {
        // connect to game manager
        GameManager.instance._EnemyFishBashed = this;

        // assign fishing area trigger
        fishingAreaTrigger = GameManager.instance.currentFishArea;

        // assigning the current fish to bash in the minigame from the trigger
        currentFish = fishingAreaTrigger.catchableFish;
        Debug.Log(currentFish);
        enemySprite.sprite = currentFish.fishSprite;

        // set the health of the enemy for both the game and UI
        enemyCurrentHealth = enemyMaxHealth;
        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        enemyHealthBar.SetHealth(enemyCurrentHealth);

        Debug.Log("Enemy Health reset");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyCurrentHealth);
        // for the rate of attack
        if (Time.time >= nextAttackTime)
        {
            // call the attack function
            EnemyAttack();

            // update for next attack
            nextAttackTime = Time.time + 5f / enemyAttackRate;
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

        // update the UI
        enemyHealthBar.SetHealth(enemyCurrentHealth);

        // play enemy damaged sound and animation

        if (enemyCurrentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        // update the fishing area trigger
        fishingAreaTrigger.numOfFish -= 1;

        // close the minigame/UI
        fishBashUI.SetActive(false);
    }

}
