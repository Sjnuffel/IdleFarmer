using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public enum ShopCategory { Seeds, Fertilizer, Tools };
    public ShopCategory currentCategory = ShopCategory.Seeds;

    [Header("Array Elements")]
    public GameObject[] categoryGrids; // array containing the various Shop Grids
    public Button[] tabButtons; // buttons for selecting categories

    [Header("UI Panel Elements")]
    public GameObject shopPanel;
    public GameObject shopGridPanel;
    public GameObject shopButtonPrefab;

    public TextMeshProUGUI shopTitle;

    [Header("List Elements")]
    public List<PlantType> availableSeeds;
    public List<FertilizerType> availableFertilizers;
    public List<ToolType> availableTools; // TO DO: Needs to be changed to "ToolType", but doesn't exist yet

    private bool debug = false;

    public void Start()
    {
        // Initialize with the first category
        SelectCategory((int)ShopCategory.Seeds);
    }

    public void ToggleShop()
    {
        if (shopPanel != null)
        {
            SetTabsInteractable(true);
            shopPanel.SetActive(!shopPanel.activeSelf);

            // only populate the grid when it's actually active
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
            {
                UnlockPlant(plant);
                availableSeeds.Remove(plant);
            }


            else if (item is FertilizerType fertilizer)
            {
                UnlockFertilizer(fertilizer);
                availableFertilizers.Remove(fertilizer);
            }


            else if (item is ToolType tool) 
            {
                UnlockTool(tool);
                availableTools.Remove(tool);
            }

            UpdateShopContent();
            ResourceManager.instance.UpdateUI();
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
    /// Allow other scripts to enable/disable the various tab buttons.
    /// </summary>
    /// <param name="state">Boolean to enable or disable interactability</param>
    public void SetTabsInteractable(bool state)
    {
        foreach (Button tab in tabButtons)
        {
            tab.gameObject.SetActive(state);
            tab.interactable = state;
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

            // to do: expand on fertilizer class, also has to unlock with rank
            case ShopCategory.Fertilizer:
                PopulateGrid(availableFertilizers, categoryGrids[(int)ShopCategory.Fertilizer]);
                shopTitle.text = "Fertilizer Depot";
                break;

            // to do: expand on tool class, does basically nothing yet
            case ShopCategory.Tools:
                PopulateGrid(availableTools, categoryGrids[(int)ShopCategory.Tools]);
                shopTitle.text = "Tool Store";
                break;
        }
    }

    // Private SHOP functions (buying various items)

    /// <summary>
    /// Setup a shop's grid by providing the list of items and the grid to present it on. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">Array of items such as plants, fertilizers or tools</param>
    /// <param name="grid">Grid to present the items on, such as ShopSeedGrid, ShopFertilizerGrid</param>
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
        if (debug)
            Debug.Log($"ShopManager.UnlockPlant - Plant unlocked: {plant.itemName}");

        ResourceManager.instance.unlockedPlantTypes.Add(plant);
    }

    private void UnlockFertilizer(FertilizerType fertilizer)
    {
        if (debug)
            Debug.Log($"ShopManager.UnlockFertilizer - Fertilizer unlocked: {fertilizer.itemName}");
        
        ResourceManager.instance.unlockedFertilizerTypes.Add(fertilizer);
    }

    private void UnlockTool(ToolType tool)
    {
        if (debug)
            Debug.Log($"ShopManager.UnlockTool - Tool unlocked: {tool.itemName}");
            
        ResourceManager.instance.unlockedToolTypes.Add(tool);
    }

    private void UnlockPlot()
    {
        Debug.Log("ShopManager.UnlockPlot - Not Implemented Yet");
    }

    private void UnlockGrid()
    {
        Debug.Log("ShopManager.UnlockGrid - Not Implemented yet");
    }
}
