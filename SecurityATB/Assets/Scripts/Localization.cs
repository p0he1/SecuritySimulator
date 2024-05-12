using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class Localization : MonoBehaviour
{
    public bool active = false;
    public TMP_Dropdown langDropdown;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        langDropdown.value = ID;
        ChangeLang(ID);
    }

    public void ChangeLang(int langNum)
    {
        if (active) return;
        StartCoroutine(SetLocale(langNum));
    }


    IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        active = false;
    }
}
