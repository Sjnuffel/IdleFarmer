using UnityEngine;

public class ToolType : Item
{

    // TO DO: define specific tool related properties

    public override void OnPurchase()
    {
        base.OnPurchase();
        Debug.Log($"Unlocking fertilizer: {itemName}");
    }

}
