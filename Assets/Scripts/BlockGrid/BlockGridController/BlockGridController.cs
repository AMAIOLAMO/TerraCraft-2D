using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BlockGridController : MonoBehaviour
{
    public static BlockGridController Instance { get; private set; }
    public GameObject parentToOBJ;
    public List<BlockInfo> blocks;
    public Vector2 GridOffSet = new Vector2(-0.5f, -0.5f);

    [Header("Debug")]
    public bool isDebugging = false;
    private void Awake()
    {
        Instance = this;
    }
    //useful methods
        //find grid pos method~
    public Vector2 FindSceneVectorWithGamePos(Vector2 gamePos)
    {
        Vector2 calculatedFloatSceneVect = new Vector2((float)gamePos.x - GridOffSet.x, (float)(gamePos.y - GridOffSet.y));
        return calculatedFloatSceneVect;
    }
    public Vector2Int FindGamePosWithSceneVector(Vector2 scenePos)
    {
        Vector2Int foundedGamePos = new Vector2Int((int)(scenePos.x + GridOffSet.x), (int)(scenePos.y + GridOffSet.y));
        return foundedGamePos;
    }
    //finding block methods
    public BlockInfo[] FindBlockInfoByName(string name)
    {
        List<BlockInfo> blockInfos = new List<BlockInfo>();
        foreach (var blockInfo in blocks)
        {
            if (blockInfo.name == name)
                blockInfos.Add(blockInfo);
        }
        return blockInfos.ToArray();
    }
    public BlockInfoContainer GetBlockAtGamePos(Vector2 gamePos)
    {
        Vector2 scenePos = FindSceneVectorWithGamePos(gamePos);
        Collider2D collider2D = Physics2D.OverlapCircle(gamePos, 0.01f);

        return collider2D.GetComponent<BlockInfoContainer>();
    }
        //setting blocks methods
    public void SetBlock(Vector2 gamePos, BlockInfo blockInfo)
    {
        //this method will set a block in a setted position
        Vector2 foundedGamePosOnScene = FindSceneVectorWithGamePos(gamePos);
        //first check if this current pos has a block (if yes then destroy it)
        DestroyBlockOnGamePos(gamePos);

        Instantiate(blockInfo.blockPrefab, foundedGamePosOnScene, Quaternion.identity, parentToOBJ.transform);
    }
    public void FillBlocks(Vector2 startGamePos, Vector2 endGamePos, BlockInfo blockInfo)
    {
        //this method will fill the blocks with the given poses
        Vector2 diffGameVec2 = new Vector2(
            endGamePos.x - startGamePos.x,
            endGamePos.y - startGamePos.y
        );
        Vector2 turnMagnitudeTO1OfDiff = new Vector2(
            diffGameVec2.x == 0 ? 0 : 1,
            diffGameVec2.y == 0 ? 0 : 1
        );
        for (int y = 0; y < Mathf.Abs(diffGameVec2.y); y++)
        {
            for (int x = 0; x < Mathf.Abs(diffGameVec2.x); x++)
            {
                Vector2 newGamePos = new Vector2(
                    startGamePos.x + turnMagnitudeTO1OfDiff.x * x,
                    startGamePos.y + turnMagnitudeTO1OfDiff.y * y
                );
                SetBlock(newGamePos, blockInfo);
            }
        }
    }
        //managing block methods
    public bool DestroyBlockOnGamePos(Vector2 gamePos)
    {
        //this method will check if this current pos has a gameOBJ with a tag of block and destroy this block
        Collider2D[] colliders = Physics2D.OverlapCircleAll(FindSceneVectorWithGamePos(gamePos), 0.01f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Block"))
                Destroy(collider.gameObject);
            return true;
        }
        return false;
    }

}
