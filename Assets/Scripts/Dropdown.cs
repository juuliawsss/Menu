using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public static int Amount = 1;
    public static string SelectedItem = "";

    // Call this from a UI input field or dropdown to set the amount
    public void SetAmount(string value)
    {
        if (int.TryParse(value, out int result) && result > 0)
        {
            Amount = result;
            Debug.Log($"Amount set to: {Amount}");
        }
        else
        {
            Debug.LogWarning("Invalid amount entered.");
        }
    }

    // Call this method to add the currently selected item with the chosen amount to cart
    public void AddItemToCart(string itemName)
    {
        SelectedItem = itemName;
        var cartObj = GameObject.FindFirstObjectByType<Shoppingcart>();
        if (cartObj != null)
        {
            cartObj.AddItem(itemName);
            Debug.Log($"Added {itemName} x{Amount} to cart");
        }
        else
        {
            Debug.LogWarning("Shoppingcart object not found.");
        }
    }

    // Call this method to go to order summary after adding items
    public void GoToOrderSummary()
    {
        var cartObj = GameObject.FindFirstObjectByType<Shoppingcart>();
        if (cartObj != null)
        {
            cartObj.OnShoppingcartButtonPressed();
        }
        else
        {
            Debug.LogWarning("Shoppingcart object not found.");
        }
    }

    // Optionally, get the current amount
    public int GetAmount()
    {
        return Amount;
    }

    void Start()
    {
        // ...existing code...
    }

    void Update()
    {
        // ...existing code...
    }
}
