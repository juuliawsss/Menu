using UnityEngine;

public class DrinksDropdown : MonoBehaviour
{
    // Public methods for each drink to be called from UI (TextMeshPro or Button)
    public void OnPunaviiniClicked()
    {
        AddDrinkToCart("Punaviini - 10.00€");
    }

    public void OnValkoviiniClicked()
    {
        AddDrinkToCart("Valkoviini - 10.00€");
    }

    public void OnPeroniClicked()
    {
        AddDrinkToCart("Peroni-olut - 8.00€");
    }

    public void OnColaClicked()
    {
        AddDrinkToCart("Cola - 3.00€");
    }

    // Helper to add a drink to the cart (one at a time)
    private void AddDrinkToCart(string drink)
    {
        int selectedAmount = 1; // Default to 1, or get from your UI if needed
        string item = $"{drink} x{selectedAmount}";
        Shoppingcart.Instance.AddItem(item);
        Debug.Log($"Added drink to cart: {item}");
    }
}


