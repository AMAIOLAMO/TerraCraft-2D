using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJMoveWithOBJ : MonoBehaviour
{
    [Header("Put this script in the object need to move")]
    public Transform moveToOBJtransform;

    public bool isMoving = true;
    public bool hasOffSet = true;

    public bool hasLerp = true;
    [Header("this uses %")]
    [Range(1f,100f)]
    public float MovingSpeed = 1f;

    [HideInInspector]
    public Vector3 offSet = Vector3.zero;

    private void Start() {
        //initialize variables
            //get the offset (using offset = enddirPos - startdirPos)
        offSet = transform.position - moveToOBJtransform.position;
    }
    private void FixedUpdate() {
        if(isMoving){
            PerformMoveToOBJ();
        }
    }
    public void PerformMoveToOBJ(){
        //this method performs moves to the object using vector3.lerp
        Vector3 newPos = moveToOBJtransform.position;
            //check the has off set (if has then add)
        if(hasOffSet)
        newPos += offSet;
            //check if there is lerp
        if(hasLerp){
            transform.position = Vector3.Lerp(transform.position,newPos,1 / (100f - MovingSpeed));
            return ;
        }
            //if up no then Do (down)
            //then just set it
        transform.position = newPos;
    }
}
