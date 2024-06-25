using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;
using VGUIService;

public class UISettingsWindowController
{
    private readonly UISettingsWindow _uiSettingsWindow;
    private readonly IUIService _uiService;
    private readonly SaveDataSystem _saveDataSystem;

    private bool _isChangeLanguage;
    
    public UISettingsWindowController(
        IUIService uiService,
        SaveDataSystem saveDataSystem)
    {
        _uiService = uiService;
        _saveDataSystem = saveDataSystem;
        _uiSettingsWindow = _uiService.Get<UISettingsWindow>();

        _uiSettingsWindow.ShowEvent += ShowWindow;
        _uiSettingsWindow.HideEvent += HideWindow;
    }

    public void CheckLanguage()
    {
        if (_saveDataSystem.LoadLanguage() == 0)
        {
            ChangeLanguageToRus();
        }
        else
        {
            ChangeLanguageToEng();
        }
    }
    
    private void ShowWindow(object sender, EventArgs e)
    {
        _uiSettingsWindow.ButtonRus.OnClickButton += ChangeLanguageToRus;
        _uiSettingsWindow.ButtonEng.OnClickButton += ChangeLanguageToEng;
        _uiSettingsWindow.ButtonReturn.OnClickButton += ReturnMenu;
    }
    
    private void HideWindow(object sender, EventArgs e)
    {
        _uiSettingsWindow.ButtonRus.OnClickButton -= ChangeLanguageToRus;
        _uiSettingsWindow.ButtonEng.OnClickButton -= ChangeLanguageToEng; 
        _uiSettingsWindow.ButtonReturn.OnClickButton -= ReturnMenu;
    }
    
    private void ChangeLanguageToEng()
    {
        if (LocalizationSettings.AvailableLocales.Locales.Count > 0)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
        else
        {
            Debug.LogError("No locales available.");
        }

        _saveDataSystem.SaveLanguage(1);
    }

    private void ChangeLanguageToRus()
    { 
        if (LocalizationSettings.AvailableLocales.Locales.Count > 0)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        else
        {
            Debug.LogError("No locales available.");
        }

        _saveDataSystem.SaveLanguage(0);
    }
    
    private void ReturnMenu()
    {
        _uiService.Hide<UISettingsWindow>();
        _uiService.Show<UIStartWindow>();
    }
}
