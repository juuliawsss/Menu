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
            orderText.text = "Your Order:\n";
            foreach (var item in items)
            {
                orderText.text += item + "\n";
            }
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
