using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Shoppingcart : MonoBehaviour
{
    public static Shoppingcart Instance { get; private set; }
    // List to store items in the cart
    private List<string> cartItems = new List<string>();

    // Add an item to the cart
    public void AddItem(string itemName)
    {
        // If the incoming item already has an amount suffix (e.g., " x2"), keep it as-is
        if (Regex.IsMatch(itemName, @"\sx\d+\s*$"))
        {
            cartItems.Add(itemName);
            Debug.Log($"Added {itemName} to cart (amount preserved).");
            return;
        }

        int amountToAdd = OrderDropdown.Amount;
        // If dessert amount was set separately and is greater, prefer it
        if (amountToAdd == 1 && DessertDropdown.Amount > 1)
        {
            amountToAdd = DessertDropdown.Amount;
        }

        bool isDessert =
            itemName.Contains("Pannacotta") ||
            itemName.Contains("kahvi") ||
            itemName.Contains("juustokakku") ||
            itemName.Contains("Gelato");

        bool isMain =
            itemName.Contains("Pasta Bolognese") ||
            itemName.Contains("Pizza, Quattro Stagione");

        bool isDrink =
            itemName.Contains("Punaviini") ||
            itemName.Contains("Valkoviini") ||
            itemName.Contains("Peroni-olut") ||
            itemName.Contains("Cola");

        if (isDessert || isMain || isDrink)
        {
            string itemWithAmount = $"{itemName} x{amountToAdd}";
            cartItems.Add(itemWithAmount);
            Debug.Log($"Added {itemWithAmount} to cart.");
        }
        else
        {
            // For other categories (e.g., appetizers), keep as-is to preserve previous behavior
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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
