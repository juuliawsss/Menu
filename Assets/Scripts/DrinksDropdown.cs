using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Globalization;

public class DrinksDropdown : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Dropdown drinksDropdown;
    [SerializeField] private TMP_Dropdown amountDropdown;
    [SerializeField] private TMP_Text totalText;

    private Dictionary<int, float> drinkPrices = new Dictionary<int, float>
    {
        { 1, 10.00f }, // Punaviini
        { 2, 10.00f }, // Valkoviini
        { 3, 8.00f },  // Peroni
        { 4, 3.00f }   // Cola
    };

    void Start()
    {
        SetupDrinksDropdown();
        SetupAmountDropdown();

        drinksDropdown.onValueChanged.AddListener(_ => UpdateTotal());
        amountDropdown.onValueChanged.AddListener(_ => UpdateTotal());

        UpdateTotal();
    }

    private void SetupDrinksDropdown()
    {
        drinksDropdown.ClearOptions();
        drinksDropdown.AddOptions(new List<string>
        {
            "Valitse juoma",
            "Punaviini - 10.00€",
            "Valkoviini - 10.00€",
            "Peroni-olut - 8.00€",
            "Cola - 3.00€"
        });

        drinksDropdown.value = 0;
    }

    private void SetupAmountDropdown()
    {
        amountDropdown.ClearOptions();
        amountDropdown.AddOptions(new List<string>
        {
            "x1",
            "x2",
            "x3",
            "x4",
            "x5"
        });

        amountDropdown.value = 0;
    }

    private void UpdateTotal()
    {
        int drinkIndex = drinksDropdown.value;
        int amount = amountDropdown.value + 1;

        if (drinkIndex == 0 || !drinkPrices.ContainsKey(drinkIndex))
        {
            totalText.text = "Total: 0.00€";
            return;
        }

        float total = drinkPrices[drinkIndex] * amount;
        totalText.text = $"Total: {total.ToString("F2", CultureInfo.InvariantCulture)}€";

        Debug.Log($"Selected: {drinksDropdown.options[drinkIndex].text} x{amount} = {total}€");
    }

    // Kutsu tätä nappulasta: "Lisää ostoskoriin"
    public void AddToCart()
    {
        int drinkIndex = drinksDropdown.value;
        if (drinkIndex == 0) return;

        int amount = amountDropdown.value + 1;
        string drinkName = drinksDropdown.options[drinkIndex].text;
        float total = drinkPrices[drinkIndex] * amount;

        string item = $"{drinkName} x{amount} - {total:F2}€";

        Shoppingcart.Instance.AddItem(item);
        Debug.Log("Added to cart: " + item);
    }
}

