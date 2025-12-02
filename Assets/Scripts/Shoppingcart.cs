using UnityEngine;
using System.Collections.Generic;

public class Shoppingcart : MonoBehaviour
{
    // List to store items in the cart
    private List<string> cartItems = new List<string>();

    // Add an item to the cart
    public void AddItem(string itemName)
    {
        cartItems.Add(itemName);
        Debug.Log($"Added {itemName} to cart.");
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

    // Call this from a UI button to place the order
    public void OnShoppingcartButtonPressed()
    {
        PlaceOrder();
    }

    public static List<string> OrderedItems = new List<string>();
}
