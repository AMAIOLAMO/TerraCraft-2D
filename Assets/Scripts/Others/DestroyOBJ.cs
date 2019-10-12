using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOBJ : MonoBehaviour
{
    [Header("Basic Infos")]
    public bool willDestroyInStart = true;
    public float destroyTime = 0f;
    
    private void Start(){
        if(willDestroyInStart){
            Destroy(gameObject,destroyTime);
        }
    }
    public void StartDestory(){
        Destroy(gameObject,destroyTime);
    }
}
