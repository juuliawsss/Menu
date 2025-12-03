using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Appetizers : MonoBehaviour
{
    [SerializeField] private Button appetizerButton;
    [SerializeField] private TextMeshProUGUI appetizerText; // Assign in Inspector
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
            }
        }
    }

    void Start()
    {
        // If button is assigned, add a listener for when it's clicked
        if (appetizerButton != null)
        {
            appetizerButton.onClick.AddListener(OnAppetizerButtonPressed);
        }
    }

    // Call this from Unity EventTrigger or OnPointerClick on the text object
    public void OnAppetizerTextClicked()
    {
        OrderCurrentAppetizer();
    }

    // This method is called when the button is pressed
    public void OnAppetizerButtonPressed()
    {
        SceneManager.LoadScene("appetizermenu");
    }

    // ...existing code...

    public void OnShoppingcartButtonPressed()
    {
        SceneManager.LoadScene("OrderSummary");
    }

    void Update()
    {
        // ...existing code...
    }
}
