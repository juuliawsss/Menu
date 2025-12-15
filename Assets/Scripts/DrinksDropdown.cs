using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DrinksDropdown : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Dropdown dropdown;

    private List<string> drinks = new List<string>
    {
        "Valitse juoma",
        "Punaviini - 10.00€",
        "Valkoviini - 10.00€",
        "Peroni-olut - 8.00€",
        "Cola - 3.00€"
    };

    void Start()
    {
        if (dropdown == null)
            dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        dropdown.AddOptions(drinks);

        dropdown.value = 0;
        dropdown.RefreshShownValue();

        dropdown.onValueChanged.AddListener(OnDrinkSelected);
    }

    private void OnDrinkSelected(int index)
    {
        if (index == 0) return; // "Valitse juoma"

        string selectedDrink = dropdown.options[index].text;

        Debug.Log("Selected drink: " + selectedDrink);

        // Lähetetään OrderSummaryyn / ostoskoriin
        OrderDropdown.SetCurrentMenuItem(selectedDrink);
    }
}
