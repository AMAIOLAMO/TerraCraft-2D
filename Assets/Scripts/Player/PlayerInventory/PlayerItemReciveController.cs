using UnityEngine;

public class PlayerItemReciveController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("ItemEntity")){
            ItemSlotSlotItemContainer thisItemSlotSlotItemContainer = other.gameObject.GetComponent<ItemSlotSlotItemContainer>();

            if(thisItemSlotSlotItemContainer.blockInfo != null)
                PerformPlayerRecievedAnItem(thisItemSlotSlotItemContainer.blockInfo);
            else if(thisItemSlotSlotItemContainer.itemInfo != null)
                PerformPlayerRecievedAnItem(thisItemSlotSlotItemContainer.itemInfo);
            else
                Debug.Log("This item Entity is has no Item's in it! Item Instance ID:" + other.gameObject.GetInstanceID());
            Destroy(other.gameObject);
        }
    }
    //------------------This script it's own methods-------------------------//
    private void PerformPlayerRecievedAnItem(BlockInfo blockInfo){
        //this method will get the current item that the player get
        //and push the things inside to the inventory or the hotbar slot
        //we dont need to check if this block info has a block inside or not..(cuz it already checked)

            //hotbar slot first
        if(!PlayerInventoryController.Instance.CheckIsHotBarSlotsFull()){
            //made for the hot bar slots
            PlayerHotBarSlotController thisCurrentEmptyHotBarSlotController = PlayerInventoryController.Instance.GetFirstSameItemButNotFullHotBarSlot(blockInfo);
            
            if(thisCurrentEmptyHotBarSlotController != null){
                thisCurrentEmptyHotBarSlotController.slotInfoContainer.currentItems ++;
                thisCurrentEmptyHotBarSlotController.UpdateCountText();
            }
            else{
                thisCurrentEmptyHotBarSlotController = PlayerInventoryController.Instance.GetLastEmptySlotPlayerHotBarSlotController();
                thisCurrentEmptyHotBarSlotController.slotInfoContainer.currentItems = 1;
                thisCurrentEmptyHotBarSlotController.slotInfoContainer.blockInfo = blockInfo;
                thisCurrentEmptyHotBarSlotController.UpdateAllDisplay();
            }
            thisCurrentEmptyHotBarSlotController.slotInfoContainer.blockInfo = blockInfo;
            thisCurrentEmptyHotBarSlotController.UpdateIMGDisplay();
        }
            //inventory last
        else if(!PlayerInventoryController.Instance.CheckIsInventorySlotsFull()){
            //made for the inventory slots
            PlayerInventorySlotController thisCurrentEmptyInventorySlotController = PlayerInventoryController.Instance.GetFirstSameItemButNotFullInventorySlot(blockInfo);
            if(thisCurrentEmptyInventorySlotController != null){
                thisCurrentEmptyInventorySlotController.currentItems ++;
                thisCurrentEmptyInventorySlotController.UpdateCountText();
            }
            else{
                thisCurrentEmptyInventorySlotController = PlayerInventoryController.Instance.GetLastEmptySlotPlayerInventoryController();
                thisCurrentEmptyInventorySlotController.currentItems = 1;
                thisCurrentEmptyInventorySlotController.blockInfo = blockInfo;
                thisCurrentEmptyInventorySlotController.UpdateAllDisplay();
            }
        }
        else
            Debug.Log("Player Inventory / hotbar slot already full!");
    }
    private void PerformPlayerRecievedAnItem(ItemInfo itemInfo){
        //same as up
        //not implemented
    }
}
