using UnityEngine;

public class ItemFloatWithSineWaveBehaviour : MonoBehaviour
{
    public float amplitude = 2f;
    public float speed = 0.05f;
    public bool isFloating = true;
    private float angle = 0f;

    private void Update(){
        if(isFloating)
            PerformFloatingAnimation();
    }
    private void PerformFloatingAnimation(){
        //this method is for performing the floating animation
        Vector3 newYOffSet = Vector2.up * Mathf.Sin(angle) * amplitude;
        transform.localPosition = newYOffSet;
        angle += speed;
        if(angle >= Mathf.PI){
            angle = 0f;
        }
    }
}
