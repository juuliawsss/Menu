using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class OrderSaver
{
    [Serializable]
    public class OrderItem
    {
        public string name;
        public float unitPrice;
        public int quantity;
        public float lineTotal;
    }

    [Serializable]
    public class OrderJson
    {
        public string id;          // unique id per order
        public string createdAt;   // ISO 8601 UTC timestamp
        public List<OrderItem> items;
        public float total;
    }

    public static string Save(List<string> rawItems)
    {
        try
        {
            if (rawItems == null || rawItems.Count == 0)
            {
                Debug.LogWarning("OrderSaver.Save called with empty items.");
                return null;
            }

            var items = new List<OrderItem>();
            float total = 0f;

            foreach (var raw in rawItems)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                var item = ParseRawItem(raw);
                items.Add(item);
                total += item.lineTotal;
            }

            var order = new OrderJson
            {
                id = Guid.NewGuid().ToString("N"),
                createdAt = DateTime.UtcNow.ToString("o"),
                items = items,
                total = (float)Math.Round(total, 2)
            };

            string json = JsonUtility.ToJson(order, true);

            string ordersDir = OrdersPath.ActiveDir();
            Directory.CreateDirectory(ordersDir);

            string fileName = $"order_{DateTime.Now:yyyyMMdd_HHmmssfff}_{order.id.Substring(0, 6)}.json";
            string fullPath = Path.GetFullPath(Path.Combine(ordersDir, fileName));

            File.WriteAllText(fullPath, json, Encoding.UTF8);
            Debug.Log($"Order saved to JSON: {fullPath}");
            return fullPath;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save order JSON: {ex}");
            return null;
        }
    }

    private static OrderItem ParseRawItem(string raw)
    {
        // Defaults
        int quantity = 1;
        float unitPrice = 0f;
        string name = raw.Trim();

        var amountMatch = Regex.Match(raw, @"x(\d+)\s*$");
        if (amountMatch.Success && int.TryParse(amountMatch.Groups[1].Value, out int q))
        {
            quantity = Mathf.Clamp(q, 1, 999);
        }

        var priceMatch = Regex.Match(raw, @"(\d+[\.,]\d{2})€");
        if (priceMatch.Success && float.TryParse(priceMatch.Groups[1].Value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float p))
        {
            unitPrice = p;
        }

        // Remove trailing amount and redundant whitespace
        name = Regex.Replace(name, @"\s*x\d+\s*$", "");
        name = name.Trim();

        // Optionally remove price token from name if present
        name = Regex.Replace(name, @"\s*\d+[\.,]\d{2}€", "").Trim();

        return new OrderItem
        {
            name = name,
            unitPrice = unitPrice,
            quantity = quantity,
            lineTotal = (float)Math.Round(unitPrice * quantity, 2)
        };
    }

        // Path resolution handled by OrdersPath
}
