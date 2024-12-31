using UnityEngine;

public class UIManager : MonoBehaviour
{

    public ShopManager shopManager;
    public PlotSlotManager slotManager;
    public DebugMenu debugMenu;
    public GameObject optionsPanel;

    private bool debug = false;

    public void OpenShopMenu()
    {
        if (debug)
            Debug.Log("UIManager - OpenShopMenu triggered");

        shopManager.ToggleShop();
    }

    public void OpenDebugMenu()
    {
        if (debug)
            Debug.Log("UIManager - OpenDebugMenu triggered");

        debugMenu.ToggleDebugMenu();
    }

    public void OpenOptionsMenu()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
        }

        Debug.Log("UIManager - OpenOptionsMenu - Not implemented yet");
    }

    public void OpenOptionsSubMenu()
    {
        Debug.Log("UIManager - OpenOptionsSubMenu - Not implemented yet");
    }

    public void OpenSaveMenu()
    {
        Debug.Log("UIManager - OpenSaveMenu - Not implemented yet");
    }

    public void OpenLoadMenu()
    {
        Debug.Log("UIManager - OpenLoadMenu - Not implemented yet");
    }

    public void QuitGame()
    {
        if (debug)
            Debug.Log("Exiting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif    

    }
}
