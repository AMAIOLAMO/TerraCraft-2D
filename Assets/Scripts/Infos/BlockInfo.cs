using UnityEngine;
[CreateAssetMenu(menuName = "BlockInfo", fileName = "New_BlockInfo")]
public class BlockInfo : ScriptableObject
{
    [Header("Basic Info")]
    public new string name;
    public GameObject blockPrefab;
    public Sprite sprite;
    [Header("Sounds")]
    public SoundInfo[] blockBreakSoundInfo;
    public SoundInfo[] blockBeenWalkedSoundInfo;
    [Header("WithPlayer")]
    public int neededToolStrength;
    public int explodeProtectionStrength;
    [Header("Interactions")]
    public bool isCurrentlyInteracting;
}
