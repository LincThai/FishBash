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
    public bool fightEnd = false;
    // states
    int state = 2; //0 = ready, 1 = attack, 2 = cooldown are Primary states. 3 = End

    // references
    [Header("References")]
    public GameObject fishBashUI;
    public TMP_Text fishTag;
    public Animator resultsAnimator;
    public GameObject results;
    public Image enemySprite;
    public HealthBar enemyHealthBar;
    public PlayerFishBash playerToBash;
    public FishingAreaTrigger fishingAreaTrigger;
    public Animator fishAnimator;
    public Inventory inventory;

    private Fish currentFish;
    private Coroutine activeCoroutine;
    // animations
    bool animatorSet = false;

    private void OnEnable()
    {
        // assign fishing area trigger
        fishingAreaTrigger = GameManager.instance.currentFishArea;

        // assigning the current fish to bash in the minigame from the trigger
        currentFish = fishingAreaTrigger.catchableFish;
        // Assigning UI element Info
        fishTag.text = currentFish.fishName;
        enemySprite.sprite = currentFish.fishSprite;
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

        // reset animation
        animatorSet = false;
        // fightEnd variable reset
        fightEnd = false;
        state = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!animatorSet)
        {
            // swap animator animation
            Debug.Log("Swapped the animation");
            fishAnimator.runtimeAnimatorController = currentFish.animationOverrideController;
            animatorSet = true;
        }

        // set to end state when game ends
        if (fightEnd)
        {
            // change to the end state
            Debug.Log("End Fight");
            state = 3;
        }

        // state check to check in which state the enemy is in
        if (state == 0)
        {
            // check if the assigned coroutine is active and stops it from being started again
            if (activeCoroutine != null)
            {
                return;
            }

            // start the coroutine of the charged attack
            activeCoroutine = StartCoroutine(EnemyAttack());
            // change to the attack state
            state = 1;
            Debug.Log("State =" + state);
        }
        else if (state == 1)
        {
            //Debug.Log("TAKE THIS !!!!!!!!!!!!!");
        }
        else if (state == 2)
        {
            // check if the assigned coroutine is active and stops it from being started again
            if (activeCoroutine != null)
            {
                return;
            }

            // start the coroutine of the cooldown between the enemies attacks
            activeCoroutine = StartCoroutine(Delay(Random.Range(enemyAttackCooldownMin, enemyAttackCooldownMax)));
        }
        else if (state == 3) { Debug.Log("State =" + state); return; } 
    }

    IEnumerator Delay(float randTime)
    {
        // stop attack animation
        fishAnimator.SetBool("Attacking", false);
        // wait for a random amount of time
        yield return new WaitForSeconds(randTime);

        //Debug.Log("Delay = " + randTime);
        // clear the coroutine check
        activeCoroutine = null;
        // change to the ready state
        state = 0;
        Debug.Log("State =" + state);
    }

    public IEnumerator EnemyAttack()
    {
        // start windup animation
        fishAnimator.SetBool("Charging", true);

        // play charge sound effect
        FindObjectOfType<AudioManager>().Play("Charge_Up");

        // wait for attack charge
        yield return new WaitForSeconds(enemyChargeTime);

        //Debug.Log("Enemy Charge Time = " + enemyChargeTime);

        // stop charge animation
        fishAnimator.SetBool("Charging", false);
        // play ping sound effect
        FindObjectOfType<AudioManager>().Play("Ping");

        // apply damage to player using the player's take damage function
        playerToBash.PlayerTakeDamage(enemyDamage);

        // animate the attack
        fishAnimator.SetBool("Attacking", true);

        // play the sound
        FindObjectOfType<AudioManager>().Play("Enemy_Attack");

        // clear the coroutine check
        activeCoroutine = null;
        
        // change to the cooldown state
        state = 2;
        Debug.Log("State =" + state);
    }

    public void EnemyTakeDamage(int damage)
    {
        // reduces enemies health
        enemyCurrentHealth -= damage;
        //Debug.Log("Enemy Health = " + enemyCurrentHealth);

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
        FindObjectOfType<AudioManager>().Play("Fish_Him");

        // update the fishing area trigger
        fishingAreaTrigger.numOfFish -= 1;

        // show lose screen
        results.SetActive(true);
        resultsAnimator.SetBool("Win", true);

        // stop enemy attacks
        fightEnd = true;

        // wait till deactivate
        yield return new WaitForSeconds(3);

        resultsAnimator.SetBool("Win", false);

        // adds the fish to the inventory list
        inventory.AddFish(currentFish);

        // close the minigame/UI
        results.SetActive(false);
        fishBashUI.SetActive(false);
    }
}
