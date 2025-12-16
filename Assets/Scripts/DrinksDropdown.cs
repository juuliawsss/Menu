using UnityEngine;



using TMPro;

public class DrinksDropdown : MonoBehaviour
{
    private int selectedAmount = 1;
    [SerializeField] private TMP_Dropdown amountDropdown; // Assign in Inspector (options 0..6)

    // Call this from a UI button if you want to set the amount
    public void SetAmount(int amount)
    {
        selectedAmount = Mathf.Clamp(amount, 1, 6);
        // Keep OrderDropdown in sync so globals reflect the latest amount
        //OrderDropdown.Amount = selectedAmount;
        Debug.Log($"Drink amount set to: {selectedAmount}");
    }

    // Convenience overload for TMP_InputField or dropdown string values
    public void SetAmount(string value)
    {
        if (int.TryParse(value, out var amt))
        {
            SetAmount(amt);
        }
        else
        {
            Debug.LogWarning($"Invalid drink amount input: {value}");
        }
    }

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
        Debug.Log($"Drink amount dropdown changed to: {text}");
        SetAmount(text);
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
        // Prefer explicitly set local amount, otherwise fall back to global dropdown amount if larger
        int amount = selectedAmount;
        Debug.Log($"Local drink amount: {amount}, Global order amount: {OrderDropdown.Amount}");
        /* if (amount == 1 && OrderDropdown.Amount > 1)
        {
            amount = OrderDropdown.Amount;
        }*/
        item = $"{item} x{amount}";
        Shoppingcart.Instance.AddItem(item);
        Debug.Log($"Added drink to cart: {item}");
    }
}


