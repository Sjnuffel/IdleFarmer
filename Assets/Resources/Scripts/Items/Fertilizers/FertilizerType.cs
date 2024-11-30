using UnityEngine;

[CreateAssetMenu(fileName = "NewFertilizerType", menuName = "ItemTypes/Fertilizer")]
public class FertilizerType : Item
{

    public int rank;                        // Order in which the fertilizer can be unlocked
    public int rewardPoints;                // number of points each fertilizer generates upon harvest

    public enum RewardCurrency              // Type of currency the fertilizer has to be unlocked with
    {
        GP,
        FP
    }
}