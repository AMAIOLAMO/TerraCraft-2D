using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class TerrainGenerator : MonoBehaviour
{
    //haven't implemented it fully
    public static TerrainGenerator Instance { get; private set; }
    [Header("Pre defined")]
    public List<TerrainInfo> terrainInfos;
    [Header("debug")]
    public bool isDebugging = false;
    public BlockInfo[] baseBlockInfos;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        int seed = Random.Range(0, 99999);
        GenerateTerrain(new Vector2(-100, -10), terrainInfos[0], seed, 200, 5, .4f);
        //Debug.Log("Generated From seed:" + seed);
    }

    //methods
    //haven't finish generate terrain method
    public void GenerateTerrain(Vector2 startPos, TerrainInfo terrainInfo, int Y_Axis_Seed, float generateSize, float freq, float amplitude)
    {
        //this method will generate terrain from the position
        //loop through all the blocks for the terrain and one by one use it to generate down under ground
        //using perlin noise
        for (int x = 0; x < generateSize; x++)
        {
            float currentGeneratedNoise = Mathf.PerlinNoise(x * amplitude, Y_Axis_Seed);
            Vector2Int newPos = new Vector2Int(Mathf.RoundToInt(startPos.x + x), Mathf.RoundToInt(startPos.y + currentGeneratedNoise * freq));
            BlockGridController.Instance.SetBlock(newPos, terrainInfo.blockInfoTerrainLayers[0]);
            //loop in every single base block infos
            foreach (var baseBlockInfo in baseBlockInfos)
            {
                for (int y = 0; y <= baseBlockInfos.Length * 8; y += 8)
                {
                    for (int down = 1; down <= 8; down++)
                    {
                        Vector2Int newDownPos = new Vector2Int(newPos.x,newPos.y - down - y);
                        BlockGridController.Instance.SetBlock(newDownPos, baseBlockInfo);
                    }
                }
            }
        }

        if (isDebugging)
            Debug.Log("Generated Terrain: " + terrainInfo.name + " At pos: " + startPos);
    }
}
