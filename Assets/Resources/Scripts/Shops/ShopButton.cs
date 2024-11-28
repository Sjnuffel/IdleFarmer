using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string plantNameText;
    private string plantDescriptionText;
    private string plantHarvestDetails;
    private int plantPrice;

    private PlantType plantType;
    private Shop shop;

    private ShopInfoPopUp shopInfoPopUp;

    public void Setup(PlantType plant)
    {
        plantType = plant;

        plantNameText = plant.plantName;
        plantDescriptionText = plant.plantDescription;
        plantHarvestDetails = plant.plantHarvestDetails;
        plantPrice = plant.plantPrice;

        Debug.Log($"Plant: {plantNameText}, Description: {plantDescriptionText}, Harvest: {plantHarvestDetails},  Price: {plantPrice}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer entered {plantNameText}");

        if (ShopInfoPopUp.Instance != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            ShopInfoPopUp.Instance.Show(
                plantNameText,
                plantDescriptionText,
                plantHarvestDetails,
                $"Cost: {plantPrice} gp",
                mousePosition
            );
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer exited {plantNameText}");

        if (ShopInfoPopUp.Instance != null)
            ShopInfoPopUp.Instance.Hide();
    }

    public void OnButtonClick()
    {
        shop.BuyPlant(plantType);
    }
}
