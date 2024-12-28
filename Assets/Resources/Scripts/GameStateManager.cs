using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    // Persistent game data
    public int playerMoney;
    public int playerExperience;

    private bool debug = true;

    // Start is called before the first frame update
    void Awake()
    {
        // ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        } else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Save the players gameplay data to the PlayerPrefs
    /// </summary>
    public void SaveGameState()
    {
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
        PlayerPrefs.SetInt("PlayerExperience", playerExperience);

        if (debug)
            Debug.Log("Game state saved!");
    }
    
    /// <summary>
    /// Loada the players gameplay data from the PlayerPrefs
    /// </summary>
    public void LoadGameState()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        playerExperience = PlayerPrefs.GetInt("PlayerExperience", 0);

        if (debug)
            Debug.Log("Game state loaded!");
    }
}
