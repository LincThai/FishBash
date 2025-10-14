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
}
