using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantType", menuName = "ItemTypes/Plant")]
public class PlantType : Item
{

    public int rank;                        // Rank of the plant (used to sort with for exampe)
    public int rewardPoints;                // Point reward of the plant;
    public float stageTime;                 // Time (in seconds) required per growth stage

    public Sprite seedBagIcon;              // Icon of the seed bag
    public Sprite cropSignSmall;            // Small crop sign
    public Sprite cropsignLarge;            // Large crope sign

    public Sprite[] growthSprites;          // Array of sprites for each growth stage

    public enum RewardCurrency              // Type of currency the reward is in
    {
        GP,
        FP
    }

    public override void OnPurchase()
    {
        base.OnPurchase();
        Debug.Log($"Unlocking plant: {itemName}");
    }
}