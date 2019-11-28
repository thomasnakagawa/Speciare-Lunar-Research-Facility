using System.Collections;
using System.Collections.Generic;
using OatsUtil;
using UnityEngine;

public class MainGameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        yield return new WaitForSeconds(1f);

        SceneUtils.FindComponentInScene<DialogBox>().ShowDialog(new DialogLine[]
        {
                    new DialogLine("{player}", "I can't believe I crashed the ship"),
                    new DialogLine("{player}", "I need to call earth to have them send someone to come get me"),
                    new DialogLine("{player}", "The ship is destroyed, along with the communication devices in it"),
                    new DialogLine("{player}", "And the lab's satellite doesn't look too good"),
                    new DialogLine("{player}", "I should see if I can fix it")
        });
    }
}
