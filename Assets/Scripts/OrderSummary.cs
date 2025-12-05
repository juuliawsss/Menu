using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class OrderSummary : MonoBehaviour
{
    public TextMeshProUGUI orderText; // Assign in Inspector

    void Start()
    {
        // Get the ordered items from Shoppingcart (using a static/shared list for simplicity)
        List<string> items = Shoppingcart.OrderedItems;
        if (items != null && items.Count > 0)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Your Order:");
            sb.AppendLine(""); // Empty line for better formatting
            foreach (var item in items)
            {
                // Remove any unsupported or control characters
                string cleanItem = System.Text.RegularExpressions.Regex.Replace(item, "[^\u0020-\u007E€]", "");
                sb.AppendLine(cleanItem.Trim());
                sb.AppendLine(""); // Empty line between items
            }
            orderText.text = sb.ToString();
        }
        else
        {
            orderText.text = "Your cart is empty.";
        }
    }
    // Call this method to switch back to the Appetizermenu scene
    public void SwitchToAppetizerMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Appetizermenu");
    }
}
