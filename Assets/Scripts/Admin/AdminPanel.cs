using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AdminPanel : MonoBehaviour
{
    [Header("PIN Protection")]
    [SerializeField] private string pinCode = "1234";
    [SerializeField] private TMP_InputField pinInput;
    [SerializeField] private GameObject lockedView;
    [SerializeField] private GameObject ordersView;

    [Header("Orders UI")]
    [SerializeField] private Transform listParent;
    [SerializeField] private GameObject orderItemPrefab;

    private readonly List<GameObject> spawned = new();

    void Start()
    {
        SetLocked(true);
    }

    public void TryUnlock()
    {
        if (pinInput == null)
        {
            Debug.LogWarning("AdminPanel: pinInput not assigned.");
            return;
        }
        if (pinInput.text == pinCode)
        {
            SetLocked(false);
            RefreshOrders();
        }
        else
        {
            Debug.LogWarning("AdminPanel: Wrong PIN.");
        }
    }

    public void Lock()
    {
        SetLocked(true);
    }

    private void SetLocked(bool isLocked)
    {
        if (lockedView != null) lockedView.SetActive(isLocked);
        if (ordersView != null) ordersView.SetActive(!isLocked);
    }

    public void RefreshOrders()
    {
        if (listParent == null || orderItemPrefab == null)
        {
            Debug.LogWarning("AdminPanel: Assign listParent and orderItemPrefab.");
            return;
        }

        foreach (var go in spawned) Destroy(go);
        spawned.Clear();

        string dir = OrdersPath.ActiveDir();
        Directory.CreateDirectory(dir);
        var files = Directory.GetFiles(dir, "*.json", SearchOption.TopDirectoryOnly);
        Array.Sort(files, StringComparer.OrdinalIgnoreCase);

        foreach (var file in files)
        {
            try
            {
                string json = File.ReadAllText(file);
                var order = JsonUtility.FromJson<OrderSaver.OrderJson>(json);
                var go = Instantiate(orderItemPrefab, listParent);
                var item = go.GetComponent<OrderListItem>();
                if (item != null)
                {
                    item.Bind(file, order, this);
                }
                spawned.Add(go);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load order {file}: {ex}");
            }
        }
    }

    public void MarkCompleted(string filePath)
    {
        try
        {
            string archiveDir = OrdersPath.ArchiveDir();
            Directory.CreateDirectory(archiveDir);
            string fileName = Path.GetFileName(filePath);
            string dest = Path.Combine(archiveDir, fileName);

            // Ensure unique name if file exists
            if (File.Exists(dest))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);
                dest = Path.Combine(archiveDir, name + "_" + DateTime.Now.ToString("HHmmssfff") + ext);
            }

            File.Move(filePath, dest);
            RefreshOrders();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to archive order: {ex}");
        }
    }
}
