using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // added

public class AppetizerDropdown : MonoBehaviour
{
    private int selectedAmount = 1;
    [SerializeField] private TMP_Dropdown amountDropdown; // Assign in Inspector (options 0..6)
    [SerializeField] private string orderSummarySceneName = "OrderSummary"; // scene name

    // Call this from a UI dropdown or button to set the amount
    public void SetAmount(int amount)
    {
        selectedAmount = Mathf.Clamp(amount, 1, 6);
        Debug.Log($"Appetizer amount set to: {selectedAmount}");
    }

    // Convenience overload for TMP dropdown option text
    public void SetAmount(string value)
    {
        if (int.TryParse(value, out var amt))
        {
            SetAmount(amt);
        }
        else
        {
            Debug.LogWarning($"Invalid appetizer amount input: {value}");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (amountDropdown != null)
        {
            amountDropdown.onValueChanged.AddListener(OnAmountDropdownChanged);
        }
    }

    // Called when TMP_Dropdown changes; reads visible option text (e.g., "0".."6")
    private void OnAmountDropdownChanged(int optionIndex)
    {
        if (amountDropdown == null || optionIndex < 0 || optionIndex >= amountDropdown.options.Count)
            return;

        var text = amountDropdown.options[optionIndex].text;
        Debug.Log($"Appetizer amount dropdown changed to: {text}");
        SetAmount(text);
    }

    // Hook your Button OnClick to this and pass "Name - price", e.g., "Bruschetta - 6.00€"
    public void OnAddAppetizerClicked(string itemWithPrice)
    {
        AddAppetizerToCart(itemWithPrice);
    }

    // Ensure a Shoppingcart exists (singleton, find, or create)
    private Shoppingcart EnsureCart()
    {
        var cart = Shoppingcart.Instance;
        if (cart == null)
        {
            // Try to find an existing cart using newer APIs
#if UNITY_2023_1_OR_NEWER
            cart = Object.FindFirstObjectByType<Shoppingcart>();
            if (cart == null)
                cart = Object.FindAnyObjectByType<Shoppingcart>();
#else
            cart = FindObjectOfType<Shoppingcart>();
#endif
            if (cart == null)
            {
                var go = new GameObject("Shoppingcart");
                cart = go.AddComponent<Shoppingcart>();
                DontDestroyOnLoad(go);
                Debug.LogWarning("Shoppingcart was not present in scene. Created one at runtime.");
            }
        }
        return cart;
    }

    // Helper to add an appetizer to the cart
    private void AddAppetizerToCart(string appetizer)
    {
        string item = appetizer?.Trim() ?? string.Empty;
        int xIndex = item.LastIndexOf(" x");
        if (xIndex > 0)
        {
            item = item.Substring(0, xIndex);
        }

        int amount = selectedAmount;
        Debug.Log($"Appetizer amount: {amount}");
        item = $"{item} x{amount}";

        var cart = EnsureCart();     // use the ensured cart
        cart.AddItem(item);
        Debug.Log($"Added appetizer to cart: {item}");
    }

    // Add this to navigate to OrderSummary
    public void GoToOrderSummary()
    {
        Debug.Log("Loading OrderSummary scene...");
        SceneManager.LoadScene(orderSummarySceneName);
    }

    // Optional: add item and then open OrderSummary in one click
    public void OnAddAppetizerAndOpenSummary(string itemWithPrice)
    {
        AddAppetizerToCart(itemWithPrice);
        GoToOrderSummary();
    }
}
