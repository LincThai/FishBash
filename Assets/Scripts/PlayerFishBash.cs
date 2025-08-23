using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishBash : MonoBehaviour
{
    // set variables
    // player variables
    public int playerLives = 3;
    public int playerDamage = 1;
    public int playerAttackCooldown = 5;
    public bool isGuarding = false;

    // references
    public GameObject fishBashUi;
    public GameObject fishAreaTrigger;

    // inputs
    private InputAction guardAction;
    private InputAction AttackActionL;
    private InputAction AttackActionR;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTakeDamage(int damageTaken)
    {
        // check if the player is guarding
        if (!isGuarding)
        {
            // reduce life/health
            playerLives -= damageTaken;

            // check if player has less than or 0 lives/health
            if (playerLives <= 0)
            {
                // kill the player
                Debug.Log("Player Died");
                PlayerDeath();
            }
        }
    }

    public void PlayerDeath()
    {
        // deactivate the ui
        fishBashUi.SetActive(false);

        // update the fishing area trigger
    }
}
