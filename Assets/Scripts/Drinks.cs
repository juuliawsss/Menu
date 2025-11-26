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
    [Header("Juomavalikoima")]
    public List<DrinkItem> drinkItems = new List<DrinkItem>();
    
    [Header("UI Viittaukset")]
    public Transform drinkContainer;
    public GameObject drinkItemPrefab;
    public TextMeshProUGUI titleText;
    public Button backButton;
    
    [Header("Valinta")]
    public TextMeshProUGUI selectedDrinkInfo;
    public Button orderButton;
    
    private DrinkItem selectedDrink;

    void Start()
    {
        InitializeDrinkMenu();
        SetupDrinks();
        DisplayDrinks();
    }

    void InitializeDrinkMenu()
    {
        if (titleText != null)
            titleText.text = "JUOMAT";
            
        if (backButton != null)
            backButton.onClick.AddListener(GoBack);
            
        if (orderButton != null)
        {
            orderButton.onClick.AddListener(OrderSelectedDrink);
            orderButton.interactable = false;
        }
    }

    void SetupDrinks()
    {
        if (drinkItems.Count == 0)
        {
            drinkItems.Add(new DrinkItem 
            { 
                name = "Punaviini", 
                price = 10.00f 
            });
            
            drinkItems.Add(new DrinkItem 
            { 
                name = "Valkoviini", 
                price = 10.00f 
            });
            
            drinkItems.Add(new DrinkItem 
            { 
                name = "Peroni-olut", 
                price = 8.00f 
            });
            
            drinkItems.Add(new DrinkItem 
            { 
                name = "Cola", 
                price = 3.00f 
            });
        }
    }

    void DisplayDrinks()
    {
        if (drinkContainer == null) return;

        // Clear existing items
        foreach (Transform child in drinkContainer)
        {
            Destroy(child.gameObject);
        }

        // Create drink items
        for (int i = 0; i < drinkItems.Count; i++)
        {
            CreateDrinkItem(drinkItems[i], i);
        }
    }

    void CreateDrinkItem(DrinkItem drink, int index)
    {
        GameObject itemGO;
        
        if (drinkItemPrefab != null)
        {
            itemGO = Instantiate(drinkItemPrefab, drinkContainer);
        }
        else
        {
            // Create a simple UI item if no prefab is provided
            itemGO = CreateSimpleDrinkItem();
            itemGO.transform.SetParent(drinkContainer);
        }

        // Setup the drink item
        SetupDrinkItemComponents(itemGO, drink, index);
    }

    GameObject CreateSimpleDrinkItem()
    {
        GameObject item = new GameObject("DrinkItem", typeof(RectTransform));
        
        // Add background
        Image bg = item.AddComponent<Image>();
        bg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        
        // Add button component
        Button button = item.AddComponent<Button>();
        
        // Set size
        RectTransform rect = item.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 80);
        
        // Add text
        GameObject textGO = new GameObject("Text", typeof(RectTransform));
        textGO.transform.SetParent(item.transform);
        
        TextMeshProUGUI text = textGO.AddComponent<TextMeshProUGUI>();
        text.text = "Juoma";
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = 18;
        text.color = Color.white;
        
        // Position text
        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
        
        return item;
    }

    void SetupDrinkItemComponents(GameObject itemGO, DrinkItem drink, int index)
    {
        // Setup text
        TextMeshProUGUI[] texts = itemGO.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length > 0)
        {
            texts[0].text = $"{drink.name}\n{drink.price:F2}€";
        }

        // Setup image if available
        Image[] images = itemGO.GetComponentsInChildren<Image>();
        if (images.Length > 1 && drink.image != null) // Skip background image
        {
            images[1].sprite = drink.image;
        }

        // Setup button
        Button button = itemGO.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectDrink(drink));
            button.interactable = drink.isAvailable;
        }

        // Gray out if not available
        if (!drink.isAvailable)
        {
            CanvasGroup canvasGroup = itemGO.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = itemGO.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0.5f;
        }
    }

    public void SelectDrink(DrinkItem drink)
    {
        if (!drink.isAvailable) return;

        selectedDrink = drink;
        
        if (selectedDrinkInfo != null)
        {
            selectedDrinkInfo.text = $"Valittu: {drink.name}\nHinta: {drink.price:F2}€";
        }
        
        if (orderButton != null)
        {
            orderButton.interactable = true;
        }

        Debug.Log($"Valittu juoma: {drink.name}");
    }

    public void OrderSelectedDrink()
    {
        if (selectedDrink == null) return;

        Debug.Log($"Tilataan: {selectedDrink.name} hintaan {selectedDrink.price:F2}€");
        
        if (selectedDrinkInfo != null)
        {
            selectedDrinkInfo.text = $"Tilattu: {selectedDrink.name}\nKiitos tilauksesta!";
        }
        
        // Reset selection after a delay
        Invoke("ResetSelection", 2f);
    }

    void ResetSelection()
    {
        selectedDrink = null;
        
        if (selectedDrinkInfo != null)
        {
            selectedDrinkInfo.text = "Valitse juoma nähdäksesi tiedot";
        }
        
        if (orderButton != null)
        {
            orderButton.interactable = false;
        }
    }

    public void GoBack()
    {
        Debug.Log("Paluu päävalikkoon");
        // Add logic to return to main menu
    }
}
