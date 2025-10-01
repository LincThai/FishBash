using UnityEngine;
using UnityEngine.InputSystem;

public class FishingAreaTrigger : MonoBehaviour
{
    // set variables
    public Fish catchableFish;
    public int numOfFish;

    // Inputs
    private InputAction interactAction;

    // popup reference
    public GameObject popupInteractInput;
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
            GameManager.instance.currentFishArea = this;

            if (numOfFish > 0)
            {
                // open a pop up to display the key to start fishing
                popupInteractInput.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // check if it is the player and the fish scriptableObject is assigned
        if (other.CompareTag("Player") && catchableFish != null)
        {
            // check if there are any fish left and for input
            if (numOfFish > 0)
            {
                if (interactAction.IsPressed())
                {
                    // activate fishing mode.
                    fishBashUI.SetActive(true);
                    popupInteractInput.SetActive(false);
                    OnFishingActionSFX();
                    // maybe call a function to update the number of fish
                }
            }
            else
            {
                // turn off popup
                popupInteractInput.SetActive(false);

                // when there is no more fish
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // empty the values in these scripts
            GameManager.instance.currentFishArea = null;

            if (numOfFish <= 0)
            {
                // turn off popup
                popupInteractInput.SetActive(false);

                // when there is no more fish
                Destroy(gameObject);
            }
            else 
            {
                // turn off popup
                popupInteractInput.SetActive(false);
            }
        }
    }

    private void OnFishingActionSFX()
    {

    }

}
