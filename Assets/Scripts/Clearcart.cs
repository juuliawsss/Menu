using UnityEngine;

public class Clearcart : MonoBehaviour
{
    // Hook this to the Clear Cart button in the OrderSummary scene
    public void OnClearCartClicked()
    {
        // Clear the items shown in OrderSummary
        Shoppingcart.OrderedItems.Clear();

        // Refresh the OrderSummary UI if present
        var summary = GameObject.FindFirstObjectByType<OrderSummary>();
        if (summary != null)
        {
            summary.UpdateOrderSummary();
        }
    }
}
