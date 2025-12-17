using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private Button completeButton;

    private string filePath;
    private AdminPanel admin;

    public void Bind(string filePath, OrderSaver.OrderJson order, AdminPanel admin)
    {
        this.filePath = filePath;
        this.admin = admin;

        if (headerText != null)
        {
            headerText.text = $"Order {Short(order.id)} • {order.createdAt}";
        }

        if (bodyText != null)
        {
            System.Text.StringBuilder sb = new();
            float total = order.total;
            foreach (var it in order.items)
            {
                sb.AppendLine($"• {it.name}  x{it.quantity}  {it.unitPrice:0.00}€  = {it.lineTotal:0.00}€");
            }
            sb.AppendLine("");
            sb.AppendLine($"Total: {total:0.00}€");
            bodyText.text = sb.ToString();
        }

        if (completeButton != null)
        {
            completeButton.onClick.RemoveAllListeners();
            completeButton.onClick.AddListener(OnCompleteClicked);
        }
    }

    private void OnCompleteClicked()
    {
        admin?.MarkCompleted(filePath);
    }

    private string Short(string id)
    {
        if (string.IsNullOrEmpty(id)) return "";
        return id.Length <= 6 ? id : id.Substring(0, 6);
    }
}
