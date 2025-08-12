using UnityEngine;

public class FishingAreaTrigger : MonoBehaviour
{
    // set variables
    public Fish catchableFish;
    public int numOfFish;

    private void OnTriggerStay(Collider other)
    {
        // check if it is the player and the fish scriptableObject is assigned
        if (other.tag == "Player" && catchableFish != null)
        {
            // check if there are any fish left and for input
            if (numOfFish > 0)
            {
                // activate fishing mode.
                Debug.Log("You Are Fishing!!!");
            }
            else
            {
                Debug.Log("NO FISH AVAILABLE!!!");
            }
        }
    }

}
