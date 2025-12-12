using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class DrinkItem
{
    public string name;
    public float price;
    public Sprite image;
    public bool isAvailable = true;
}

public class Drinks : MonoBehaviour
{
    private List<DrinkItem> drinkItems = new List<DrinkItem>();
    private DrinkItem selectedDrink;

    void Start()
    {
        
    }

    void SetupDrinks()
    {
        if (drinkItems.Count == 0)
        {
            drinkItems.Add(new DrinkItem { name = "Punaviini", price = 10.00f });
            drinkItems.Add(new DrinkItem { name = "Valkoviini", price = 10.00f });
            drinkItems.Add(new DrinkItem { name = "Peroni-olut", price = 8.00f });
            drinkItems.Add(new DrinkItem { name = "Cola", price = 3.00f });
        }
    }

    // If you want to access the drinks from other scripts:
    public List<DrinkItem> GetDrinks() => drinkItems;

    public void SelectDrink(DrinkItem drink)
    {
        if (!drink.isAvailable) return;
        selectedDrink = drink;
        // Handle selection logic
    }

    public void OrderSelectedDrink()
    {
        if (selectedDrink == null) return;
        Debug.Log($"Tilataan: {selectedDrink.name} hintaan {selectedDrink.price:F2}€");
        // Handle order logic
    }

    public void GoBack()
    {
        Debug.Log("Paluu päävalikkoon");
        // Handle back logic
    }

    void ResetSelection()
    {
        selectedDrink = null;
        // Handle reset logic
    }

    public void LoadDrinksScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Drinks");
    }
}
