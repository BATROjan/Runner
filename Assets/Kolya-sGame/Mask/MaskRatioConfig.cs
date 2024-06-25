using UnityEngine;

[CreateAssetMenu(fileName = "MaskRatioConfig", menuName = "Configs/MaskRatioConfig")]
public class MaskRatioConfig : ScriptableObject
{
    public float DurationForChangeRealityToVr => durationForChangeRealityToVr;
    public float DurationForChangeVrToReality => durationForChangeVrToReality;
    [SerializeField] private float durationForChangeRealityToVr;
    [SerializeField] private float durationForChangeVrToReality;
}
