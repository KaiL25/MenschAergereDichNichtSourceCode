using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : Point
{
    void Awake()
    {
        _pointColor = Enums.Color.NEUTRAL;        
        _figureIndex = -1;
    }
}
