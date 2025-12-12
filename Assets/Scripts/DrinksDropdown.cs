using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DrinksDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown; // assign in Inspector, remove 'private'
    private Drinks drinksSource;
    private int selectedIndex = 0;
    private int selectedAmount = 1; // Default amount

    void Start()
    {
        if (drinksSource == null)
        {
            drinksSource = FindFirstObjectByType<Drinks>();
        }

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        var drinksList = drinksSource != null ? drinksSource.GetDrinks() : null;
        if (drinksList != null)
        {
            foreach (var drink in drinksList)
            {
                options.Add(new TMP_Dropdown.OptionData($"{drink.name} {drink.price:F2}€"));
            }
        }
        else
        {
            options.Add(new TMP_Dropdown.OptionData("No drinks found"));
        }
        if (dropdown != null)
        {
            dropdown.options = options;
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
    }

    void OnDropdownValueChanged(int index)
    {
        selectedIndex = index;
        Debug.Log("Selected drink: " + dropdown.options[index].text);
    }

    // Call this from UI (e.g. input field or +/- buttons)
    public void SetAmount(int amount)
    {
        selectedAmount = Mathf.Max(1, amount); // Ensure at least 1
    }

    // This method can be assigned in the Inspector to add the currently selected drink to the cart
    public void AddToCart()
    {
        var drinksList = drinksSource != null ? drinksSource.GetDrinks() : null;
        if (drinksList != null && selectedIndex < drinksList.Count)
        {
            var drink = drinksList[selectedIndex];
            string item = $"{drink.name} {drink.price:F2}€ x{selectedAmount}";
            Debug.Log($"AddToCart called: {item}, amount: {selectedAmount}");
            Shoppingcart.Instance.AddItem(item);
            Debug.Log($"Added to cart (Inspector): {item}");
        }
        else
        {
            Debug.LogWarning($"AddToCart: No valid drink selected or drinksList is null. selectedIndex={selectedIndex}");
        }
    }
}
