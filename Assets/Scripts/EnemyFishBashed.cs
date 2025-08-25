using UnityEngine;

public class EnemyFishBashed : MonoBehaviour
{
    // set variables
    // enemy Variables
    [Header("Editable Variables")]
    public int enemyMaxHealth = 3;
    public int enemyCurrentHealth;
    public int enemyDamage = 1;

    // references
    [Header("References")]
    public GameObject fishBashUI;
    public FishingAreaTrigger fishingAreaTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTakeDamage(int damage)
    {
        // reduces enemies health
        enemyCurrentHealth -= damage;

        // play enemy damaged sound and animation

        if (enemyCurrentHealth <= 0)
        {

        }
    }

    public void EnemyDeath()
    {
        // close the minigame/UI
        fishBashUI.SetActive(false);

        // update the fishing area trigger
        fishingAreaTrigger.numOfFish -= 1;
    }

}
