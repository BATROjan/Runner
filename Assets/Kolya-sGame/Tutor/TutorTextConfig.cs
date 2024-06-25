using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorTextConfig", menuName = "Configs/TutorTextConfig")]
public class TutorTextConfig : ScriptableObject
{
    public TutorTextModel[] TutorTextModels => tutorTextModels;
    [SerializeField] private TutorTextModel[] tutorTextModels;
}

[Serializable]
public struct TutorTextModel
{
    public string Text;
    public float Duration;
    public float Delay;
}
