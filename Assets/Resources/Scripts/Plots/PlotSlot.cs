using UnityEngine;
using UnityEngine.UI;

public class PlotSlot : MonoBehaviour
{

    public Button plotSlotButton;
    public PlantType currentPlant;

    private Image plotImage;
    private bool debug = true;

    void Start()
    {
        if (plotImage == null) 
            plotImage = plotSlotButton.GetComponent<Image>();

        plotSlotButton.onClick.AddListener(OpenSeedSelection);
    }

    private void OpenSeedSelection()
    {
        // notify the plotslotmanager to open the seed selection panel
        PlotSlotManager.instance.OpenSeedSelection(this);
    }

    public void PlantSeed(PlantType plant)
    {
        if (debug)
            Debug.Log($"Entered PlantSeed method: {plant.itemName} and current plant: {currentPlant}");

        Transform plotGrid = transform.parent;

        if (plotGrid == null)
        {
            Debug.LogError("Could not find plotGrid parent!");
            return;
        }
        
        foreach (Transform plotPrefab in plotGrid)
        {
            Transform plantPrefab = plotPrefab.Find("PlantPrefab(Clone)");

            if (plantPrefab != null) 
            { 
                Plant plantComponent = plantPrefab.GetComponent<Plant>();

                if (plantComponent != null)
                {
                    plantComponent.SetPlantType(plant);

                    if (debug)
                        Debug.Log($"Updated {plantPrefab.name} with {plant.itemName} from {plotGrid.name}");
                }
                else
                {
                    if (debug)
                        Debug.Log($"No Plant Script found on {plantPrefab.name}");
                }

            } else
            {
                Debug.Log($"No PlantPrefab found under {plotPrefab.name}");
            }
        }

        // update the plot slot's image now
        plotImage.sprite = plant.cropSignSmall;

        if (debug)
            Debug.Log($"Planted {plant.itemName} in this plot!");
    }
}
