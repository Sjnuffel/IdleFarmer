using UnityEngine;

public class PlotManager : MonoBehaviour
{

    public GameObject plotPrefab; // assign the plot prefab in the inspector

    public int rows = 3;
    public int columns = 15;
    public float xOffSet = 64f;
    public float yOffSet = 32f;

    // Start is called before the first frame update
    void Start()
    {
        PopulateGrid();
    }

    void PopulateGrid()
    {
        int plotIDCounter = 0;

        // use the Transform center position as the grid starting point
        RectTransform gridTransform = GetComponent<RectTransform>();
        Vector3 gridBottomLeft = gridTransform.position;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPos = gridBottomLeft.x + (col - row) * xOffSet; // Diamond shape
                float yPos = gridBottomLeft.y + (col + row) * yOffSet; // Stacked appearence

                Vector3 position = new(xPos, yPos, 0);

                // instantiate the plot
                GameObject plot = Instantiate(plotPrefab, position, Quaternion.identity, transform);

                // assign a unique ID to the plot
                Plot plotScript = plot.GetComponent<Plot>();
                if (plot != null)
                    plotScript.plotID = plotIDCounter;

                // Debug.Log($"Plot {plotIDCounter} at Position: {position}");

                plotIDCounter++;
            }
        }        
    }
}