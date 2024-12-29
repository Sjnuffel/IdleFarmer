using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlotSlotManager : MonoBehaviour
{
    // Manages the plot contents for all plots available split across multiple plots.

    public static PlotSlotManager instance;

    public PlotSlot[] plots;                    // All plot objects on the scene
    public GameObject seedSelectionPanel;       // Shop UI panel
    public GameObject seedButtonPrefab;         // Prefab for seed buttons
    public PlantType currentPlant;
    public Transform seedSelectionGrid;         // Grid for unlocked seeds
    public TextMeshProUGUI panelTitle;          // Title for the panel
    public ShopManager shopManager;

    private PlotSlot selectedSlot;              // track the plot currently being planted
    
    private bool debug = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void OpenSeedSelection(PlotSlot plot)
    {
        // Track the selected Plot Slot
        selectedSlot = plot;

        if (debug)
            Debug.Log($"Selected plot: {selectedSlot.name}");

        // if it's not active, active the panel
        if (!seedSelectionPanel.activeSelf)
            seedSelectionPanel.SetActive(true);

        // hide the tabs
        shopManager.SetTabsInteractable(false);

        // Populate the Seed Selection panel
        PopulateSeedSelection();

    }

    private void PopulateSeedSelection()
    {
        // Clear existing buttons
        foreach (Transform child in seedSelectionGrid)
        {
            Destroy(child.gameObject);
        }

        panelTitle.text = "Available seeds:";

        // Populate with unlocked seeds
        foreach (PlantType unlockedSeed in ResourceManager.instance.unlockedPlantTypes)
        {
            GameObject seedButton = Instantiate(seedButtonPrefab, seedSelectionGrid);
            seedButton.GetComponentInChildren<Image>().sprite = unlockedSeed.itemShopIcon;

            if (debug)
                Debug.Log($"Adding available seed button: {seedButton.name}");

            // Add listener for planting the seed
            Button button = seedButton.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => PlantSeed(unlockedSeed));
        }
    }

    /// <summary>
    /// Plant a seed/plant in the slot
    /// </summary>
    /// <param name="seed"></param>
    private void PlantSeed(PlantType seed)
    {

        if (selectedSlot != null)
        {
            selectedSlot.PlantSeed(seed);
            seedSelectionPanel.SetActive(false);
        }
    }
}
