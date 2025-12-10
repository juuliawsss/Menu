using UnityEngine;
using System.Collections.Generic;

public class Shoppingcart : MonoBehaviour
{
    // List to store items in the cart
    private List<string> cartItems = new List<string>();

    // Add an item to the cart
    public void AddItem(string itemName)
    {
        int amountToAdd = OrderDropdown.Amount;
        // Use DessertDropdown.Amount if OrderDropdown.Amount is 1 and DessertDropdown.Amount > 1
        if (amountToAdd == 1 && DessertDropdown.Amount > 1)
        {
            amountToAdd = DessertDropdown.Amount;
        }
        // For main dishes that support amount selection, add the amount
        if (itemName.Contains("Pasta Bolognese") || itemName.Contains("Pizza, Quattro Stagione"))
        {
            string itemWithAmount = $"{itemName} x{amountToAdd}";
            cartItems.Add(itemWithAmount);
            Debug.Log($"Added {itemWithAmount} to cart.");
        }
        // For desserts, always add amount
        else if (
            itemName.Contains("Pannacotta") ||
            itemName.Contains("kahvi") ||
            itemName.Contains("juustokakku") ||
            itemName.Contains("Gelato")
        )
        {
            string itemWithAmount = $"{itemName} x{amountToAdd}";
            cartItems.Add(itemWithAmount);
            Debug.Log($"Added {itemWithAmount} to cart.");
        }
        else
        {
            cartItems.Add(itemName);
            Debug.Log($"Added {itemName} to cart.");
        }
    // Do not reset amount here; user should control amount per item
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
        cartItems.Clear(); // Only clear cart on actual order
        // Switch to OrderSummary scene after placing order
        UnityEngine.SceneManagement.SceneManager.LoadScene("OrderSummary");
    }

    // Call this from a UI button to load OrderSummary scene
    public void OnShoppingcartButtonPressed()
    {
        Debug.Log($"OnShoppingcartButtonPressed: Cart has {cartItems.Count} items");
        foreach (var item in cartItems)
        {
            Debug.Log($"Cart item: {item}");
        }
        OrderedItems = new List<string>(cartItems);
        Debug.Log($"OrderedItems now has {OrderedItems.Count} items");
        UnityEngine.SceneManagement.SceneManager.LoadScene("OrderSummary");
    }

    public static List<string> OrderedItems = new List<string>();
    
    void Awake()
    {
        // Ensure OrderedItems is initialized
        if (OrderedItems == null)
        {
            OrderedItems = new List<string>();
            Debug.Log("OrderedItems initialized in Awake");
        }
    }
}
