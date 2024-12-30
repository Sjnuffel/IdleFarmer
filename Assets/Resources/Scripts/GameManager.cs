using System.Collections.Generic;
using UnityEngine;

/* GameManager
 * Primary Responsibility: Manages global game state and backend mechanics.
 * 
 * Examples:
 * - Tracking plot-plant relationships
 * - Keeping a list of active plots, plants, or other game objects.
 * - Handling game-wide events (e.g., weather, save/load operations). */

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Dictionary<int, GameObject> plantedPlants = new Dictionary<int, GameObject>();
    private List<Plant> plants = new();

    private bool debug = false;


    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        foreach (Plant plant in plants)
        {
            plant.AdvanceGrowthExternally(Time.deltaTime);
        }
    }

    /// <summary>
    /// Add a plant to the plot
    /// </summary>
    /// <param name="plant">The plant we are planting in the plot</param>
    public void RegisterPlant(Plant plant)
    {
        if (!plants.Contains(plant))
        {
            plants.Add(plant);
            
            if (debug)
                Debug.Log($"Plant registered: {plant.name}");
        }
    }

    /// <summary>
    /// Remove a plant from the plot
    /// </summary>
    /// <param name="plant">The plant we are removing from the plot</param>
    public void UnregisterPlant(Plant plant)
    {
        if (plants.Contains(plant))
        {
            plants.Remove(plant);

            if (debug)
                Debug.Log($"Plant unregistered: {plant.name}");
        }
    }

    /// <summary>
    /// Assign a plant and an ID into the plantedPlants Dictionary
    /// </summary>
    /// <param name="plotID">ID of the plot the plant is assigned to</param>
    /// <param name="plant">The plant object being planted</param>
    public void AssignPlantToPlot(int plotID, GameObject plant)
    {
        plantedPlants[plotID] = plant;
    }

    /// <summary>
    /// Get the plant in the current plot, found by plot ID.
    /// </summary>
    /// <param name="plotID">The plot ID</param>
    /// <returns></returns>
    public GameObject GetPlantInPlot(int plotID)
    {
        return plantedPlants.ContainsKey(plotID) ? plantedPlants[plotID] : null;
    }
}
    