using UnityEngine;

public class SlotInfo : MonoBehaviour {
    public BlockInfo blockInfo;
    public ItemInfo itemInfo;
    public int maxItems = 64;
    public int currentItems = 0;
    
    public bool CheckIfThisSlotInfoIsEmpty(){
        return (blockInfo == null && itemInfo == null);
    }
    public void Add(int val){
        if((currentItems + val) > 0 && (currentItems + val) <= maxItems){
            currentItems += val;
        }
        else if((currentItems + val) <= 0){
            currentItems = 0;
            blockInfo = null;
            itemInfo = null;
        }
        else {
            Debug.Log("This is the bug that u re finding: " + currentItems + val);
        }
    }
}