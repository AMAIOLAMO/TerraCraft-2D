using UnityEngine;

[CreateAssetMenu(fileName = "New_SoundInfo", menuName = "SoundInfo")]
public class SoundInfo : ScriptableObject {
    [Header("Basic Info")]
    public new string name;
    public AudioClip clip;
}