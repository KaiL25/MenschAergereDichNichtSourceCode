using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : Point
{
    // Use this for initialization
	void Awake ()
    {
        _pointColor = Enums.Color.NEUTRAL;        
        _figureIndex = -1;
	}	
}
