using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetPlayerName : MonoBehaviour
{
    [SerializeField] private string playerName;
    void Start()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        Debug.LogWarning("Debug script in scene", this);
    }
}
