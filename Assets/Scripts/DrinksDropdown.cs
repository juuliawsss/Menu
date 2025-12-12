using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Add this line for TextMeshProUGUI

public class DrinksDropdown : MonoBehaviour
{
    private Dropdown dropdown;
    private Drinks drinksSource;
    private int selectedIndex = 0;

    void Start()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<Dropdown>();
        }
        if (drinksSource == null)
        {
            drinksSource = FindFirstObjectByType<Drinks>();
        }

        List<string> options = new List<string>();
        var drinksList = drinksSource != null ? drinksSource.GetDrinks() : null;
        if (drinksList != null)
        {
            foreach (var drink in drinksList)
            {
                options.Add($"{drink.name} {drink.price:F2}€");
            }
        }
        else
        {
            options.Add("No drinks found");
        }
        if (dropdown != null)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
    }

    void OnDropdownValueChanged(int index)
    {
        selectedIndex = index;
        Debug.Log("Selected drink: " + dropdown.options[index].text);
    }

    // This method can be assigned in the Inspector to add the currently selected drink to the cart
    public void AddItemToCart()
    {
        var drinksList = drinksSource != null ? drinksSource.GetDrinks() : null;
        if (drinksList != null && selectedIndex < drinksList.Count)
        {
            var drink = drinksList[selectedIndex];
            string item = $"{drink.name} {drink.price:F2}€";
            Shoppingcart.Instance.AddItem(item);
            Debug.Log($"Added to cart (Inspector): {item}");
        }
    }
}
