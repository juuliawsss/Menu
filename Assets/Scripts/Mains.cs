using UnityEngine;
using UnityEngine.SceneManagement;

public class Mains : MonoBehaviour
{
    private string pastaBolognese = "Pasta Bolognese - Spagettia, bolognesekastiketta ja parmesaanilastuja. (Laktoositon, vegaaninen) 15.00€";
    private string pizzaQuattroStagione = "Pizza, Quattro Stagione - Kinkkua, katkarapuja, tonnikalaa ja tuoreita herkkusieniä. (Laktoositon) 15.00€";
    // Call this from the Pizza Quattro Stagione text OnClick or EventTrigger
    public void OnPizzaQuattroStagioneClicked()
    {
        Debug.Log(pizzaQuattroStagione);
        // Only set the current menu item for the dropdown
        OrderDropdown.SetCurrentMenuItem(pizzaQuattroStagione);
    }

    void Start()
    {
        // ...existing code...
    }

    void Update()
    {
        // ...existing code...
    }

    // Call this from a UI button to load the Mains scene
    public void LoadMainsScene()
    {
        SceneManager.LoadScene("Mains");
    }

    // Call this from the Pasta Bolognese text OnClick or EventTrigger
    public void OnPastaBologneseClicked()
    {
        Debug.Log(pastaBolognese);
        // Only set the current menu item for the dropdown
        OrderDropdown.SetCurrentMenuItem(pastaBolognese);
    }

    // Call this method to navigate to the order summary
    public void GoToOrderSummary()
    {
        var dropdownObj = GameObject.FindFirstObjectByType<OrderDropdown>();
        if (dropdownObj != null)
        {
            dropdownObj.GoToOrderSummary();
        }
        else
        {
            // Fallback: go directly
            var cartObj = GameObject.FindFirstObjectByType<Shoppingcart>();
            if (cartObj != null)
            {
                cartObj.OnShoppingcartButtonPressed();
            }
        }
    }
}
