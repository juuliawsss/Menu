using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DessertDropdown : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject Kakkudropdown;
    public GameObject PannacottaDropdown;
    public GameObject KahviDropdown;
    public GameObject JäätelöDropdown;

    public static int Amount = 1;
    public static string SelectedDessert = "";
    public static string CurrentDessert = "Pannacotta - Vaniljalla maustettua kermavanukasta ja granaattiomenakastiketta. (Gluteeniton, Laktoositon) 9.00€";

    // Dessert names and prices
    private Dictionary<string, float> dessertPrices = new Dictionary<string, float>()
    {
        {"Pannacotta - Vaniljalla maustettua kermavanukasta ja granaattiomenakastiketta. (Gluteeniton, Laktoositon)", 9.00f},
        {"Aito italialaistyylinen kahvi", 2.50f},
        {"Italialainen tuorejuustokakku", 10.00f},
        {"Gelato - Italialainen jäätelö, mansikka", 6.00f}
    };

    // Dessert counts
    private Dictionary<string, int> dessertCounts = new Dictionary<string, int>()
    {
        {"Pannacotta - Vaniljalla maustettua kermavanukasta ja granaattiomenakastiketta. (Gluteeniton, Laktoositon)", 0},
        {"Aito italialaistyylinen kahvi", 0},
        {"Italialainen tuorejuustokakku", 0},
        {"Gelato - Italialainen jäätelö, mansikka", 0}
    };

    // Set the amount from UI or code
    public void SetAmount(int value)
    {
        if (value > 0)
        {
            Amount = value;
            Debug.Log($"Dessert Amount set to: {Amount}");
        }
        else
        {
            Debug.LogWarning("Invalid dessert amount entered.");
        }
    }

    // Set the current dessert
    public static void SetCurrentDessert(string dessert)
    {
        CurrentDessert = dessert;
        Debug.Log($"Current dessert set to: {dessert}");
    }

    // Add the selected dessert and amount to the cart
    public void AddDessertToCart(string dessertName)
    {
        SelectedDessert = dessertName;
        Debug.Log($"Dropdown AddDessertToCart called with: {dessertName}, Amount: {Amount}");
        var cartObj = GameObject.FindFirstObjectByType<Shoppingcart>();
        if (cartObj != null)
        {
            cartObj.AddItem(dessertName + $" x{Amount}");
            Debug.Log($"Added {dessertName} x{Amount} to cart");
        }
        else
        {
            Debug.LogWarning("Shoppingcart object not found.");
        }
    }

    // Go to order summary
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

    // Call this to add one to a dessert
    public void AddDessert(string dessertName)
    {
        if (dessertCounts.ContainsKey(dessertName))
            dessertCounts[dessertName]++;
    }

    // Call this to remove one from a dessert
    public void RemoveDessert(string dessertName)
    {
        if (dessertCounts.ContainsKey(dessertName) && dessertCounts[dessertName] > 0)
            dessertCounts[dessertName]--;
    }

    // Get the count for a dessert
    public int GetDessertCount(string dessertName)
    {
        return dessertCounts.ContainsKey(dessertName) ? dessertCounts[dessertName] : 0;
    }

    // Get the total price for a dessert
    public float GetDessertTotal(string dessertName)
    {
        if (dessertCounts.ContainsKey(dessertName) && dessertPrices.ContainsKey(dessertName))
            return dessertCounts[dessertName] * dessertPrices[dessertName];
        return 0f;
    }

    // Get all dessert counts and totals as a string (for UI)
    public string GetSummaryText()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var kvp in dessertCounts)
        {
            if (kvp.Value > 0)
            {
                float price = dessertPrices[kvp.Key];
                sb.AppendLine($"{kvp.Key} x{kvp.Value} ({price:0.00}€ each, {kvp.Value * price:0.00}€ total)");
            }
        }
        return sb.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // No dropdown logic needed
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAddDessertButtonClicked(string dessertName, int amount)
    {
        SetCurrentDessert(dessertName);
        SetAmount(amount);
        AddDessertToCart(dessertName);
    }
}
