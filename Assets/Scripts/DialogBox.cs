using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OatsUtil;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private float TypeWriteSpeed = 0.05f;
    [SerializeField] private AudioClip TypeSound = default;

    private string PlayerName = "Player";
    private Queue<DialogLine> LinesToShow;

    private Canvas DialogBoxCanvas;
    private TMPro.TMP_Text DialogBoxText;
    private Button NextButton;
    private TMPro.TMP_InputField InputField;

    private Cursor cursor;

    private AudioSource AudioSource;

    public System.Action OnDialogEnded;

    void Start()
    {
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
        LinesToShow = new Queue<DialogLine>();

        InputField = this.RequireDescendantGameObject("NameInputField").RequireComponent<TMPro.TMP_InputField>();
        NextButton = this.RequireDescendantGameObject("NextButton").RequireComponent<Button>();
        DialogBoxText = this.RequireDescendantGameObject("DialogText").RequireComponent<TMPro.TMP_Text>();
        DialogBoxCanvas = this.RequireComponent<Canvas>();

        DialogBoxCanvas.enabled = false;

        cursor = SceneUtils.FindComponentInScene<Cursor>();

        AudioSource = this.RequireComponent<AudioSource>();
    }

    public bool IsShowingDialog => DialogBoxCanvas.enabled;

    public void ShowDialog(DialogLine[] lines)
    {
        cursor.HideText();

        foreach (DialogLine line in lines)
        {
            LinesToShow.Enqueue(line);
        }

        if (DialogBoxCanvas.enabled == false)
        {
            DialogBoxCanvas.enabled = true;
            ShowNextLine();
        }
    }

    private void ShowNextLine()
    {
        StopAllCoroutines();
        if (LinesToShow.Count < 1)
        {
            // no next line, so hide box
            DialogBoxCanvas.enabled = false;

            if (OnDialogEnded != null)
            {
                OnDialogEnded.Invoke();
                OnDialogEnded = null;
            }
        }
        else
        {
            DialogLine nextLine = LinesToShow.Dequeue();

            // show the line
            string speaker = "";
            if (nextLine.Speaker.Equals("{player}"))
            {
                speaker = PlayerName;
            }
            else if (nextLine.Speaker != null)
            {
                speaker = nextLine.Speaker;
            }
            if (speaker.Length > 0)
            {
                speaker = "<b>" + speaker + ":</b> ";
            }

            DialogBoxText.text = speaker;
            StartCoroutine(TypeWrite(nextLine.Content));

            if (nextLine.Sound != null)
            {
                AudioSource.PlayOneShot(nextLine.Sound);
            }

            // special lines
            if(nextLine.SpecialLine == DialogLine.SpecialLines.NONE)
            {
                InputField.gameObject.SetActive(false);
            }
            if (nextLine.SpecialLine == DialogLine.SpecialLines.NAME_BOX)
            {
                InputField.gameObject.SetActive(true);
                NextButton.interactable = false;
                InputField.onValueChanged.AddListener(inputText =>
                {
                    if (validateName(inputText))
                    {
                        PlayerPrefs.SetString("PlayerName", inputText);
                    }
                    NextButton.interactable = validateName(inputText);
                });
            }
        }
    }

    private bool validateName(string inputName)
    {
        return inputName.Contains(" ") == false &&
            inputName.Length > 0 &&
            inputName.Length < 20;
    }

    private IEnumerator TypeWrite(string content)
    {
        if (PlayerPrefs.GetInt("TypewriterDisabled", 0) == 1)
        {
            DialogBoxText.text += content;
            AudioSource.PlayOneShot(TypeSound);
        }
        else
        {
            for (int i = 0; i < content.Length; i++)
            {
                DialogBoxText.text += content[i];

                if (content[i] != ' ' && AudioSource.isPlaying == false)
                {
                    AudioSource.PlayOneShot(TypeSound);
                }
                yield return new WaitForSeconds(TypeWriteSpeed);

            }
        }
    }

    public void OnNextButtonPressed()
    {
        ShowNextLine();
    }
}
