using UnityEngine;

[CreateAssetMenu(fileName = "NewToolType", menuName = "ItemTypes/Tool")]
public class ToolType : Item
{

    /* TO DO:
     * 1. Class is entirely incomplete, consider the implenetation details.
     * Current idea is that Tools unlock more plots, so more stuff can be planted at the same time?
     * 
     * 
     * 
     */


    public override void OnPurchase()
    {
        base.OnPurchase();
        Debug.Log($"Unlocking tool: {itemName}");
    }

}
