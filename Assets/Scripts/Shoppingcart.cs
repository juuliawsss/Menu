using UnityEngine;
using System.Collections.Generic;

public class Shoppingcart : MonoBehaviour
{
    // List to store items in the cart
    private List<string> cartItems = new List<string>();

    // Add an item to the cart
    public void AddItem(string itemName)
    {
        // For main dishes that support amount selection, add the amount
        if (itemName.Contains("Pasta Bolognese") || itemName.Contains("Pizza, Quattro Stagione"))
        {
            string itemWithAmount = $"{itemName} x{Dropdown.Amount}";
            cartItems.Add(itemWithAmount);
            Debug.Log($"Added {itemWithAmount} to cart.");
        }
        else
        {
            // For other items, check if we need to add amount from dropdown
            if (Dropdown.Amount > 1)
            {
                string itemWithAmount = $"{itemName} x{Dropdown.Amount}";
                cartItems.Add(itemWithAmount);
                Debug.Log($"Added {itemWithAmount} to cart.");
            }
            else
            {
                cartItems.Add(itemName);
                Debug.Log($"Added {itemName} to cart.");
            }
        }
    }

    // Place the order
    public void PlaceOrder()
    {
        if (cartItems.Count == 0)
        {
            Debug.Log("Cart is empty. Cannot place order.");
            return;
        }
        Debug.Log("Order placed! Items:");
        foreach (var item in cartItems)
        {
            Debug.Log(item);
        }
        OrderedItems = new List<string>(cartItems);
        cartItems.Clear();
        // Switch to OrderSummary scene after placing order
        UnityEngine.SceneManagement.SceneManager.LoadScene("OrderSummary");
    }

    // Call this from a UI button to load OrderSummary scene
    public void OnShoppingcartButtonPressed()
    {
        OrderedItems = new List<string>(cartItems);
        UnityEngine.SceneManagement.SceneManager.LoadScene("OrderSummary");
    }

    public static List<string> OrderedItems = new List<string>();
}
