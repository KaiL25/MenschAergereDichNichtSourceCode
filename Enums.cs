using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour {

	public enum Color
    {
        RED = 0,
        BLUE = 1,
        GREEN = 2,
        YELLOW = 3,
        NEUTRAL = 4
    }

    //The points for each color where they first appear on the board
    public enum Startingpoints
    {
        REDSTARTINGFIELD = 0,
        BLUESTARTINGFIELD = 10,
        GREENSTARTINGFIELD = 20,
        YELLOWSTARTINGFIELD = 30
    }

    //The last field for each color before they enter their destinations
    public enum Endpoints
    {
        REDENDFIELD = 39,
        BLUEENDFIELD = 9,
        GREENENDFIELD = 19,
        YELLOWENDFIELD = 29
    }
}
