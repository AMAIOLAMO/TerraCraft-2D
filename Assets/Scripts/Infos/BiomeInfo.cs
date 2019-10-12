using UnityEngine;
[CreateAssetMenu(fileName = "New_BiomeInfo", menuName = "BiomeInfo")]
public class BiomeInfo : ScriptableObject
{
    [Header("Basic infos")]
    public string biomeName;
    [Tooltip("The block layer infos determines how this terrain is going to be generated")]
    public BlockLayerInfo[] blockLayerInfos;
    
    [Header("Biome Positions")]
    public float BiomeAppearMaxPos;
    public float BiomeAppearMinPos;

    
}
