using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Fish> caughtFishList = new List<Fish>();

    public void AddFish(Fish fish)
    {
        caughtFishList.Add(fish);
    }

    public void RemoveFish(Fish fish) 
    {  
        caughtFishList.Remove(fish);
    }

    private void Update()
    {
        // check if there are any fish
        if (caughtFishList.Count > 0)
        {
            // then loop through all the caught fish
            for (int i = 0; i < caughtFishList.Count; i++)
            {
                // call the submit quest function in the game manager to check
                GameManager.instance.SubmitQuest(caughtFishList[i]);
            }
        }
    }
}
