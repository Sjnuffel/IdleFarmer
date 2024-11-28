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

    public void RegisterPlant(Plant plant)
    {
        if (!plants.Contains(plant))
        {
            plants.Add(plant);
            Debug.Log($"Plant registered: {plant.name}");
        }
    }

    public void UnregisterPlant(Plant plant)
    {
        if (plants.Contains(plant))
        {
            plants.Remove(plant);
        }
    }

    // assign a plant and an ID into the plantedPlants Dictionary
    public void AssignPlantToPlot(int plotID, GameObject plant)
    {
        plantedPlants[plotID] = plant;
    }

    public GameObject GetPlantInPlot(int plotID)
    {
        return plantedPlants.ContainsKey(plotID) ? plantedPlants[plotID] : null;
    }
}
    