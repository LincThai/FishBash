using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // set variables
    public FishingAreaTrigger currentFishArea;

    public EnemyFishBashed _EnemyFishBashed;
    public PlayerFishBash _PlayerFishBash;

    private void Awake()
    {
        // singleton to ensure there is only one gamemanager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
