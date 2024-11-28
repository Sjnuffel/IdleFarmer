using UnityEngine;

public class ShopItem : MonoBehaviour
{

    public int cost = 10;

    public void BuyPlant()
    {
        if (ResourceManager.instance.totalGrowthPoints > cost)
        {
            ResourceManager.instance.totalGrowthPoints -= cost;
            ResourceManager.instance.UpdateUI();
            Debug.Log("Plant purchased");
        }
        else
        {
            Debug.Log("Not Enough Growth Points");
        }
    }

}
