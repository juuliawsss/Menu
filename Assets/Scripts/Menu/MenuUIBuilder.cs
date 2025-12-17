using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIBuilder : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform listParent;
    [SerializeField] private GameObject itemPrefab;

    [Header("Options")]
    [SerializeField] private bool includeDescriptionInName = true;

    void Start()
    {
        var svc = gameObject.AddComponent<MenuService>();
        StartCoroutine(svc.LoadMenu(BuildUI));
    }

    private void BuildUI(MenuData data)
    {
        if (data == null)
        {
            Debug.LogError("MenuUIBuilder: No menu data.");
            return;
        }
        if (listParent == null || itemPrefab == null)
        {
            Debug.LogWarning("MenuUIBuilder: Assign listParent and itemPrefab.");
            return;
        }

        foreach (Transform child in listParent) Destroy(child.gameObject);

        foreach (var cat in data.categories)
        {
            foreach (var item in cat.items)
            {
                var go = Instantiate(itemPrefab, listParent);
                var ui = go.GetComponent<MenuItemUI>();
                if (ui != null)
                {
                    string displayName = includeDescriptionInName && !string.IsNullOrWhiteSpace(item.description)
                        ? $"{item.name} - {item.description}"
                        : item.name;
                    ui.Bind(displayName, item.price);
                }
                else
                {
                    // Fallback simple binding if prefab doesn't include MenuItemUI
                    var texts = go.GetComponentsInChildren<TextMeshProUGUI>(true);
                    foreach (var t in texts)
                    {
                        string n = t.gameObject.name.ToLower();
                        if (n.Contains("name")) t.text = item.name;
                        if (n.Contains("price")) t.text = item.price.ToString("0.00") + "€";
                    }
                }
            }
        }
    }
}
