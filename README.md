# Menu

## Orders: Storage and Admin Guide

### Where are orders stored?
- Editor (Unity Editor play mode): Orders are written to the project folder under `Orders/Json`.
	- Example: `<project-root>/Orders/Json`
- Player builds (Windows/macOS/Linux/iOS/Android): Orders are written under Unity's persistent data directory, inside `Orders/Json`.
	- Path base: `Application.persistentDataPath`
	- Typical Windows path: `C:\Users\\<User>\\AppData\\LocalLow\\<CompanyName>\\<ProductName>\\Orders\\Json`
- Every successful save logs the absolute file path to the Unity Console: `Order saved to JSON: <full-path>`.

### JSON structure example
Each placed order is saved to its own file with a name like `order_YYYYMMDD_HHmmssfff_<id6>.json` and the following structure:

```json
{
	"id": "a1b2c3d4e5f6",
	"createdAt": "2025-12-17T12:34:56.789Z",
	"items": [
		{
			"name": "Pasta Bolognese - Spagettia, bolognesekastiketta ja parmesaanilastuja. (Laktoositon, vegaaninen)",
			"unitPrice": 15.0,
			"quantity": 2,
			"lineTotal": 30.0
		},
		{
			"name": "Punaviini",
			"unitPrice": 6.5,
			"quantity": 1,
			"lineTotal": 6.5
		}
	],
	"total": 36.5
}
```

Field meanings:
- `id`: Unique identifier for the order.
- `createdAt`: UTC timestamp in ISO 8601 format.
- `items[]`: Line items with `name`, `unitPrice`, `quantity`, and `lineTotal`.
- `total`: Sum of all `lineTotal` values.

### How can the admin see the orders?
- In the Unity Editor:
	- Open the folder directly in your OS file explorer: `<project-root>/Orders/Json`.
	- Each file name indicates the time and a short id, e.g., `order_20251217_123456789_abc123.json`.
- In built players:
	- Check the Unity Console/logs for the exact path printed after each save: `Order saved to JSON: ...`.
	- Or navigate to the persistent data directory for your platform. On Windows this is typically:
		`C:\Users\\<User>\\AppData\\LocalLow\\<CompanyName>\\<ProductName>\\Orders\\Json`.
	- `<CompanyName>` and `<ProductName>` are set in Project Settings → Player.

Optional helper (for a debug/admin button):

```csharp
using System.IO;
using UnityEngine;

public class OpenOrdersFolder : MonoBehaviour
{
		public void OpenFolder()
		{
#if UNITY_EDITOR
				string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
				string path = Path.Combine(projectRoot, "Orders", "Json");
#else
				string path = Path.Combine(Application.persistentDataPath, "Orders", "Json");
#endif
				Directory.CreateDirectory(path);
				Application.OpenURL("file://" + path);
		}
}
```

