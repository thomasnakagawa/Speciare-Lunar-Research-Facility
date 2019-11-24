using System;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public string Content;
    public string Speaker = "{player}";
    public AudioClip Sound;
}
