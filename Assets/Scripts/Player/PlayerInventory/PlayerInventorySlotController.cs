using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerInventorySlotController : MonoBehaviour
{
    [Header("Basic Info")]
    public Image playerInventorySlotIMG;
    public TextMeshProUGUI countText;
    [Header("Slot Infos")]
    public BlockInfo blockInfo;
    public ItemInfo itemInfo;
    public int maxItems = 64;
    public int currentItems;
    public void UpdateAllDisplay(){
        UpdateDisplayPlayerInventorySlotIMG();
        UpdateCountText();

    }

    public void UpdateDisplayPlayerInventorySlotIMG()
    {
        //this method will update the display
        if (!IsThisInventorySlotEmpty())
        {
            playerInventorySlotIMG.enabled = true;
            
            if (blockInfo != null)
                playerInventorySlotIMG.sprite = blockInfo.sprite;

            else if(itemInfo != null)
                playerInventorySlotIMG.sprite = itemInfo.sprite;
        }
        else
        {
            playerInventorySlotIMG.enabled = false;
        }
    }
    public void UpdateCountText(){
        countText.text = currentItems.ToString();
    }
    public bool IsThisInventorySlotEmpty(){
        //this method will return if this slot is empty
        return (blockInfo == null && itemInfo == null && currentItems < maxItems);
    }
    public void Add(int value){
        //this method is the same as the name(it means it will add counts inside)
        if(currentItems + value <= maxItems && currentItems + value > 0){
            currentItems += value;
        }
        else if(currentItems + value <= 0){
            currentItems = 0;
            blockInfo = null;
            itemInfo = null;
        }
    }
}
