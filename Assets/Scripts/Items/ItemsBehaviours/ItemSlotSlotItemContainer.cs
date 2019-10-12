using UnityEngine;

public class ItemSlotSlotItemContainer : MonoBehaviour
{
    [Header("Slot Items")]
    public BlockInfo blockInfo;
    public ItemInfo itemInfo;
    public int maxCount;
    public int currentCount;

    public bool isThisSlotMax(){
        return currentCount >= maxCount;
    }
    public bool isThisSlotEmpty(){
        return (blockInfo != null || itemInfo != null);
    }
    public void SetCurrentItem(BlockInfo blockInfo){
        //this method will help the slot to fill itself up
        this.blockInfo = blockInfo;
    }
    public void SetCurrentItem(ItemInfo itemInfo){
        //same as up
        this.itemInfo = itemInfo;
    }
}
