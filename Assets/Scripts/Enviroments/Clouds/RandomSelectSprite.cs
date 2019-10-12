using UnityEngine;

public class RandomSelectSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] cloudSprites;

    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cloudSprites[Random.Range(0,cloudSprites.Length)];
    }
}
