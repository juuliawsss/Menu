using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class OrderDropdown : MonoBehaviour
{
    private int selectedAmount = 1;
    public static int Amount = 1;
    public static string SelectedItem = "";
    public static string CurrentMenuItem = "Pasta Bolognese - Spagettia, bolognesekastiketta ja parmesaanilastuja. (Laktoositon, vegaaninen) 15.00€"; // Default item
    [SerializeField]
    private TMP_Dropdown bologneseDropdown;

    // Call this from a UI input field or dropdown to set the amount
    public void SetAmount(string value)
    {
        if (int.TryParse(value, out var amt))
        {
            SetAmount(amt);
        }
        else
        {
            Debug.LogWarning($"Invalid amount input: {value}");
        }
    }

    // Int overload matching DrinksDropdown style; also keeps static Amount in sync
    public void SetAmount(int amount)
    {
        selectedAmount = Mathf.Clamp(amount, 1, 6);
        Amount = selectedAmount; // keep global in sync for existing cart logic
        Debug.Log($"Amount set to: {selectedAmount}");
    }

    // Call this method to set which menu item will be added when amount is selected
    public static void SetCurrentMenuItem(string menuItem)
    {
        CurrentMenuItem = menuItem;
        Debug.Log($"Current menu item set to: {menuItem}");
    }

    // Call this method to add the currently selected item with the chosen amount to cart
    public void AddItemToCart(string itemName)
    {
        // Fallback to current menu item when no argument is passed from the UI
        if (string.IsNullOrWhiteSpace(itemName))
        {
            itemName = CurrentMenuItem;
        }
        SelectedItem = itemName;
        Debug.Log($"Dropdown AddItemToCart called with: {itemName}, Amount: {Amount}");
        var cartObj = GameObject.FindFirstObjectByType<Shoppingcart>();
        if (cartObj != null)
        {
            Debug.Log("Found Shoppingcart object, calling AddItem");
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

    void Awake()
    {
        // Try to auto-bind from same GO; if not, look in children
        if (bologneseDropdown == null)
        {
            bologneseDropdown = GetComponent<TMP_Dropdown>();
            if (bologneseDropdown == null)
            {
                bologneseDropdown = GetComponentInChildren<TMP_Dropdown>(true);
            }
        }
    }

    void OnEnable()
    {
        // Reset amount on scene/enable to avoid stale static values bleeding between scenes
        SetAmount(1);
    }

    void Start()
    {
        if (bologneseDropdown != null)
        {
            bologneseDropdown.onValueChanged.AddListener(OnBologneseDropdownChanged);
            // Initialize once so Amount matches current selection
            OnBologneseDropdownChanged(bologneseDropdown.value);
        }
        else
        {
            Debug.LogWarning("OrderDropdown: bologneseDropdown not found. Assign it in the Inspector if you need dropdown-driven amount changes.");
            // Keep script enabled; other methods (SetAmount, AddItemToCart) still work
            // Ensure a sane default even without a dropdown
            Amount = 1;
        }
    }

    private void OnBologneseDropdownChanged(int _)
    {
        var optText = bologneseDropdown.options[bologneseDropdown.value].text;
        SetAmount(optText);
    }

    void Update()
    {
        // ...existing code...
    }
}
