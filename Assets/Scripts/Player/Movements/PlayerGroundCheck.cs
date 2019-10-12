using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [Header("Basic Infos")]
    public Transform groundCheckTrans;
    public Vector2 size = new Vector2(1f, 0.1f);
    public bool isOnGround = false;
    private void Update(){
        PerformCheckGround();
    }
    private void PerformCheckGround(){
        //this method will check if the ground is a block
        isOnGround = false;
        Collider2D[] collider2Ds;
        collider2Ds = Physics2D.OverlapBoxAll(groundCheckTrans.position,size,0f);
        foreach (var collider2D in collider2Ds)
        {
            if(collider2D.CompareTag("Block")){
                isOnGround = true;
                return;
            }
        }
    }
    private void OnDrawGizmosSelected() {
        //this method will draw a red jumping box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckTrans.position,size);
    }
}
