# Menu

## Orders: Storage and Admin Guide

### Where are orders stored?
- Active orders:
	- Editor (Unity Editor play mode): `<project-root>/Orders/Active`
	- Builds: `Application.persistentDataPath/Orders/Active`
- Archived orders (after "Mark as completed"):
	- Editor: `<project-root>/Orders/Archive`
	- Builds: `Application.persistentDataPath/Orders/Archive`
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
- Use the Admin view (PIN-protected) to see Active orders and complete them.
- Or open the folder directly:
	- Editor: `<project-root>/Orders/Active` (and `Orders/Archive` for completed).
	- Builds: `Application.persistentDataPath/Orders/Active` and `/Orders/Archive`.
	- Windows example: `C:\Users\\<User>\\AppData\\LocalLow\\<CompanyName>\\<ProductName>\\Orders\\Active`.
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
				string path = Path.Combine(projectRoot, "Orders", "Active");
#else
				string path = Path.Combine(Application.persistentDataPath, "Orders", "Active");
#endif
				Directory.CreateDirectory(path);
				Application.OpenURL("file://" + path);
		}
}
```

## Admin View (PIN)
- Add `AdminPanel` to a Canvas.
- Assign: `pinInput` (TMP_InputField), `lockedView` (login area), `ordersView` (list area), `listParent` (content transform), and `orderItemPrefab`.
- `orderItemPrefab` should include `OrderListItem` with texts and a Complete button. The Complete button uses `OrderListItem`'s built-in listener.
- Default PIN is `1234` (change in Inspector).

## Menu From JSON
- File location: Assets/StreamingAssets/menu.json
- Edit this file to change products without touching code.
- Add `MenuUIBuilder` to a GameObject and assign `listParent` and an `itemPrefab` that has `MenuItemUI`.
- `MenuItemUI` exposes an Add button which adds the product to the cart using price and quantity.

`menu.json` schema (excerpt):

```json
{
	"categories": [
		{ "name": "Mains", "items": [ { "name": "Pasta Bolognese", "description": "...", "price": 15.0 } ] }
	]
}
```

