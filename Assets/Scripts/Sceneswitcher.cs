using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToAppetizerMenu()
    {
        SceneManager.LoadScene("appetizermenu");
    }

    public void SwitchToOrderSummary()
    {
        SceneManager.LoadScene("OrderSummary");
    }

    public void SwitchToMains()
    {
        SceneManager.LoadScene("Mains");
    }

    public void SwitchToDesserts()
    {
        SceneManager.LoadScene("Dessert");
    }

    public void SwitchToDrinks()
    {
        SceneManager.LoadScene("Drinks");
    }
}
