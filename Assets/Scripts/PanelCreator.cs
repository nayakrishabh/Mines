using UnityEngine;
using UnityEngine.UI;

public class SquarePanelCreator : MonoBehaviour
{
    public RectTransform canvasTransform; // Assign your Canvas' RectTransform in the Inspector
    public float size = 200f; // Desired size of the square panel

    void Start()
    {
        // Create a new GameObject with a RectTransform and Image component
        GameObject panelObject = new GameObject("SquarePanel", typeof(RectTransform), typeof(Image));
        
        // Set the panel's parent to be the Canvas
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.SetParent(canvasTransform, false);

        // Set the size of the panel
        panelRect.sizeDelta = new Vector2(size, size);

        // Center the panel and make it maintain a square aspect ratio
        panelRect.anchorMin = new Vector2(0.5f, 0.5f);
        panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = Vector2.zero;

        // Optional: Add color to the panel
        Image panelImage = panelObject.GetComponent<Image>();
        
        panelImage.color = new Color(255f,255f,255f,100f); // Change to any color you like
        
    }
}
