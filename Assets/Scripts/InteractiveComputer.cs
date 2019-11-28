using UnityEngine;
using System.Collections;
using OatsUtil;

public class InteractiveComputer : Interactive
{
    public override void Interact()
    {
        base.Interact();
        var adventureState = SceneUtils.FindComponentInScene<AdventureState>();
        if (adventureState.SatelliteWorks)
        {
            FindObjectOfType<DialogBox>().ShowDialog(new DialogLine[] {
                new DialogLine("Computer", "Connecting..."),
                new DialogLine("Computer", "Error. No satellite connection"),
                new DialogLine("{player}", "I guess I have to fix the satellite before I can use this"),
            });
        }
        else
        {
            if (adventureState.EarthHasBeenContacted == false)
            {
                adventureState.EarthHasBeenContacted = true;
                FindObjectOfType<DialogBox>().ShowDialog(new DialogLine[] {
                    new DialogLine("Computer", "Connecting..."),
                    new DialogLine("Computer", "Connected. Video call established"),
                    new DialogLine("{player}", "Hello!? Can anyone hear me?"),
                    new DialogLine("Earthling", "Is this {player}?! Are you okay?"),
                    new DialogLine("{player}", "Hey! I crashed the ship, but I'm alright. Send someone to come get me!"),
                    new DialogLine("Earthling", "Are you in the moon lab? Have you found the cures to all the diseases?"),
                    new DialogLine("{player}", "Yes, I'm in the lab. There's barely anything in here, the researchers must have taken everything with them when they left"),
                    new DialogLine("Earthling", "No, the cures must be there. They're not here on earth. Aren't they written down in a book up there?"),
                    new DialogLine("{player}", "The bookshelves are empty"),
                    new DialogLine("Earthling", "I swear there must be a \"Book of Cures\" there somewhere. We didn't send you to the moon for nothing"),
                    new DialogLine("{player}", "There's nothing here. I'm stranded, send help"),
                    new DialogLine("Earthling", "You have to find that \"Book of Cures\"")
                });
            }
            else
            {
                FindObjectOfType<DialogBox>().ShowDialog(new DialogLine[] {
                    new DialogLine("Earthling", "Keep looking for the Book Of Cures. We're not sending anyone to come get you until you've found it")
                });
            }
        }
    }

    public override bool UseItemOn(string item)
    {
        var adventureState = SceneUtils.FindComponentInScene<AdventureState>();

        if (adventureState.EarthHasBeenContacted)
        {
            if (item.ToLower().Contains("book of cures"))
            {
                // end of game dialog
                //FindObjectOfType<DialogBox>().ShowDialog(CorrectUseLines);
            }
            else
            {
                // wrong item
                FindObjectOfType<DialogBox>().ShowDialog(new DialogLine[] {
                    new DialogLine("Earthling", "That doesn't look like a cure. Go find the Book of Cures")
                });
            }
        }
        else
        {

            FindObjectOfType<DialogBox>().ShowDialog(new DialogLine[] {
                    new DialogLine("{player}", "")
            });
        }

        return true;
    }
}
