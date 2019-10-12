using UnityEngine;
[CreateAssetMenu(fileName = "New_BlockLayerInfo", menuName = "BlockLayerInfo")]
public class BlockLayerInfo : ScriptableObject
{
    [Header("Basic infos")]
    //this info is for storing the block layers and storing the layer infos
    [Tooltip("This determines which block that this layer uses")]
    public BlockInfo blockInfo;
    [Tooltip("This determines how deep this block layer is(Min)")]
    public int Deepness_Min;
    [Tooltip("This determines how deep this block layer is(Max)")]
    public int Deepness_Max;
}
