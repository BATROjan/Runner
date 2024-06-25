using System.Collections;
using System.Collections.Generic;
using Kolya_sGame.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSetting : UIbutton
{
    public Image Image => image;
    [SerializeField] private Image image;
}
