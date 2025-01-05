using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private string shopItemNameText;
    private string shopItemDescriptionText;
    private string shopItemDetailsText;
    private int shopItemPriceText;

    private ShopManager shopManager;
    private Item shopItem;

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private UnityEngine.UI.Image buttonIcon;

    private readonly bool debug = false;

    public void SetupButton(Item item, ShopManager manager)
    {
        shopItem = item;

        shopItemNameText = item.itemName;
        shopItemDescriptionText = item.itemDescription;
        shopItemDetailsText = item.itemDetails;
        shopItemPriceText = item.itemCost;

        shopManager = manager;

        GetComponentInChildren<TextMeshProUGUI>().text = shopItemNameText;

        buttonText.text = shopItemNameText;
        buttonIcon.sprite = item.itemShopIcon;

        if (debug)
            Debug.Log($"Plant: {shopItemNameText}, Description: {shopItemDescriptionText}, Harvest: {shopItemDetailsText},  Price: {shopItemPriceText}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (debug)
            Debug.Log($"Pointer entered {shopItemNameText}");

        if (ShopInfoPopUp.instance != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            ShopInfoPopUp.instance.Show(
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
        if (debug)
            Debug.Log($"Pointer exited {shopItemNameText}");

        if (ShopInfoPopUp.instance != null)
            ShopInfoPopUp.instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // generalized item purchase
        shopManager.BuyItem(shopItem);
    }
}
