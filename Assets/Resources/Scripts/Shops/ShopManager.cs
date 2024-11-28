using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject shopPanel;
    public GameObject shopGridPanel;
    public GameObject shopButtonPrefab;
    public List<PlantType> availablePlantTypes;

    public void ToggleShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(!shopPanel.activeSelf);

            if (shopPanel.activeSelf)
                PopulateShopGrid();
        }
    }

    public void PopulateShopGrid()
    {
        // Clear existing buttons (if any)
        foreach (Transform child in shopGridPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // sort the items by rank, lowest to highest
        availablePlantTypes.Sort((x, y) => x.plantRank.CompareTo(y.plantRank));

        // Loop through available plant types
        foreach (PlantType plant in availablePlantTypes)
        {
            // Only show plants with a rank greater than the player's current rank
            if (plant.plantRank > ResourceManager.instance.currentFarmRank)
            {
                AddPlantShopButton(plant);
            }
        }
    }

    public void AddPlantShopButton(PlantType plant)
    {
        GameObject button = Instantiate(shopButtonPrefab, shopGridPanel.transform);
        ShopButton shopButton = button.GetComponent<ShopButton>();
        
        Image image = button.GetComponent<Image>();

        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null)
        {
            buttonText.text = $"{plant.name}";
            image.sprite = plant.shopIcon;
        }

        if (button.TryGetComponent<Button>(out var buttonComponent))
            buttonComponent.onClick.AddListener(() => BuyPlant(plant));

        shopButton.Setup(plant);
    }

    public void BuyPlant(PlantType plant)
    {
        if (ResourceManager.instance.totalGrowthPoints >= plant.plantPrice)
        {
            ResourceManager.instance.totalGrowthPoints -= plant.plantPrice;
            ResourceManager.instance.UpdateUI();
            Debug.Log($"Purchased {plant.name}!");

            // ResourceManager.instance.UnlockPlant(plant);
        } 
        else
        {
            Debug.Log($"Not enough GP");
        }
    }
}
