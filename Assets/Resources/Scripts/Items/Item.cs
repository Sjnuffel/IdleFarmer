using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantType", menuName = "ItemTypes/Item")]
public class Item : ScriptableObject
{

    public string itemName;                 // Name of the item
    public string itemDescription;          // Description of the item
    public string itemDetails;              // Additional information about the item

    public int itemCost;                    // Cost of the item

    public Sprite itemShopIcon;             // Icon presented in the shop

    public enum ItemCurrency                // Type of currency needed to unlock the item
    {
        GP,
        FP
    }

    public virtual void OnPurchase()
    {
        Debug.Log($"Purchased: {itemName}");
    }
}
