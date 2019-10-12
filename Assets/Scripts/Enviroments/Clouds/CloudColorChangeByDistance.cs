using System.Threading;
using UnityEngine;

public class CloudColorChangeByDistance : MonoBehaviour
{
    private SpriteRenderer cloudSpriteRenderer;
    
    private void Start(){
        float randomBlack = Random.Range(10f,50f);
        cloudSpriteRenderer = GetComponent<SpriteRenderer>();
        Color newColor = new Color(randomBlack,randomBlack,randomBlack,255f);
        cloudSpriteRenderer.color = newColor;
    }
}
