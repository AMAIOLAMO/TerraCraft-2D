using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Image))]
public class PlayerHotBarSlotController : MonoBehaviour
{
    [Header("basic infos")]
    public SlotInfo slotInfoContainer;
    public Sprite normalSprite;
    public Sprite selectedSprite;
    public Image hotbarSlotOBJIMG;
    public TextMeshProUGUI currentCountText;
    private Image hotbarSlotBoundsIMG;
    private void Awake(){
        hotbarSlotBoundsIMG = GetComponent<Image>();
    }
    private void Start(){
        if(!slotInfoContainer.CheckIfThisSlotInfoIsEmpty())
            slotInfoContainer.currentItems = 1;
        UpdateAllDisplay();
    }
    public void Select(bool boolean){
        //this method will do the sprite changing and change the player is current getting
        hotbarSlotBoundsIMG.sprite = boolean ? selectedSprite : normalSprite;
        
        PlayerBreakAndPlaceBlockController.Instance.playerCurrentPlaceBlockInfo = slotInfoContainer.blockInfo;
            //haven't implement
        // if(currentSlotInfoContainer.itemInfo != null)
        //     //then update the player's item
    }
    public void UpdateAllDisplay(){
        UpdateIMGDisplay();
        UpdateCountText();
    }
    public void UpdateIMGDisplay(){
        hotbarSlotOBJIMG.enabled = true;
        //this method will update the display of the slot
        if(slotInfoContainer.blockInfo != null){
            hotbarSlotOBJIMG.sprite = slotInfoContainer.blockInfo.sprite;
        }

        else if(slotInfoContainer.itemInfo != null)
            hotbarSlotOBJIMG.sprite = slotInfoContainer.itemInfo.sprite;
            
        else
            hotbarSlotOBJIMG.enabled = false;
    }
    public void UpdateCountText(){
        currentCountText.text = slotInfoContainer.currentItems.ToString();
    }
    public bool CheckIfThisHotBarSlotIsEmpty(){
        //this method will return if the current slot has no OBJ in it
        return (slotInfoContainer.blockInfo == null && slotInfoContainer.itemInfo == null && slotInfoContainer.currentItems < slotInfoContainer.maxItems);
    }
}
