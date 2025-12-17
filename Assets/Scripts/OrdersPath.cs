using System.IO;
using UnityEngine;

public static class OrdersPath
{
    public static string ActiveDir()
    {
#if UNITY_EDITOR
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        return Path.Combine(projectRoot, "Orders", "Active");
#else
        return Path.Combine(Application.persistentDataPath, "Orders", "Active");
#endif
    }

    public static string ArchiveDir()
    {
#if UNITY_EDITOR
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        return Path.Combine(projectRoot, "Orders", "Archive");
#else
        return Path.Combine(Application.persistentDataPath, "Orders", "Archive");
#endif
    }
}
