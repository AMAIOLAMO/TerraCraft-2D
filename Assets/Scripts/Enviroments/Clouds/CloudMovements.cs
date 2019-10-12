using UnityEngine;

public class CloudMovements : MonoBehaviour
{
    public float movingDir;
    public float movingSpeed;
    public Vector2 randomScale;
    private void Start(){
            //randomly give moving dir a random dir
        movingDir = (Random.Range(0,2) == 0) ? -1 : 1;
            //randomly give some moving speed
        movingSpeed = Random.Range(1f,2f);
            //randomly give some scale
        randomScale = new Vector2(Random.Range(1f, 3f),Random.Range(1f,2f));
            //set scale
        transform.localScale = randomScale;
    }
    private void Update(){
        PerformMoving();
    }

    private void PerformMoving(){
        //this method performs this cloud to move at the given dir at given speed
        Vector2 newPos = transform.position + Vector3.right * movingDir * movingSpeed * Time.deltaTime;
            //set the pos into new pos
        transform.position = newPos;
    }
    
}
