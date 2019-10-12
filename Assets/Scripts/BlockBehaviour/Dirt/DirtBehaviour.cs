using UnityEngine;

public class DirtBehaviour : MonoBehaviour
{
    public BlockInfo grassBlockInfo;
    public float turnToGrassTime_max = 60f;
    public float turnToGrassTime_min = 15f;
    private float currentGrassTime;
    private void Start()
    {
        currentGrassTime = Random.Range(turnToGrassTime_min, turnToGrassTime_max);
    }
    private void Update()
    {
        PerformTurnToGrassBehaviour();
    }
    private void PerformTurnToGrassBehaviour()
    {
        Vector2 thisBlockUpGamePos = BlockGridController.Instance.FindGamePosWithSceneVector(transform.position + new Vector3(0, 1f));
        BlockInfoContainer gottedBlockAtUp = BlockGridController.Instance.GetBlockAtGamePos(thisBlockUpGamePos);
        //check if this block up there is alive
        if (gottedBlockAtUp.blockInfo == null)
        {
            //if time runs out just change this current block
            if (currentGrassTime <= 0)
            {
                Vector2 currentBlockGamePos = BlockGridController.Instance.FindGamePosWithSceneVector(transform.position);
                BlockGridController.Instance.SetBlock(currentBlockGamePos, grassBlockInfo);
            }
            else{
                currentGrassTime -= Time.deltaTime;
            }
        }
    }
}
