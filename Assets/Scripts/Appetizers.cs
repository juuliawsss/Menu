using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Appetizers : MonoBehaviour
{
    [SerializeField] private Button appetizerButton;
    public GameObject shoppingcart; // Assign the Shoppingcart prefab or object in the Inspector

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
            Shoppingcart cartScript = shoppingcart.GetComponent<Shoppingcart>();
            if (cartScript != null)
            {
                string item = appetizerList[currentAppetizerIndex];
                cartScript.AddItem(item);
                Debug.Log($"Ordered: {item}");
            }
            else
            {
                Debug.LogWarning("No Shoppingcart script found on assigned GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Shoppingcart GameObject not set in Appetizers script.");
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
        // Switch to the Appetizer scene, then order the appetizer
    SceneManager.LoadScene("appetizermenu");
        // Optionally, you can delay ordering until after the scene loads, but for now, order immediately:
        OrderCurrentAppetizer();
    }
    
    // ...existing code...

    // Update is called once per frame
    void Update()
    {
        
    }
}
