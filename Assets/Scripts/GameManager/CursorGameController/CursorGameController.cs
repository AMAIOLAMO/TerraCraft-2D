using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class CursorGameController : MonoBehaviour
{
    public static CursorGameController Instance { get; private set; }
    [Header("Basic Infos")]
    public Sprite[] cursors;
    public enum cursorState{
        FocusCursor, PointingCursor
    };
    private void Awake()
    {
        Instance = this;
    }
    public void ChangeCursorTextureUsingSprite(Sprite cursorSprite, Vector2 hotSpot, CursorMode cursorMode)
    {
        //this method will use the sprite to change the cursor texture 2D into the cursor img
        Texture2D cursorTexture = cursorSprite.texture;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    public void ChangeCursorUsingDefinedSpritesIndex(int index, Vector2 hotSpot, CursorMode cursorMode)
    {
        ChangeCursorTextureUsingSprite(cursors[index], hotSpot, cursorMode);
    }
}
