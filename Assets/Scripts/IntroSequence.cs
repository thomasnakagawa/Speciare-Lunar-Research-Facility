using OatsUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIntro()
    {
        SceneUtils.FindComponentInScene<DialogBox>().ShowDialog(new DialogLine[]
        {
            new DialogLine("", "It was the year 2070"),
            new DialogLine("", "Humanity's space exploration had flourished"),
            new DialogLine("", "Extraterestrial rocks, plants, and even small creatures from other planets have been brought back to planet earth"),
            new DialogLine("", "Along with them, alien diseases"),
            new DialogLine("", "These diseases caused immense plagues and devastated the earth's population"),
            new DialogLine("", "After several years of the epidemic, medical researchers discovered a cure"),
            new DialogLine("", "This cure requires intense galactic cosmic radiation to manufacture, meaning it can only be made outside of the earth's atmosphere"),
            new DialogLine("", "So, an ambitious team of astronauts, scientists and engineers set up a lab on the moon"),
            new DialogLine("", "In their moon lab, they created a vaccination and cure to all the known diseases from outer space"),
            new DialogLine("", "In the following years, they were able to stop the diseases on earth"),
            new DialogLine("", "With a stockpile of cures and vaccines that would last years, they stopped manufacturing them and decommissioned the moon lab"),
            new DialogLine("", "Now, fourty years later"),
            new DialogLine("", "We've run out, and the diseases have come back"),
            new DialogLine("", "Fortunately, we have since developed the technology to obtain the galactic cosmic radiation necessary using satellites"),
            new DialogLine("", "However, all the procedures for making the cures and vaccines have been lost"),
            new DialogLine("", "We have to return to the moon lab and retreive the recipes, formulas and methods"),
            new DialogLine("", "Our budget's tight this year, so we can only send one person to the moon for this mission"),
            new DialogLine("", "Who should we send?", DialogLine.SpecialLines.NAME_BOX),
            new DialogLine("", "Good choice"),
            new DialogLine("", "We'll send {player} to the moon right away"),
            new DialogLine("", "I hope this goes well")
        });

        SceneUtils.FindComponentInScene<DialogBox>().OnDialogEnded = () =>
        {
            StartCoroutine(EndOfIntroSequence());
        };
    }

    private IEnumerator EndOfIntroSequence()
    {
        SceneUtils.FindObjectInScene("IntroAnimation").transform.RequireComponent<Animator>().SetTrigger("DoAnimation");
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(1);
    }
}
