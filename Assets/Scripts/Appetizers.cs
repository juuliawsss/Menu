using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Appetizers : MonoBehaviour
{
    [SerializeField] private Button appetizerButton;
    [SerializeField] private TextMeshProUGUI appetizerDisplayText;
    [SerializeField] private GameObject appetizerMenuPanel;
    
    private string[] appetizerList = {
        "� Garlic bread and cheese assortment - Fresh crispy focaccia bread and Italian cheeses. (vegan) 5.00€",
        "� Antipasti assortment - Assortment of the best Italian cold cuts, Aura cheese and garlic focaccia bread. (Lactose-free) 9.00€"
    };
    
    private int currentAppetizerIndex = 0;
    private bool menuVisible = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If button is assigned, add a listener for when it's clicked
        if (appetizerButton != null)
        {
            appetizerButton.onClick.AddListener(OnAppetizerButtonPressed);
        }
        
        // Hide the menu panel initially
        if (appetizerMenuPanel != null)
        {
            appetizerMenuPanel.SetActive(false);
        }
    }

    // This method is called when the button is pressed
    public void OnAppetizerButtonPressed()
    {
        if (!menuVisible)
        {
            ShowAppetizers();
        }
        else
        {
            ShowNextAppetizer();
        }
    }
    
    private void ShowAppetizers()
    {
        menuVisible = true;
        
        // Show the appetizer menu panel
        if (appetizerMenuPanel != null)
        {
            appetizerMenuPanel.SetActive(true);
        }
        
        // Display the first appetizer
        DisplayCurrentAppetizer();
        
        Debug.Log("Appetizer menu opened!");
    }
    
    private void ShowNextAppetizer()
    {
        // Move to next appetizer (loop back to start if at end)
        currentAppetizerIndex = (currentAppetizerIndex + 1) % appetizerList.Length;
        DisplayCurrentAppetizer();
    }
    
    private void DisplayCurrentAppetizer()
    {
        if (appetizerDisplayText != null)
        {
            appetizerDisplayText.text = appetizerList[currentAppetizerIndex];
        }
        
        Debug.Log($"Now showing: {appetizerList[currentAppetizerIndex]}");
    }
    
    // Method to close the appetizer menu
    public void CloseAppetizerMenu()
    {
        menuVisible = false;
        currentAppetizerIndex = 0;
        
        if (appetizerMenuPanel != null)
        {
            appetizerMenuPanel.SetActive(false);
        }
        
        Debug.Log("Appetizer menu closed!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
