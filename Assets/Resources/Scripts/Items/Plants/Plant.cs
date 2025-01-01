using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public GrowthStage currentStage = GrowthStage.Seed;
    public PlantType plantType;

    private float stageTime;
    private float witherTime;
    private float growthTimer = 0f;
    private float witherTimer = 0f;

    private Sprite[] growthSprites; // Array of sprites containing stages
    private Image image; // while the sprite renderer can loop through, we have to set the image to see our sprite update
    private SpriteRenderer spriteRenderer;

    private bool debug = false;

    public enum GrowthStage
    {
        Seed,
        SmallSprout,
        LargeSprout,
        SmallSapling,
        LargeSapling,
        Mature,
        Withered
    }

    void Start()
    {

        growthSprites = plantType.growthSprites;
        stageTime = plantType.stageTime;
        witherTime = plantType.witherTime;

        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
            Debug.Log("SpriteRenderer component not found on this object!");


        if (!TryGetComponent<Image>(out image))
            Debug.Log("Image component not found on this object");

        UpdateVisuals();
    }

    /// <summary>
    /// Increase the growthtimer, so we know when to switch the stages. Keep separate track of the wither time as well.
    /// This way it stays a bit more stable instead of the same time (so if it's short you have to click/harvest very fast).
    /// </summary>
    /// <param name="deltaTime"></param>
    public void AdvanceGrowthExternally(float deltaTime)
    {
        growthTimer += deltaTime;

        if (growthTimer >= stageTime && currentStage != GrowthStage.Mature)
        {
            AdvanceGrowth();
        } 
        
        else if (currentStage == GrowthStage.Mature)
        {
            witherTimer += deltaTime;

            if (witherTimer >= witherTime)
                AdvanceGrowth();
        }

    }

    /// <summary>
    /// Method for harvesting the plant, when it's mature. If it's withered it's just reset.
    /// Else we just print a debug statement for now.
    /// </summary>
    public void HarvestPlant()
    {
        if (currentStage == GrowthStage.Mature)
        {
            if (debug) 
                Debug.Log($"Harvested {plantType.itemName}, earned {plantType.rewardPoints} growth points!");

            ResourceManager.instance.AddGrowthPoints(plantType.rewardPoints);
            ResetPlant();
        } 

        else if ( currentStage == GrowthStage.Withered)
        {
            if (debug)
                Debug.Log($"{plantType.itemName} has unfortunately withered.");

            ResetPlant();
        } 

        else
        {
            Debug.Log($"Plant is not ready for harvest yet.");
        }
    }

    /// <summary>
    /// Method to change to a new plant type so the correct sprints will be rendered.
    /// </summary>
    /// <param name="newPlantType">PlantType Scriptable Object</param>
    public void SetPlantType(PlantType newPlantType)
    {
        plantType = newPlantType;
        growthSprites = plantType.growthSprites;
        currentStage = GrowthStage.Seed;

        if (plantType.growthSprites.Length > 0)
        {
            spriteRenderer.sprite = plantType.growthSprites[0];
            UpdateVisuals();
        }
        else
        {
            Debug.LogError($"No growth sprites defined for {newPlantType.itemName}");
        }
    }

    // private functions


    // Advance the plant to it's next growth stage, based on it's growth timer
    private void AdvanceGrowth()
    {
        if (currentStage != GrowthStage.Withered)
        {
            currentStage++;
            growthTimer = 0f;
            UpdateVisuals();
        }
    }

    // Update the Plant's growth sprites
    private void UpdateVisuals()
    {
        if (spriteRenderer != null && (int)currentStage < growthSprites.Length)
        {
            spriteRenderer.sprite = growthSprites[(int)currentStage];
            image.sprite = spriteRenderer.sprite;

            if (debug) 
                Debug.Log($"Sprite updated to: {growthSprites[(int)currentStage].name}");

        } else
        {
            Debug.Log($"Growth Stage {currentStage} is out of bounds for the sprite array!");
        }
    }

    private void ResetPlant()
    {
        currentStage = GrowthStage.Seed;
        UpdateVisuals();
    }
}
