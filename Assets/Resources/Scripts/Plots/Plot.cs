using UnityEngine;

public class Plot : MonoBehaviour
{
    public GameObject plantPrefab; // assign the plantPrefab to show when we plant our seed

    public bool isOccupied = false;
    public int plotID;

    private bool debug = false;

    public void PlantSeed()
    {

        // plant a seed when the plot is free
        if (!isOccupied)
        {
            isOccupied = true;

            // assign the plant to the plot's position
            GameObject newPlant = Instantiate(plantPrefab, transform.position, Quaternion.identity);
            newPlant.transform.SetParent(transform);

            // Register the plant in the GameManager
            if (newPlant.TryGetComponent<Plant>(out var plantScript))
            {
                if (debug)
                    Debug.Log($"Plant script: {plantScript}");
                
                GameManager.instance.RegisterPlant(plantScript);
            }

            // Generate an ID for the plot
            GameManager.instance.AssignPlantToPlot(plotID, newPlant);
            
            if (debug)
                Debug.Log($"Seed planted in plot {plotID}!");
        }
        else
        {
            // get the plant by plot id and access it's harvest method
            GameObject existingPlant = GameManager.instance.GetPlantInPlot(plotID);

            if (existingPlant != null && existingPlant.TryGetComponent<Plant>(out var plantScript))
            {
                plantScript.HarvestPlant();
                if (debug)
                    Debug.Log($"Harvested plant in plot {plotID}");
            }
            else
            {
                Debug.LogError($"No valid plant found in plot {plotID}");
            }

            Debug.Log("This pot is already occupied");
        }
    }
}
