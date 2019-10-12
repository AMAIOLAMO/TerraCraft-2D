using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocusPointController : MonoBehaviour
{
    public Transform focusPointTrans;
    public float maxFocusingRadius = 1f;
    private Camera mainCam;
    private void Start(){
        mainCam = Camera.main;
    }
    private void Update(){
        if(Input.GetMouseButton(0)){
            //if is focusing 
            PerformMouseFocus();
        }
        else{
            PerformTransformBackToPlayer();
        }
    }
    private void PerformMouseFocus(){
        //this method will perform the mouse to focus onto the middle bounds of the player and the pos
        Vector3 diffOfPos = GetMouseOnScenePos() - transform.position;
            //divide it into 2 (to get the center of the bounds)
        diffOfPos /= 2;
            //clamp the pos inside or else it will be to big (and the player may be out of sight)
        float ClampedX = Mathf.Clamp(diffOfPos.x,-maxFocusingRadius,maxFocusingRadius);
        float ClampedY = Mathf.Clamp(diffOfPos.y,-maxFocusingRadius,maxFocusingRadius);

        diffOfPos = new Vector3(ClampedX,ClampedY);
            //at last, add the off set on it
        Vector2 newPos = transform.position + diffOfPos;

        focusPointTrans.position = newPos;
    }
    private void PerformTransformBackToPlayer(){
        //this method will perform transform the focus point into the pos of the player
        focusPointTrans.position = transform.position;
    }
    private Vector3 GetMouseOnScenePos(){
        //this method will get the mouse pos on the scene
        return mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
