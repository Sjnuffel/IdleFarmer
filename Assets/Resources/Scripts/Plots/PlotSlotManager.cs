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

    private PlotSlot selectedPlot;              // track the plot currently being planted
    
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
        selectedPlot = plot;

        if (debug)
            Debug.Log($"Selected plot: {selectedPlot.name}");

        // Populate the Seed Selection panel
        PopulateSeedSelection();

        // Show the panel
        seedSelectionPanel.SetActive(true);
        shopManager.SetTabsInteractable(false);
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

    private void PlantSeed(PlantType seed)
    {
        if (selectedPlot != null)
        {
            // plant the seed in selected plot
            selectedPlot.PlantSeed(seed);
        }

        // deactive the seed selection panell
        seedSelectionPanel.SetActive(false);
    }
}
