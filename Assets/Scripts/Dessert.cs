using UnityEngine;

public class Dessert : MonoBehaviour
{
    // Reference to the Shoppingcart object (assign in Inspector)
    public GameObject shoppingcart;

    private string pannacotta = "Pannacotta - Vaniljalla maustettua kermavanukasta ja granaattiomenakastiketta. (Gluteeniton, Laktoositon) 9.00€";
    private string kahvi = "Aito italialaistyylinen kahvi 2.50€";
    private string juustokakku = "Italialainen tuorejuustokakku 10.00€";
    private string gelato = "Gelato - Italialainen jäätelö, mansikka 6.00€";

    // Call these from UI buttons for each dessert
    public void OrderPannacotta()
    {
        AddDessertToCart(pannacotta);
    }
    public void OrderKahvi()
    {
        AddDessertToCart(kahvi);
    }
    public void OrderJuustokakku()
    {
        AddDessertToCart(juustokakku);
    }
    public void OrderGelato()
    {
        AddDessertToCart(gelato);
    }

    private void AddDessertToCart(string dessert)
    {
        if (shoppingcart != null)
        {
            Shoppingcart cartScript = shoppingcart.GetComponent<Shoppingcart>();
            if (cartScript != null)
            {
                cartScript.AddItem(dessert);
                Debug.Log($"Added dessert: {dessert}");
            }
        }
        else
        {
            Debug.LogWarning("Shoppingcart reference not set in Dessert.cs");
        }
    }
}
