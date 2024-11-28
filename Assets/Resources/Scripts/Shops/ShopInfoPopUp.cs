using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopInfoPopUp : MonoBehaviour
{
    public static ShopInfoPopUp Instance {  get; private set; }

    [Header("UI Elements")]
    public GameObject popUpBackgroundPanel;
    public TextMeshProUGUI popUpNameText;
    public TextMeshProUGUI popUpDescriptionText;
    public TextMeshProUGUI popUpHarvestDetails;
    public TextMeshProUGUI popUpPriceText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate ShopInfoPopUp found. Destroying the duplicate.");
            Destroy(gameObject);
            return;
        }

        CanvasGroup canvasGroup = popUpBackgroundPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = popUpBackgroundPanel.AddComponent<CanvasGroup>();
        }

        canvasGroup.blocksRaycasts = false;

        Instance = this;
    }

    // show the popup and it's contents
    public void Show(string title, string description, string harvest, string price, Vector3 position)
    {
        popUpBackgroundPanel.SetActive(true);

        if (popUpNameText != null)
            popUpNameText.text = title;

        if (popUpDescriptionText != null)
            popUpDescriptionText.text = description;

        if (popUpHarvestDetails != null)
            popUpHarvestDetails.text = harvest;

        if (popUpPriceText != null)
            popUpPriceText.text = price;

        SetPopUpPosition(position);
       
    }

    public void Hide()
    {
        popUpBackgroundPanel.SetActive(false);
    }

    private void SetPopUpPosition(Vector3 position)
    {

        // offset slightly below and to the left of the cursor
        Vector3 offset = new(-150f, -150f, 0f); 
        Vector3 adjustedPosition = position + offset;

        Debug.Log($"Offset: {offset} and position: {adjustedPosition}");

        // Clamp position to screen bounds
        RectTransform canvasRect = popUpBackgroundPanel.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        RectTransform rectTransform = popUpBackgroundPanel.GetComponent<RectTransform>();

        Vector2 pivot = rectTransform.pivot;
        Vector2 minPosition = new(
            rectTransform.sizeDelta.x * pivot.x,
            rectTransform.sizeDelta.y * pivot.y
        );

        Vector2 maxPosition = new(
            canvasRect.rect.width - rectTransform.sizeDelta.x * (1 - pivot.x),
            canvasRect.rect.height - rectTransform.sizeDelta.y * (1 - pivot.y)
        );

        // Ensure position stays within bounds
        adjustedPosition.x = Mathf.Clamp(adjustedPosition.x, minPosition.x, maxPosition.x);
        adjustedPosition.y = Mathf.Clamp(adjustedPosition.y, minPosition.y, maxPosition.y);

        rectTransform.position = adjustedPosition;

    }
}
