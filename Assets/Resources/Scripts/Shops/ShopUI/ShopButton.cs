using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private string shopItemNameText;
    private string shopItemDescriptionText;
    private string shopItemDetailsText;
    private int shopItemPriceText;

    private ShopManager shopManager;
    private Item shopItem;
    private Shop shop;

    private ShopInfoPopUp shopInfoPopUp;

    public void SetupButton(Item item, ShopManager manager)
    {
        shopItem = item;

        shopItemNameText = item.itemName;
        shopItemDescriptionText = item.itemDescription;
        shopItemDetailsText = item.itemDetails;
        shopItemPriceText = item.itemCost;

        shopManager = manager;

        GetComponent<UnityEngine.UI.Image>().sprite = item.itemShopIcon;

        Debug.Log($"Plant: {shopItemNameText}, Description: {shopItemDescriptionText}, Harvest: {shopItemDetailsText},  Price: {shopItemPriceText}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer entered {shopItemNameText}");

        if (ShopInfoPopUp.Instance != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            ShopInfoPopUp.Instance.Show(
                shopItemNameText,
                shopItemDescriptionText,
                shopItemDetailsText,
                $"Cost: {shopItemPriceText} gp",
                mousePosition
            );
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer exited {shopItemNameText}");

        if (ShopInfoPopUp.Instance != null)
            ShopInfoPopUp.Instance.Hide();
    }

    public void OnButtonClick()
    {
        shop.BuyItem(shopItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // generalized item purchase
        shopManager.BuyItem(shopItem);
    }
}
