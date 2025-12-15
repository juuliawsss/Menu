using UnityEngine;
using UnityEngine.SceneManagement;

public class DrinksMenu : MonoBehaviour
{
    private string punaviini = "Punaviini - 10.00€";
    private string valkoviini = "Valkoviini - 10.00€";
    private string peroni = "Peroni-olut - 8.00€";
    private string cola = "Cola - 3.00€";

    // === JUOMIEN KLIKKAUKSET (SAMA KUIN MAINS) ===

    public void OnPunaviiniClicked()
    {
        Debug.Log(punaviini);
        OrderDropdown.SetCurrentMenuItem(punaviini);
    }

    public void OnValkoviiniClicked()
    {
        Debug.Log(valkoviini);
        OrderDropdown.SetCurrentMenuItem(valkoviini);
    }

    public void OnPeroniClicked()
    {
        Debug.Log(peroni);
        OrderDropdown.SetCurrentMenuItem(peroni);
    }

    public void OnColaClicked()
    {
        Debug.Log(cola);
        OrderDropdown.SetCurrentMenuItem(cola);
    }

    // === SCENE NAVIGOINTI ===

    public void LoadDrinksScene()
    {
        SceneManager.LoadScene("Drinks");
    }

    public void GoToOrderSummary()
    {
        var dropdownObj = GameObject.FindFirstObjectByType<OrderDropdown>();
        if (dropdownObj != null)
        {
            dropdownObj.GoToOrderSummary();
        }
        else
        {
            var cartObj = GameObject.FindFirstObjectByType<Shoppingcart>();
            if (cartObj != null)
            {
                cartObj.OnShoppingcartButtonPressed();
            }
        }
    }
}
