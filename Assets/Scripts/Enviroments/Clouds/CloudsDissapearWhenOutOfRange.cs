using UnityEngine;

public class CloudsDissapearWhenOutOfRange : MonoBehaviour
{
    public float outOfRange_Range = 10f;
    private Transform outOfRangeOBJ;
    public delegate void VoidDelegate();
    public VoidDelegate PlayerTrigger_Destroy;
    private void Start(){
        outOfRangeOBJ = FindObjectOfType<PlayerMovements>().transform;
        PlayerTrigger_Destroy += () =>{
            Destroy(gameObject);
        };
    }
    private void Update(){
        PerformDissapearWhenOutOfRange();
    }
    private void PerformDissapearWhenOutOfRange(){
        //this will check the distance is out of the range
        if(Vector2.Distance(transform.position, outOfRangeOBJ.position) > outOfRange_Range){
            PlayerTrigger_Destroy?.Invoke();
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        if(outOfRangeOBJ != null)
            Gizmos.DrawWireSphere(outOfRangeOBJ.position,outOfRange_Range);
    }
}
