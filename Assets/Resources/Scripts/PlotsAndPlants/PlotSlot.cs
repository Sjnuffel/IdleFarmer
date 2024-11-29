using UnityEngine;
using UnityEngine.UI;

public class PlotSlot : MonoBehaviour
{

    public Button plotSlotButton;
    public Image plotImage;
    public PlantType currentPlant;

    void Start()
    {
        plotSlotButton.onClick.AddListener(OpenSeedSelection);
    }

    private void OpenSeedSelection()
    {
        // notify the plotslotmanager to open the seed selection panel
        PlotSlotManager.instance.OpenSeedSelection(this);
    }

    public void PlantSeed(PlantType seed)
    {
        // plant the seed
        currentPlant = seed;
        plotImage.sprite = seed.shopIcon; // update the visual

        Debug.Log($"Planted {seed.plantName} in this plot!");
    }
}
