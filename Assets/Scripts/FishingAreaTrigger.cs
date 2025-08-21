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
    public GameObject FishBashUI;

    private void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
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
                if (interactAction.WasPressedThisFrame())
                {
                    // activate fishing mode.
                    Debug.Log("You Are Fishing!!!");
                    FishBashUI.SetActive(true);
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

}
