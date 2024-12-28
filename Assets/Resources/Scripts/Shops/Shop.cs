using UnityEngine;

public class Shop : MonoBehaviour
{

    public Transform shopUIParent;          // parent object for all shop buttons
    public GameObject shopButtonPrefab;     // prefab button object

    private bool debug = false;

    public void BuyItem(Item item)
    {
        if (ResourceManager.instance.totalGrowthPoints >= item.itemCost)
        {
            ResourceManager.instance.totalGrowthPoints -= item.itemCost;

            if (debug)
                Debug.Log($"Bought {item.itemName}");

        }
        else
        {
            Debug.Log("Not enough growth points!");
        }
    }
}
