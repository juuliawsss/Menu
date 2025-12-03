using UnityEngine;
using UnityEngine.SceneManagement;

public class Mains : MonoBehaviour
{
    private string pastaBolognese = "Pasta Bolognese - Spagettia, bolognesekastiketta ja parmesaanilastuja. (Laktoositon, vegaaninen) 15.00€";
    private string pizzaQuattroStagione = "Pizza, Quattro Stagione - Kinkkua, katkarapuja, tonnikalaa ja tuoreita herkkusieniä. (Laktoositon) 15.00€";
    // Call this from the Pizza Quattro Stagione text OnClick or EventTrigger
    public void OnPizzaQuattroStagioneClicked()
    {
        Debug.Log(pizzaQuattroStagione);
    }

    void Start()
    {
        // ...existing code...
    }

    void Update()
    {
        // ...existing code...
    }

    // Call this from a UI button to load the Mains scene
    public void LoadMainsScene()
    {
        SceneManager.LoadScene("Mains");
    }

    // Call this from the Pasta Bolognese text OnClick or EventTrigger
    public void OnPastaBologneseClicked()
    {
        Debug.Log(pastaBolognese);
    }
}
