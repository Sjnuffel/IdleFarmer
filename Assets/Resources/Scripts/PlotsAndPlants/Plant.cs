using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public GrowthStage currentStage = GrowthStage.Seed;
    public PlantType plantType;

    private float stageTime;
    private float growthTimer = 0f;

    private Sprite[] growthSprites; // Array of sprites containing stages
    private Image image; // while the sprite renderer can loop through, we have to set the image to see our sprite update
    private SpriteRenderer spriteRenderer;

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

    // Start is called before the first frame update
    void Start()
    {

        growthSprites = plantType.growthSprites;
        stageTime = plantType.stageTime;

        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
            Debug.Log("SpriteRenderer component not found on this object!");


        if (!TryGetComponent<Image>(out image))
            Debug.Log("Image component not found on this object");

        UpdateVisuals();
    }

    public void AdvanceGrowthExternally(float deltaTime)
    {
        growthTimer += deltaTime;

        if (growthTimer >= stageTime)
            AdvanceGrowth();
    }

    public void HarvestPlant()
    {
        if (currentStage == GrowthStage.Mature)
        {
            Debug.Log($"Harvested {plantType.plantName}, earned {plantType.harvestPoints} growth points!");
            ResourceManager.instance.AddGrowthPoints(plantType.harvestPoints);

            ResetPlant();
        } 
        else if ( currentStage == GrowthStage.Withered)
        {
            Debug.Log($"{plantType.plantName} has unfortunately withered.");

            ResetPlant();
        } 
        else
        {
            Debug.Log($"Plant is not ready for harvest yet.");
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
