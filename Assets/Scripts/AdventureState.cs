using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureState : MonoBehaviour
{
    public bool SatelliteWorks = false;
    public bool EarthHasBeenContacted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnSatelliteFix()
    {
        SatelliteWorks = true;
    }
}
