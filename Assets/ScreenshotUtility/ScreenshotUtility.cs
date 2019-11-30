using System;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Takes screenshots when a hotkey is pressed or a method is called. Also has hotkeys for changing timeScale and hidding canvases.
/// Made as a dev tool for taking screenshots for store pages, key images, etc and not as a production ready screenshot feature. 
/// </summary>
public class ScreenshotUtility : MonoBehaviour
{
    [Header("File")]
    [SerializeField] private string FileName = "Screencap";
    [Tooltip("Ex: \"screens\" or \"screenshots/keyshots\"")]
    [SerializeField] private string Folder = "Screenshots";

    [Header("Screencap")]
    [SerializeField] private bool ScreenCapKeyEnabled = true;
    [SerializeField] private KeyCode ScreenCaptureKey = KeyCode.F12;

    [Header("TimeScale")]
    [SerializeField] private bool TimeKeysEnabled = true;
    [SerializeField] private KeyCode SlowDownTimeKey = KeyCode.F5;
    [SerializeField] private KeyCode ResetTimeKey = KeyCode.F6;
    [SerializeField] private KeyCode SpeedUpTimeKey = KeyCode.F7;

    [Header("HUD")]
    [SerializeField] private bool HUDKeysEnabled = true;
    [SerializeField] private KeyCode HideCanvasesKey = KeyCode.F4;

    void Start()
    {
        Debug.LogWarning("ScreenshotTool component is present in scene", this);
    }

    void Update()
    {
        if (ScreenCapKeyEnabled)
        {
            if (Input.GetKeyDown(ScreenCaptureKey))
            {
                TakeScreenshot();
            }
        }

        if (TimeKeysEnabled)
        {
            if (Input.GetKeyDown(SlowDownTimeKey))
            {
                Time.timeScale -= 0.1f;
                Debug.Log("Time slowed down: " + Time.timeScale, this);
            }
            if (Input.GetKeyDown(SpeedUpTimeKey))
            {
                Time.timeScale += 0.1f;
                Debug.Log("Time sped up: " + Time.timeScale, this);
            }
            if (Input.GetKeyDown(ResetTimeKey))
            {
                Time.timeScale = 1f;
                Debug.Log("Time reset: " + Time.timeScale, this);
            }
        }

        if (HUDKeysEnabled)
        {
            if (Input.GetKeyDown(HideCanvasesKey))
            {
                FindObjectsOfType<Canvas>().ToList()
                    .ForEach(canvas => canvas.enabled = false);
                Debug.Log("Canvases hidden", this);

            }
        }
    }

    /// <summary>
    /// Takes a png screenshot and saves it in a folder
    /// </summary>
    public void TakeScreenshot()
    {
        if (!Directory.Exists(Folder))
        {
            Directory.CreateDirectory(Folder);
            Debug.Log("Folder created: " + Folder, this);
        }

        string date = DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss");
        string extension = ".png";
        string dimensions = Screen.width + "x" + Screen.height;
        string fullPath = Folder + "/" + FileName + "_" + dimensions + "_" + date + extension;
        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("Screenshot taken: " + fullPath, this);
    }
}
