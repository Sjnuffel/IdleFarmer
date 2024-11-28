using UnityEngine;

public class Shop : MonoBehaviour
{

    public Transform shopUIParent;          // parent object for all shop buttons
    public GameObject shopButtonPrefab;     // prefab button object

    public void BuyPlant(PlantType plant)
    {
        if (ResourceManager.instance.totalGrowthPoints >= plant.harvestPoints)
        {
            ResourceManager.instance.totalGrowthPoints -= plant.harvestPoints;
            Debug.Log($"Bought {plant.plantName}");

            // unlock the plant // allow it to be planted
        }
        else
        {
            Debug.Log("Not enough growth points!");
        }
    }
}
