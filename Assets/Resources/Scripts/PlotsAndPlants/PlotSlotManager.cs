using UnityEngine;
using UnityEngine.UI;

public class PlotSlotManager : MonoBehaviour
{

    public static PlotSlotManager instance;

    public PlotSlot[] plots; // All plot objects on the scene
    public GameObject seedSelectionPanel; // seed selection panel
    public Transform seedSelectionGrid; // Grid for unlocked seeds
    public GameObject seedButtonPrefab; // Prefab for seed buttons

    private PlotSlot selectedPlot; // track the plot currently being planted

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

        // Populate the Seed Selection panel
        PopulateSeedSelection();

        // Show the panel
        seedSelectionPanel.SetActive(true);
    }

    private void PopulateSeedSelection()
    {
        // Clear existing buttons
        foreach (Transform child in seedSelectionGrid)
        {
            Destroy(child.gameObject);
        }

        // Populate with unlocked seeds
        foreach (PlantType unlockedSeed in ResourceManager.instance.unlockedPlants)
        {
            GameObject seedButton = Instantiate(seedButtonPrefab, seedSelectionGrid);
            seedButton.GetComponentInChildren<Image>().sprite = unlockedSeed.shopIcon;

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
