using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_TerrainInfo", menuName = "TerrainInfo")]
public class TerrainInfo : ScriptableObject {
    [Header("Basic Infos")]
    public new string name;
    public int terrainID;
    [Header("Terrain Layer Settings")]
    public List<BlockInfo> blockInfoTerrainLayers;
    public List<float> terrainDepth;

}