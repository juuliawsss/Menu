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
        "Valkosipulileipää ja juustolajitelma - Tuoretta rapeaa focaccia leipää ja italialaisia juustoja. (vegaaninen) 5.00€",
        "Antipasti lajitelma - Lajitelma parhaita italialaisia leikkeleitä, Aura-juustoa sekä valkosipuli focaccia-leipää. (Laktoositon) 9.00€"
    };

    private int currentAppetizerIndex = 0;

    // Call this method to order the currently displayed appetizer
    public void OrderCurrentAppetizer()
    {
        OrderAppetizerByIndex(currentAppetizerIndex);
    }

    // Call this from UI to add a specific appetizer by index
    public void OrderAppetizerByIndex(int index)
    {
        if (shoppingcart != null && index >= 0 && index < appetizerList.Length)
        {
            Shoppingcart cartScript = shoppingcart.GetComponent<Shoppingcart>();
            if (cartScript != null)
            {
                string item = appetizerList[index];
                cartScript.AddItem(item);
                Debug.Log($"Added appetizer: {item}");
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
        Debug.Log(appetizerList[0]);
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
