using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainMenuManager : MonoBehaviour
{
    public enum MenuCategory { None, Graphics, Sound, Language };
    public MenuCategory currentCategory = MenuCategory.None;

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
            if (currentCategory != MenuCategory.Graphics)
            {
                SwitchMenuPanel(currentCategory);
                graphicsPanel.SetActive(true);
                currentCategory = MenuCategory.Graphics;
            }
        } 
        else
        {
            Debug.LogError("MainMenuManager.Graphics() - No Graphics Panel component configured or found");
        }
    }

    /// <summary>
    /// Open the sound menu and the related tab containing all settings
    /// </summary>
    public void Sound()
    {
        if (soundPanel != null) 
        {
            if (currentCategory != MenuCategory.Sound) 
            {
                SwitchMenuPanel(currentCategory);
                soundPanel.SetActive(true);
                currentCategory = MenuCategory.Sound;
            }
            
        }
    }

    /// <summary>
    /// Open the language menu and the related tab containing all settings
    /// </summary>
    public void Language()
    {
        if (languagePanel != null)
        {
            SwitchMenuPanel(currentCategory);
            languagePanel.SetActive(true);
            currentCategory = MenuCategory.Language;
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

    private void SwitchMenuPanel(MenuCategory category)
    {
        switch (category)
        {
            case MenuCategory.Graphics:
                graphicsPanel.SetActive(false);
                break;
            case MenuCategory.Sound:
                soundPanel.SetActive(false);
                break;
            case MenuCategory.Language:
                languagePanel.SetActive(false); 
                break;
            default:
                break;
        }
            
    }
}
