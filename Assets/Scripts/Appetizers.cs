using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Appetizers : MonoBehaviour
{
    [SerializeField] private Button appetizerButton;
    [SerializeField] private Shoppingcart shoppingcart; // Reference to Shoppingcart

    private string[] appetizerList = {
        "� Garlic bread and cheese assortment - Fresh crispy focaccia bread and Italian cheeses. (vegan) 5.00€",
        "� Antipasti assortment - Assortment of the best Italian cold cuts, Aura cheese and garlic focaccia bread. (Lactose-free) 9.00€"
    };

    private int currentAppetizerIndex = 0;
    // Call this method to order the currently displayed appetizer
    public void OrderCurrentAppetizer()
    {
        if (shoppingcart != null)
        {
            string item = appetizerList[currentAppetizerIndex];
            shoppingcart.AddItem(item);
            Debug.Log($"Ordered: {item}");
        }
        else
        {
            Debug.LogWarning("Shoppingcart reference not set in Appetizers script.");
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If button is assigned, add a listener for when it's clicked
        if (appetizerButton != null)
        {
            appetizerButton.onClick.AddListener(OnAppetizerButtonPressed);
        }
    }

    // This method is called when the button is pressed
    public void OnAppetizerButtonPressed()
    {
        // When the button is pressed, order the current appetizer
        OrderCurrentAppetizer();
    }
    
    // ...existing code...

    // Update is called once per frame
    void Update()
    {
        
    }
}
