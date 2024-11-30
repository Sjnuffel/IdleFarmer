using UnityEngine;
using TMPro;
using System.Collections.Generic;

/* <ResourceManager>
 * Primary Responsibility: Manages player-facing resources.
 * 
 * Examples:
 * - Tracking resources like coins, seeds, or growth points.
 * - Handling resource updates (e.g., spending coins, gaining rewards).
 * - Communicating changes to the UI so players can see their resource count */

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public TextMeshProUGUI growthPointsText;
    public TextMeshProUGUI farmRankText;

    public int totalGrowthPoints = 0;
    public int currentFarmRank = 0;

    public List<PlantType> unlockedPlantTypes; 
    public List<FertilizerType> unlockedFertilizerTypes;
    public List<ToolType> unlockedToolTypes;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        
        else
        {
            Debug.LogWarning("Multiple ResourceManager instances detected destroying extra instance");
            Destroy(gameObject);
        }

        unlockedPlantTypes = new List<PlantType>();
        unlockedFertilizerTypes = new List<FertilizerType>();
        unlockedToolTypes = new List<ToolType>();
    }

    public void AddGrowthPoints(int points)
    {
        totalGrowthPoints += points;
        Debug.Log($"Growthpoints updated: {totalGrowthPoints}");
        UpdateUI();
    }

    public void IncreaseFarmRank()
    {
        currentFarmRank++;
    }

    public void UpdateUI()
    {
        if (growthPointsText != null)
            growthPointsText.text = $"GP: {totalGrowthPoints}";

        if (farmRankText != null)
            farmRankText.text = $"Rank: {currentFarmRank}"; 
    }

    public void UnlockItem(List<Item> list, Item item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
            Debug.Log($"{item.itemName} has been unlocked!");
        }
    }

    public bool IsItemUnlocked(List<Item> list, Item item)
    {
        return list.Contains(item);
    }
}