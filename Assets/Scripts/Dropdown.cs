using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public static int Amount = 1;

    // Call this from a UI input field or dropdown to set the amount
    public void SetAmount(string value)
    {
        if (int.TryParse(value, out int result) && result > 0)
        {
            Amount = result;
            Debug.Log($"Amount set to: {Amount}");
            // Add item to shopping cart before loading summary
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
        else
        {
            Debug.LogWarning("Invalid amount entered.");
        }
    }

    // Optionally, get the current amount
    public int GetAmount()
    {
        return Amount;
    }

    void Start()
    {
        // ...existing code...
    }

    void Update()
    {
        // ...existing code...
    }
}
