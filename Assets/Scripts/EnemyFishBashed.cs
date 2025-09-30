using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFishBashed : MonoBehaviour
{
    // set variables
    // enemy Variables
    [Header("Editable Variables")]
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int enemyDamage;
    public float enemyAttackCooldownMax;
    public float enemyAttackCooldownMin;
    public float enemyChargeTime;
    // states
    int state = 0; //0 = ready, 1 = attack, 2 = cooldown

    // references
    [Header("References")]
    public GameObject fishBashUI;
    public TMP_Text fishTag;
    public TMP_Text resultsText;
    public GameObject results;
    public Image enemySprite;
    public HealthBar enemyHealthBar;
    public PlayerFishBash playerToBash;
    public FishingAreaTrigger fishingAreaTrigger;
    public Animator fishAnimator;

    private Fish currentFish;

    private void OnEnable()
    {
        // assign fishing area trigger
        fishingAreaTrigger = GameManager.instance.currentFishArea;

        // assigning the current fish to bash in the minigame from the trigger
        currentFish = fishingAreaTrigger.catchableFish;
        // Assigning UI element Info
        fishTag.text = currentFish.fishName;
        enemySprite.sprite = currentFish.fishSprite;
        // animations
        fishAnimator.runtimeAnimatorController = currentFish.animationOverrideController;
        // Assign Enemy Data
        enemyMaxHealth = currentFish.MaxHealth;
        enemyDamage = currentFish.damage;
        enemyAttackCooldownMax = currentFish.attackMaxCooldown;
        enemyAttackCooldownMin = currentFish.attackMinCooldown;
        enemyChargeTime = currentFish.attackChargeTime;

        // set the health of the enemy for both the game and UI
        enemyCurrentHealth = enemyMaxHealth;
        enemyHealthBar.SetMaxHealth(enemyMaxHealth);
        enemyHealthBar.SetHealth(enemyCurrentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // state check to check in which the enemy is in
        if (state == 0)
        {
            // start the coroutine of the charged attack
            StartCoroutine(EnemyAttack());
            // change to the attack state
            state = 1;
        }
        else if (state == 1)
        {

        }else if (state == 2)
        {
            // start the coroutine of the cooldown between the enemies attacks
            StartCoroutine(Delay(Random.Range(enemyAttackCooldownMin, enemyAttackCooldownMax)));
        }
    }

    IEnumerator Delay(float randTime)
    {
        // stop attack animation
        fishAnimator.SetBool("Attacking", false);
        // wait for a random amount of time
        yield return new WaitForSeconds(randTime);
        // change to the ready state
        state = 0;
    }

    public IEnumerator EnemyAttack()
    {
        // start windup animation
        fishAnimator.SetBool("Charging", true);

        // play charge sound effect
        FindObjectOfType<AudioManager>().Play("Charge_Up");

        // wait for attack charge
        yield return new WaitForSeconds(enemyChargeTime);

        // stop charge animation
        fishAnimator.SetBool("Charging", false);
        // play ping sound effect


        // apply damage to player using the player's take damage function
        playerToBash.PlayerTakeDamage(enemyDamage);

        // animate the attack
        fishAnimator.SetBool("Atacking", true);

        // play the sound
        FindObjectOfType<AudioManager>().Play("Enemy_Attack");

        // change to the cooldown state
        state = 2;
    }

    public void EnemyTakeDamage(int damage)
    {
        // reduces enemies health
        enemyCurrentHealth -= damage;
        Debug.Log("Enemy Health = " + enemyCurrentHealth);

        // update the UI
        enemyHealthBar.SetHealth(enemyCurrentHealth);

        // play enemy damaged sound and animation
        FindObjectOfType<AudioManager>().Play("Fish_Hurt");

        if (enemyCurrentHealth <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    public IEnumerator EnemyDeath()
    {
        // play death sound
        FindObjectOfType<AudioManager>().Play("Death");

        // update the fishing area trigger
        fishingAreaTrigger.numOfFish -= 1;

        // show lose screen
        results.SetActive(true);
        resultsText.text = "KO You Win";

        // wait till deactivate
        yield return new WaitForSeconds(3);

        // close the minigame/UI
        results.SetActive(false);
        fishBashUI.SetActive(false);
    }

}
