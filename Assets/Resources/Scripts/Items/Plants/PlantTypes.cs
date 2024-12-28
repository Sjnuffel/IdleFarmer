using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantType", menuName = "ItemTypes/Plant")]
public class PlantType : Item
{
    /* TO DO:
     * 1. Add a separate withering time, as I generally want this to stay fixed instead of also scaling along
     * 2. Whatever else comes to mind...
     */


    public int rank;                        // Rank of the plant (used to sort with for exampe)
    public int rewardPoints;                // Point reward of the plant;
    public float stageTime;                 // Time (in seconds) required per growth stage

    public Sprite seedBagIcon;              // Icon of the seed bag
    public Sprite cropSignSmall;            // Small crop sign
    public Sprite cropsignLarge;            // Large crope sign

    public Sprite[] growthSprites;          // Array of sprites for each growth stage

    private bool debug = false;

    public enum RewardCurrency              // Type of currency the reward is in
    {
        GP,
        FP
    }

    public override void OnPurchase()
    {
        base.OnPurchase();
        if (debug)
            Debug.Log($"Unlocking plant: {itemName}");
    }
}