using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishBash : MonoBehaviour
{
    // set variables
    // player variables
    [Header("Editable Variables")]
    public int playerMaxLives = 3;
    public int playerCurrentLives;
    public int playerDamage = 1;
    public float playerAttackCooldown = 1f;
    public bool isGuarding = false;

    private float nextLeftPunch;
    private float nextRightPunch;

    // references
    [Header("References")]
    public GameObject fishBashUi;
    public HealthBar playerHealthBar;
    public EnemyFishBashed enemyToBash;
    public FishingAreaTrigger fishAreaTrigger;
    public TMP_Text resultsText;
    public GameObject results;
    public Animator animatorLeftFist;
    public Animator animatorRightFist;
    public Animator animatorBlockArms;
    public GameObject gaurdArms;
    public Cooldown leftCooldown;
    public Cooldown rightCooldown;

    // inputs
    private InputAction guardAction;
    private InputAction attackActionL;
    private InputAction attackActionR;

    private void Awake()
    {
        // subscribe to the inputs in your input actions asset
        guardAction = InputSystem.actions.FindAction("Jump");
        attackActionL = InputSystem.actions.FindAction("AttackLeft");
        attackActionR = InputSystem.actions.FindAction("AttackRight");
    }

    private void OnEnable()
    {
        // assign the fishing trigger
        fishAreaTrigger = GameManager.instance.currentFishArea;

        // set the life/health of player for both game and UI
        playerCurrentLives = playerMaxLives;
        playerHealthBar.SetMaxHealth(playerMaxLives);
        playerHealthBar.SetHealth(playerCurrentLives);

        // set cooldown to zero
        leftCooldown.SetFillAmount(0);
        rightCooldown.SetFillAmount(0);
        nextLeftPunch = 0;
        nextRightPunch = 0;

        Debug.Log("Player health = " + playerCurrentLives);
    }
         
    // Update is called once per frame
    void Update()
    {
        // check if the player is guarding
        if (guardAction.IsPressed())
        {
            // change the bool
            isGuarding = true;

            // do the animation
        }
        else { isGuarding = false; }

        // decrease the cooldown
        nextLeftPunch -= Time.deltaTime;
        nextRightPunch -= Time.deltaTime;
        // play sound effect
        if (nextLeftPunch < 0) { FindObjectOfType<AudioManager>().Play("Cooldown"); }
        if (nextRightPunch < 0) { FindObjectOfType<AudioManager>().Play("Cooldown"); }

        // apply to UI
        leftCooldown.SetFillAmount(nextLeftPunch/playerAttackCooldown);
        rightCooldown.SetFillAmount(nextRightPunch/playerAttackCooldown);

        if (!isGuarding)
        {
            if (attackActionL.IsPressed() && nextLeftPunch <= 0)
            {
                // call the attack function passing in the left hand animator
                PlayerAttack(animatorLeftFist);
                // add cooldown
                nextLeftPunch = playerAttackCooldown;
            }
            if (attackActionR.IsPressed() && nextRightPunch <= 0)
            {
                // call the attack function passing in the right hand animator
                PlayerAttack(animatorRightFist);
                // add cooldown
                nextRightPunch = playerAttackCooldown;
            }
        }
        //Debug.Log("Player Health: " + playerCurrentLives);
    }

    public void PlayerAttack(Animator attackAnimator)
    {
        // apply damage to enemy using the enemy's take damage function
        enemyToBash.EnemyTakeDamage(playerDamage);

        // animate the attack
        attackAnimator.SetBool("Attack", true);
        // start a coroutine to wait till the animation is finished to switch the bool
        StartCoroutine(StopAnimations(attackAnimator, 0.5f));

        // play the sound
        FindObjectOfType<AudioManager>().Play("Heavy_Punch");
    }

    IEnumerator StopAnimations(Animator animator, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        animator.SetBool("Attack", false);
    }

    public void PlayerTakeDamage(int damageTaken)
    {
        // check if the player is guarding
        if (!isGuarding)
        {
            // reduce life/health
            playerCurrentLives -= damageTaken;

            Debug.Log("Damage Taken: " + damageTaken);

            // update in UI
            playerHealthBar.SetHealth(playerCurrentLives);

            // play damage sound and animation
            FindObjectOfType<AudioManager>().Play("Player_Hurt");

            // check if player has less than or 0 lives/health
            if (playerCurrentLives <= 0)
            {
                // kill the player
                StartCoroutine(PlayerDeath());
            }
        }
    }

    public IEnumerator PlayerDeath()
    {
        // play lose sound
        FindObjectOfType<AudioManager>().Play("Death");

        // update the fishing area trigger
        fishAreaTrigger.numOfFish -= 1;

        // show lose screen
        results.SetActive(true);
        resultsText.text = "KO You Lose";

        // stop enemy from attacking
        enemyToBash.fightEnd = true;

        // wait till deactivate
        yield return new WaitForSeconds(3);

        // deactivate the ui
        results.SetActive(false);
        fishBashUi.SetActive(false);
    }
}
