using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class MenuData
{
    public List<MenuCategory> categories;
}

[Serializable]
public class MenuCategory
{
    public string name;
    public List<MenuItemData> items;
}

[Serializable]
public class MenuItemData
{
    public string name;
    public string description;
    public float price;
}

public class MenuService : MonoBehaviour
{
    public static MenuData Current;

    public static string MenuPath()
    {
        return Path.Combine(Application.streamingAssetsPath, "menu.json");
    }

    public IEnumerator LoadMenu(Action<MenuData> onLoaded)
    {
        string path = MenuPath();
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_WSA
        yield return LoadFromFile(path, onLoaded);
#else
        yield return LoadFromWeb(path, onLoaded);
#endif
    }

    private IEnumerator LoadFromFile(string path, Action<MenuData> onLoaded)
    {
        try
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"menu.json not found at {path}");
                onLoaded?.Invoke(null);
                yield break;
            }
            string json = File.ReadAllText(path);
            Current = JsonUtility.FromJson<MenuData>(json);
            onLoaded?.Invoke(Current);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load menu: {ex}");
            onLoaded?.Invoke(null);
        }
        yield break;
    }

    private IEnumerator LoadFromWeb(string url, Action<MenuData> onLoaded)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load menu: {req.error}");
                onLoaded?.Invoke(null);
                yield break;
            }
            Current = JsonUtility.FromJson<MenuData>(req.downloadHandler.text);
            onLoaded?.Invoke(Current);
        }
    }
}
