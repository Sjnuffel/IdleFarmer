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
    public int totalFertilizerPoints = 0;
    public int currentFarmRank = 0;

    public List<PlantType> unlockedPlantTypes;
    public List<FertilizerType> unlockedFertilizerTypes;
    public List<ToolType> unlockedToolTypes;

    private bool debug = true;

    public void Awake()
    {
        if (instance == null)
            instance = this;

        else
        {
            Debug.LogWarning("Multiple ResourceManager instances detected destroying extra instance");
            Destroy(gameObject);
        }

        unlockedFertilizerTypes = new List<FertilizerType>();
        unlockedToolTypes = new List<ToolType>();
    }

    /// <summary>
    /// Add/update the growth points variable
    /// </summary>
    /// <param name="points"></param>
    public void AddGrowthPoints(int points)
    {
        totalGrowthPoints += points;

        if (debug)
            Debug.Log($"Growthpoints updated: {totalGrowthPoints}");

        UpdateUI();
    }

    /// <summary>
    /// Add/update the growth points variable
    /// </summary>
    /// <param name="points"></param>
    public void AddFertilizerPoints(int points)
    {
        totalFertilizerPoints += points;

        if (debug)
            Debug.Log($"Growthpoints updated: {totalGrowthPoints}");

        UpdateUI();
    }

    /// <summary>
    /// Increase the current farm rank by 1
    /// </summary>
    public void IncreaseFarmRank()
    {
        currentFarmRank++;
    }

    /// <summary>
    /// Redraw the UI values
    /// </summary>
    public void UpdateUI()
    {
        if (growthPointsText != null)
            growthPointsText.text = $"GP: {totalGrowthPoints}";

        if (farmRankText != null)
            farmRankText.text = $"Rank: {currentFarmRank}";
    }

    /// <summary>
    /// Unlock an item in one of the various purchase lists
    /// </summary>
    /// <param name="list">The list you are unlocking the item from (seeds, fertilizers, tools)</param>
    /// <param name="item">The item being unlocked</param>
    public void UnlockItem(List<Item> list, Item item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);

            if (debug)
                Debug.Log($"ResourceManager.UnlockItem - {item.itemName} has been unlocked!");
        }
    }

    /// <summary>
    /// Check if the item is already unlocked
    /// </summary>
    /// <param name="plant">The plant on the list</param>
    /// <returns>bool</returns>
    public bool IsPlantUnlocked(PlantType plant)
    {
        if (unlockedPlantTypes.Contains(plant))
            return true;

        else
            return false;
    }

    /// <summary>
    /// Check if the item is already unlocked
    /// </summary>
    /// <param name="tool">The tool on the list</param>
    /// <returns>bool</returns>
    public bool IsToolUnlocked(ToolType tool)
    {
        if (unlockedToolTypes.Contains(tool))
            return true;

        else
            return false;
    }

    /// <summary>
    /// Check if the item is already unlocked
    /// </summary>
    /// <param name="fertilizer">The fertilizer on the list</param>
    /// <returns>bool</returns>
    public bool IsFertilizerUnlocked(FertilizerType fertilizer)
    {
        if (unlockedFertilizerTypes.Contains(fertilizer))
            return true;

        else 
            return false;   
    }
}