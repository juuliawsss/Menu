using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Ensure a TMP_Dropdown exists on the same GameObject when intended
[RequireComponent(typeof(TMP_Dropdown))]
public class OrderDropdown : MonoBehaviour
{
    public static int Amount = 1;
    public static string SelectedItem = "";
    public static string CurrentMenuItem = "Pasta Bolognese - Spagettia, bolognesekastiketta ja parmesaanilastuja. (Laktoositon, vegaaninen) 15.00€"; // Default item
    [SerializeField]
    private TMP_Dropdown bologneseDropdown;

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

    // Call this method to set which menu item will be added when amount is selected
    public static void SetCurrentMenuItem(string menuItem)
    {
        CurrentMenuItem = menuItem;
        Debug.Log($"Current menu item set to: {menuItem}");
    }

    // Call this method to add the currently selected item with the chosen amount to cart
    public void AddItemToCart(string itemName)
    {
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
        // Bind to the TMP_Dropdown on the same GameObject by default
        if (bologneseDropdown == null)
        {
            bologneseDropdown = GetComponent<TMP_Dropdown>();
        }
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
            Debug.LogError("OrderDropdown: bologneseDropdown is not assigned. Assign it in the Inspector or keep this script on the same GameObject as the TMP_Dropdown.");
            enabled = false; // Prevent further null-reference use
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
