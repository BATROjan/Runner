using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class SwipeDown : MonoBehaviour
{
    public LeanFingerSwipe LeanFingerSwipe => leanFingerSwipe;
    [SerializeField] private LeanFingerSwipe leanFingerSwipe;
}
