using UnityEngine;
using CXEasyAsset;
public class ItemBehaviour : MonoBehaviour
{
    [Header("basic Info")]
    public float radius = 1.5f;
    [Range(0f,100f)]
    public float itemDampSpeed = 20f;
    public float itemMoveSpeed = 5f;
    private ItemSlotSlotItemContainer thisSlotItemContainer;
    [Range(0f,100f)]
    public float flyToSpeed = 25f;
    public bool canFloatToPlayer = true;
    private Transform playerTrans;
    private Rigidbody2D rigidBody2D;
    private void Start(){
        playerTrans = PlayerStatContainer.Instance.GetComponent<Transform>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        thisSlotItemContainer = GetComponent<ItemSlotSlotItemContainer>();
    }
    private void Update(){
        if(canFloatToPlayer)
            PerformFloatToPlayerBehavior();
    }
    private void PerformFloatToPlayerBehavior(){
        //this method will perform that this thing will float to the player
        if(CheckIfPlayerInRadius() && CheckIfPlayerIsNotFull()){
            PerformMoveToPlayer();
        }
        else if(!CXMathFunctions.CheckFloatInRange(rigidBody2D.velocity.x, -.5f, .5f)){
            float newVelocityX = Mathf.Lerp(rigidBody2D.velocity.x, 0, CXMathFunctions.Map(itemDampSpeed, 0f, 100f, 0f, 1f));
            Vector2 newVelocity = new Vector2(newVelocityX, rigidBody2D.velocity.y);
            rigidBody2D.velocity = newVelocity;
        }
    }
    private bool CheckIfPlayerInRadius(){
        Vector3 directionFromItemToPlayer = GetDirectionVectorFromItemToPlayer();
            //set the direction in to 1,1,1
        directionFromItemToPlayer = new Vector2(Mathf.Abs(directionFromItemToPlayer.x),Mathf.Abs(directionFromItemToPlayer.y));
        float playerToItemDistance = directionFromItemToPlayer.magnitude - playerTrans.localScale.magnitude;

        return (playerToItemDistance <= radius);
    }
    private Vector3 GetDirectionVectorFromItemToPlayer(){
        return playerTrans.position - transform.position;
    }
    private bool CheckIfPlayerIsNotFull(){
        //this method will check if the player is not full (and still have some things to use)
            //first check if the player inventory's still have this current block's space, if there is , then return yes
            //else then check for if the player is full
                //check the player's block info
        if(thisSlotItemContainer.blockInfo != null){
            if(PlayerInventoryController.Instance.GetFirstSameItemButNotFullHotBarSlot(thisSlotItemContainer.blockInfo) != null)
                return true;
            else if(PlayerInventoryController.Instance.GetFirstSameItemButNotFullInventorySlot(thisSlotItemContainer.blockInfo) != null)
                return true;
                //else
            return false;
        }
                //check the player's item info
        else if(thisSlotItemContainer.itemInfo != null){
            if(PlayerInventoryController.Instance.GetFirstSameItemButNotFullHotBarSlot(thisSlotItemContainer.itemInfo) != null)
                return true;
            else if(PlayerInventoryController.Instance.GetFirstSameItemButNotFullInventorySlot(thisSlotItemContainer.itemInfo) != null)
                return true;
                //else
            return false;
        }
            //if this slot info is nope, then say good bye
        else{
            return false;
        }
    }
    private void PerformMoveToPlayer(){
        //this method will move the item to the player in a time
        Vector2 dirFromItemToPlayer = GetDirectionVectorFromItemToPlayer();
        Vector3 newDirFromItemToPlayer = new Vector2(
            //set the dir from item to player to normal
            (dirFromItemToPlayer.x > 0) ? 1 : ((dirFromItemToPlayer.x == 0) ? 0 : -1),
            (dirFromItemToPlayer.y > 0) ? 1 : ((dirFromItemToPlayer.y == 0) ? 0 : -1)
        );
        rigidBody2D.AddForce(newDirFromItemToPlayer * Time.deltaTime * itemMoveSpeed, ForceMode2D.Impulse);
        //transform.position = Vector2.Lerp(transform.position, playerTrans.position,CXMathFunctions.Map(flyToSpeed,0f,100f,0f,1f));
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
