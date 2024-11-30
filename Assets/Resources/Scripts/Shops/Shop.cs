using UnityEngine;

public class Shop : MonoBehaviour
{

    public Transform shopUIParent;          // parent object for all shop buttons
    public GameObject shopButtonPrefab;     // prefab button object

    public void BuyItem(Item item)
    {
        if (ResourceManager.instance.totalGrowthPoints >= item.itemCost)
        {
            ResourceManager.instance.totalGrowthPoints -= item.itemCost;
            Debug.Log($"Bought {item.itemName}");

            // unlock the plant // allow it to be planted
        }
        else
        {
            Debug.Log("Not enough growth points!");
        }
    }
}
