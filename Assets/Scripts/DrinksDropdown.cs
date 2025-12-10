using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DrinksDropdown : MonoBehaviour
{
    private Dropdown dropdown;
    private Drinks drinksSource;
    private int selectedIndex = 0;
    public Button orderButton; // Assign in Inspector

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
        if (drinksSource != null && drinksSource.drinkItems != null)
        {
            foreach (var drink in drinksSource.drinkItems)
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
        if (orderButton != null)
        {
            orderButton.onClick.AddListener(OnOrderButtonClicked);
        }
    }

    void OnDropdownValueChanged(int index)
    {
        selectedIndex = index;
        Debug.Log("Selected drink: " + dropdown.options[index].text);
    }

    void OnOrderButtonClicked()
    {
        if (drinksSource != null && drinksSource.drinkItems != null && selectedIndex < drinksSource.drinkItems.Count)
        {
            var drink = drinksSource.drinkItems[selectedIndex];
            string item = $"{drink.name} {drink.price:F2}€";
            Shoppingcart.Instance.AddItem(item);
            Debug.Log($"Added to cart: {item}");
        }
    }
}
