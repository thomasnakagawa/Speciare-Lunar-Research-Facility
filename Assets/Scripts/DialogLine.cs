using System;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public string Content;
    public string Speaker = "{player}";
    public AudioClip Sound;
    public SpecialLines SpecialLine = DialogLine.SpecialLines.NONE;

    public enum SpecialLines
    {
        NONE,
        NAME_BOX
    }

    public DialogLine(string Speaker, string Content, SpecialLines specialLine = SpecialLines.NONE)
    {
        this.Speaker = Speaker;
        this.Content = Content;
        this.SpecialLine = specialLine;
    }
}
