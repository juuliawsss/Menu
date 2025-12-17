using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TMP_InputField quantityInput;
    [SerializeField] private Button addButton;

    private string displayName;
    private float unitPrice;

    void Awake()
    {
        if (addButton != null)
        {
            addButton.onClick.AddListener(AddToCart);
        }
    }

    public void Bind(string displayName, float unitPrice)
    {
        this.displayName = displayName;
        this.unitPrice = unitPrice;
        if (nameText != null) nameText.text = displayName;
        if (priceText != null) priceText.text = unitPrice.ToString("0.00") + "€";
        if (quantityInput != null && string.IsNullOrWhiteSpace(quantityInput.text)) quantityInput.text = "1";
    }

    public void AddToCart()
    {
        int qty = 1;
        if (quantityInput != null && int.TryParse(quantityInput.text, out int parsed) && parsed > 0)
        {
            qty = parsed;
        }
        var cart = GameObject.FindFirstObjectByType<Shoppingcart>();
        if (cart != null)
        {
            cart.AddItem(displayName, unitPrice, qty);
        }
        else
        {
            Debug.LogWarning("MenuItemUI: Shoppingcart not found in scene.");
        }
    }
}
