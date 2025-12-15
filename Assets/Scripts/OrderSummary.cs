using System.Globalization;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OrderSummary : MonoBehaviour
{
    public TextMeshProUGUI orderText; // Assign in Inspector

    void Start()
    {
        UpdateOrderSummary();
    }

    // Call this to update the order summary text at any time
    public void UpdateOrderSummary()
    {
        // Get the ordered items from Shoppingcart (using a static/shared list for simplicity)
        List<string> items = Shoppingcart.OrderedItems;
        Debug.Log($"OrderSummary Update: Found {items?.Count ?? 0} items in OrderedItems");
        
        if (items != null && items.Count > 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Your Order:");
            sb.AppendLine(""); // Empty line for better formatting

            float total = 0f;
            foreach (var item in items)
            {
                Debug.Log($"Processing item: {item}");
                // Remove any unsupported or control characters
                string cleanItem = System.Text.RegularExpressions.Regex.Replace(item, @"[^\p{L}\p{N}\p{P}\p{Z}€]", "");
                sb.AppendLine(cleanItem.Trim());
                sb.AppendLine(""); // Empty line between items

                // Try to extract price and amount
                float price = 0f;
                int amount = 1;
                // Find price (e.g., 15.00€)
                var priceMatch = System.Text.RegularExpressions.Regex.Match(cleanItem, @"(\d+[\.,]\d{2})€");
                if (priceMatch.Success)
                {
                    Debug.Log($"Price match: {priceMatch.Groups[1].Value}");
                    float.TryParse(priceMatch.Groups[1].Value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out price);
                }
                else
                {
                    Debug.LogWarning($"No price found in: {cleanItem}");
                }
                // Find amount (e.g., x2)
                var amountMatch = System.Text.RegularExpressions.Regex.Match(cleanItem, @"x(\d+)");
                if (amountMatch.Success)
                {
                    Debug.Log($"Amount match: {amountMatch.Groups[1].Value}");
                    int.TryParse(amountMatch.Groups[1].Value, out amount);
                }
                else
                {
                    Debug.Log($"No amount found in: {cleanItem}, defaulting to 1");
                }
                Debug.Log($"Adding to total: {price} * {amount} = {price * amount}");
                total += price * amount;
            }
            sb.AppendLine($"<size=90%><b>Total: {total:0.00}€</b></size>");
            orderText.text = sb.ToString();
        }
        else
        {
            Debug.Log("OrderSummary: Cart is empty or null");
            orderText.text = "Your cart is empty.";
        }
    }
    // Call this method to switch back to the Appetizermenu scene
    public void SwitchToAppetizerMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Appetizermenu");
    }

    // Call this from your + button, passing the index of the item in the cart
    public void IncreaseAmount(int itemIndex)
    {
        UpdateAmount(itemIndex, 1);
    }

    // Call this from your - button, passing the index of the item in the cart
    public void DecreaseAmount(int itemIndex)
    {
        UpdateAmount(itemIndex, -1);
    }

    private void UpdateAmount(int itemIndex, int delta)
    {
        var items = Shoppingcart.OrderedItems;
        if (itemIndex < 0 || itemIndex >= items.Count) return;

        string item = items[itemIndex];
        // Find and update the xN part
        var match = System.Text.RegularExpressions.Regex.Match(item, @"^(.*?€) x(\d+)$");
        if (match.Success)
        {
            string nameAndPrice = match.Groups[1].Value;
            int amount = int.Parse(match.Groups[2].Value);
            amount = Mathf.Clamp(amount + delta, 1, 99); // Prevent going below 1
            items[itemIndex] = $"{nameAndPrice} x{amount}";
            UpdateOrderSummary();
        }
    }
}
