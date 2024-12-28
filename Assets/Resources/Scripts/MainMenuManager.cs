using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject graphicsPanel;
    public GameObject soundPanel;
    public GameObject languagePanel;

    private bool debug = true; 

    /// <summary>
    /// Start a new game
    /// </summary>
    public void StartNewGame()
    {
        Addressables.LoadSceneAsync("FarmOverview").Completed += OnSceneLoaded;
    }

    /// <summary>
    /// Load an existing game
    /// </summary>
    public void LoadGame()
    {
        GameStateManager.instance.LoadGameState();
        Addressables.LoadSceneAsync("FarmOverview").Completed += OnSceneLoaded;
    }

    /// <summary>
    /// Open the options menu to configure the various game options
    /// </summary>
    public void Options()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
        }
    }

    /// <summary>
    /// Open the graphics menu and the related tab containing all settings
    /// </summary>
    public void Graphics()
    {
        if (graphicsPanel != null)
        {
            graphicsPanel.SetActive(!graphicsPanel.activeSelf);
        }
    }

    /// <summary>
    /// Open the sound menu and the related tab containing all settings
    /// </summary>
    public void Sound()
    {
        if (soundPanel != null) 
        { 
            soundPanel.SetActive(!soundPanel.activeSelf);
        }
    }

    public void Language()
    {
        if (languagePanel != null)
        {
            languagePanel.SetActive(!languagePanel.activeSelf);
        }
    }

    /// <summary>
    /// Quit the application
    /// </summary>
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

    private void OnSceneLoaded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("FarmOverview loaded succesfully");
        }
        else
        {
            Debug.LogError("Failed to load FarmOverview");
        }
    }
}
