using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundOBJ : MonoBehaviour
{
    public Transform RotateAroundTrans;
    public float RotateSpeedPerAngle = 1f;
    
    public bool isRotating = true;

    private void Start(){

    }
    private void Update(){
        if(isRotating){
            PerfromCircularRotation();
        }
    }

    private void PerfromCircularRotation(){
        //this method performs the rotate around thingy
        transform.RotateAround(RotateAroundTrans.position,RotateAroundTrans.forward,RotateSpeedPerAngle);
    }
}
