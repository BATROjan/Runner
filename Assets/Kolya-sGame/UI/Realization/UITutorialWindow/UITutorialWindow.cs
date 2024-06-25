using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VGUIService;

public class UITutorialWindow : UIWindow
{
    public Transform[] Step => step;
    public Text Text => text;
    [SerializeField] private Transform[] step;
    [SerializeField] private Text text;
    public override void Show()
    {
        ShowEvent?.Invoke(null,null);
    }

    public override void Hide()
    {
        HideEvent?.Invoke(null,null);
    }
}
