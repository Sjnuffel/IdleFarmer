using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantType", menuName = "Plants/PlantType")]
public class PlantType : ScriptableObject
{

    public string plantName;                // Name of the plant
    public string plantDescription;         // Description of the plant
    public string plantHarvestDetails;      // Details about the point gains when harvesting this plant
    
    public int plantRank;                   // Order in which the plant can be unlocked
    public int plantPrice;                  // Cost of the plant's seed to unlock
    public int harvestPoints;               // number of points each plant generates upon harvest
    public float stageTime;                 // time required per growth stage

    public Sprite shopIcon;                 // Icon presented in the shop
    public Sprite seedBagIcon;              // Icon of the seed bag
    public Sprite cropSignSmall;            // Small crop sign
    public Sprite cropsignLarge;            // Large crope sign

    public Sprite[] growthSprites;          // Array of sprites for each growth stage

    public enum PlantCurrency               // Type of currency the plant has to be unlocked with
    {
        GP,
        FP
    }
}