using UnityEngine;
using UnityEngine.InputSystem;

public class FishingAreaTrigger : MonoBehaviour
{
    // set variables
    public Fish catchableFish;
    public int numOfFish;

    // Inputs
    private InputAction interactAction;

    // minigame reference
    public GameObject fishBashUI;

    private void Awake()
    {
        // subscribe to the input in your input actions asset
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // set this trigger to these variables
            GameManager.instance._EnemyFishBashed.fishingAreaTrigger = this;
            GameManager.instance._PlayerFishBash.fishAreaTrigger = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // check if it is the player and the fish scriptableObject is assigned
        if (other.CompareTag("Player") && catchableFish != null)
        {
            Debug.Log("Player entered Fishing Area");
            // check if there are any fish left and for input
            if (numOfFish > 0)
            {
                Debug.Log("There is fish here");
                if (interactAction.IsPressed())
                {
                    // activate fishing mode.
                    Debug.Log("You Are Fishing!!!");
                    fishBashUI.SetActive(true);
                    OnFishingActionSFX();
                    // maybe call a function to update the number of fish
                }
            }
            else
            {
                // possibly call a destroy gameobject function or call it in update
                Debug.Log("NO FISH AVAILABLE!!!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // empty the values in these scripts
            GameManager.instance._EnemyFishBashed.fishingAreaTrigger = null;
            GameManager.instance._PlayerFishBash.fishAreaTrigger = null;
        }
    }

    private void OnFishingActionSFX()
    {

    }

}
