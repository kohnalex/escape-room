using UnityEngine;

public class InventoryController : BaseObservable
{
    public static int INVENTORY_MAX_ITEMS = 9;
    public PlayerContext playerContext;
    private InventoryData _inventoryData;
    
    #region Builtin

    private void Start()
    {
        if (playerContext == null)
        {
            Debug.LogError("No PlayerContext Given");
        }
        _inventoryData = playerContext.InventoryData;
    }

    #endregion

    #region Inventory

    public bool AddItem(Item item)
    {
        if (_inventoryData.Count() >= INVENTORY_MAX_ITEMS)
        {
            Debug.Log("Inventory Full");
            return false;
        }

        _inventoryData.Add(item);
        Notify();
        return true;
    }

    public Item RemoveItem(string itemId)
    {
        Item removed = _inventoryData.Remove(itemId);
        Notify();
        return removed;
    }
    
    public Item ContainsItem(string itemId)
    {
        Item contains = _inventoryData.Contains(itemId);
        return contains;
    }

    #endregion
}
