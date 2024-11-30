using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public enum ShopCategory { Seeds, Fertilizer, Tools };
    public ShopCategory currentCategory = ShopCategory.Seeds;

    public GameObject[] categoryGrids; // array containing the various Shop Grids
    public Button[] tabButtons; // buttons for selecting categories

    public GameObject shopPanel;
    public GameObject shopGridPanel;
    public GameObject shopButtonPrefab;

    public TextMeshProUGUI shopTitle;

    public List<PlantType> availableSeeds;
    public List<FertilizerType> availableFertilizers;
    public List<PlantType> availableTools; // TO DO: Needs to be changed to "ToolType", but doesn't exist yet

    public void Start()
    {
        // Initialize with the first category
        SelectCategory((int)ShopCategory.Seeds);
    }

    public void ToggleShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(!shopPanel.activeSelf);

            if (shopPanel.activeSelf)
                PopulateGrid(availableSeeds, categoryGrids[(int)currentCategory]);
        }
    }

    /// <summary>
    /// Buy an item from one of the shop tabs and add it to unlockable list of it's specific type.
    /// Currently: Tools, Fertilizers and Plants
    /// </summary>
    /// <param name="item">Accepts a base item object as input</param>
    public void BuyItem(Item item)
    {
        if (ResourceManager.instance.totalGrowthPoints >= item.itemCost)
        {
            ResourceManager.instance.totalGrowthPoints -= item.itemCost;

            // call the item's onPurchase method
            item.OnPurchase();

            // handle sub-class specific logic
            if (item is PlantType plant)
                UnlockPlant(plant);

            else if (item is FertilizerType fertilizer)
                UnlockFertilizer(fertilizer);

            else if (item is ToolType tool)
                UnlockTool(tool);

            ResourceManager.instance.UpdateUI();

            UpdateShopContent();
        } 

        else
        {
            Debug.Log($"Not enough GP to buy {item.itemName}");
        }
    }

    /// <summary>
    /// Select the shop's category to allow tab switching and populate various shop grids with items to buy
    /// </summary>
    /// <param name="categoryIndex">Index (int) of the category in question</param>
    public void SelectCategory(int categoryIndex)
    {
        currentCategory = (ShopCategory)categoryIndex;

        // update the shop grid and title
        UpdateShopContent();

        for (int i = 0; i < categoryGrids.Length; i++)
        {
            categoryGrids[i].SetActive(i == categoryIndex);
        }

        // highlight the selected tab
        for (int i = 0; i < tabButtons.Length; i++) 
        {
            var colors = tabButtons[i].colors;
            colors.normalColor = (i == categoryIndex) ? Color.green : Color.white; 
            tabButtons[i].colors = colors;
        }
    }

    /// <summary>
    /// Returns the content of the selected category, to show specific buttons to buy specific things
    /// </summary>
    private void UpdateShopContent()
    {
        switch (currentCategory)
        {
            case ShopCategory.Seeds:
                availableSeeds.Sort((x, y) => x.rank.CompareTo(y.rank));
                PopulateGrid(availableSeeds, categoryGrids[(int)ShopCategory.Seeds]);
                shopTitle.text = "Seed Store";
                break;

            case ShopCategory.Fertilizer:
                PopulateGrid(availableFertilizers, categoryGrids[(int)ShopCategory.Fertilizer]);
                shopTitle.text = "Fertilizer Depot";
                break;

            case ShopCategory.Tools:
                PopulateGrid(availableTools, categoryGrids[(int)ShopCategory.Tools]);
                shopTitle.text = "Tool Store";
                break;
        }
    }

    // Private SHOP functions (buying various items)

    private void PopulateGrid<T>(List<T> items, GameObject grid)
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        // populate the grid with the category's items
        foreach (T item in items)
        {
            GameObject button = Instantiate(shopButtonPrefab, grid.transform);
            var shopButton = button.GetComponent<ShopButton>();

            // customize the button based on the item type
            if (item is PlantType plant)
                shopButton.SetupButton(plant, this);

            else if (item is FertilizerType fertilizer)
                shopButton.SetupButton(fertilizer, this);

            else if (item is ToolType tool)
                shopButton.SetupButton(tool, this);
        }
    }

    private void UnlockPlant(PlantType plant)
    {
        Debug.Log($"Plant unlocked: {plant.itemName}");
        ResourceManager.instance.unlockedPlantTypes.Add(plant);
    }

    private void UnlockFertilizer(FertilizerType fertilizer)
    {
        Debug.Log($"Fertilizer unlocked: {fertilizer.itemName}");
        ResourceManager.instance.unlockedFertilizerTypes.Add(fertilizer);
    }

    private void UnlockTool(ToolType tool)
    {
        Debug.Log($"Tool unlocked: {tool.itemName}");
        ResourceManager.instance.unlockedToolTypes.Add(tool);
    }
}
