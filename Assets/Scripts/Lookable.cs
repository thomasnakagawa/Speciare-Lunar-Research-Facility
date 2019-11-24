using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookable : Interactive
{
	[SerializeField] private string[] Observations = default;

    public override void Interact()
	{
		base.Interact();
	}
}
