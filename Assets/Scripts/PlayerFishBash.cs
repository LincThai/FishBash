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
    public float playerAttackCooldown = 3f;
    public bool isGuarding = false;

    private float nextLeftPunch;
    private float nextRightPunch;

    // references
    [Header("References")]
    public GameObject fishBashUi;
    public HealthBar playerHealthBar;
    public EnemyFishBashed enemyToBash;
    public FishingAreaTrigger fishAreaTrigger;

    // inputs
    private InputAction guardAction;
    private InputAction attackActionL;
    private InputAction attackActionR;

    private void Awake()
    {
        // connect to game manager
        GameManager.instance._PlayerFishBash = this;

        // subscribe to the inputs in your input actions asset
        guardAction = InputSystem.actions.FindAction("Jump");
        attackActionL = InputSystem.actions.FindAction("AttackLeft");
        attackActionR = InputSystem.actions.FindAction("AttackRight");
    }

    private void OnEnable()
    {
        // connect to game manager
        GameManager.instance._PlayerFishBash = this;

        fishAreaTrigger = GameManager.instance.currentFishArea;

        // set the life/health of player for both game and UI
        playerCurrentLives = playerMaxLives;
        playerHealthBar.SetMaxHealth(playerMaxLives);
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

        if (attackActionL.IsPressed() && Time.time >= nextLeftPunch)
        {
            // call the attack function passing in the left hand animation
            PlayerAttack();
            // add cooldown
            nextLeftPunch = Time.time + playerAttackCooldown;
        }
        if (attackActionR.IsPressed() && Time.time >= nextRightPunch)
        {
            // call the attack function passing in the right hand animation
            PlayerAttack();
            // add cooldown
            nextRightPunch = Time.time + playerAttackCooldown;
        }
    }

    public void PlayerAttack()
    {
        // apply damage to enemy using the enemy's take damage function
        enemyToBash.EnemyTakeDamage(playerDamage);

        // animate the attack

        // play the sound

    }

    public void PlayerTakeDamage(int damageTaken)
    {
        // check if the player is guarding
        if (!isGuarding)
        {
            // reduce life/health
            playerCurrentLives -= damageTaken;
            Debug.Log("Player Lives = " + playerCurrentLives);

            // update in UI
            playerHealthBar.SetHealth(playerCurrentLives);

            // play damage sound and animation
        
            // check if player has less than or 0 lives/health
            if (playerCurrentLives <= 0)
            {
                // kill the player
                Debug.Log("Player Died");
                PlayerDeath();
            }
        }
    }

    public void PlayerDeath()
    {
        // play lose sound

        // update the fishing area trigger
        fishAreaTrigger.numOfFish -= 1;

        // deactivate the ui
        fishBashUi.SetActive(false);
    }
}
