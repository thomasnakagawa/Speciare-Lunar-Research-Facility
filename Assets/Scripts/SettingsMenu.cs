using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OatsUtil;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private void OnEnable()
    {
        this.RequireDescendantGameObject("TypeWriterToggle").RequireComponent<Toggle>().isOn = PlayerPrefs.GetInt("TypewriterDisabled") == 0;
    }

    public void OnTypeWriterToggle(bool value)
    {
        PlayerPrefs.SetInt("TypewriterDisabled", value ? 0 : 1);
    }
}
