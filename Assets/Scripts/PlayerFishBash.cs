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
    public int playerAttackCooldown = 5;
    public bool isGuarding = false;

    // references
    [Header("References")]
    public GameObject fishBashUi;
    public GameObject playerHealthBar;
    public EnemyFishBashed enemyToBash;
    public FishingAreaTrigger fishAreaTrigger;

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
        playerCurrentLives = playerMaxLives;
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

        if (attackActionL.IsPressed())
        {
            // call the attack function passing in the left hand animation
            PlayerAttack();
        }
        if (attackActionR.IsPressed())
        {
            // call the attack function passing in the right hand animation
            PlayerAttack();
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

        // deactivate the ui
        fishBashUi.SetActive(false);

        // update the fishing area trigger
    }
}
