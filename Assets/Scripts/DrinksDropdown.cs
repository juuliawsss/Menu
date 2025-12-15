using UnityEngine;



using TMPro;

public class DrinksDropdown : MonoBehaviour
{
    private int selectedAmount = 1;

    // Call this from a UI button if you want to set the amount
    public void SetAmount(int amount)
    {
        selectedAmount = Mathf.Clamp(amount, 1, 6);
        Debug.Log($"Drink amount set to: {selectedAmount}");
    }

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
        // Ensure format: DrinkName - 8.00€ xN
        string item = drink.Trim();
        int xIndex = item.LastIndexOf(" x");
        if (xIndex > 0)
        {
            item = item.Substring(0, xIndex);
        }
        item = $"{item} x{selectedAmount}";
        Shoppingcart.Instance.AddItem(item);
        Debug.Log($"Added drink to cart: {item}");
    }
}


