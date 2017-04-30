using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

    //Used to determine if a figure is on the point and 
    //also to determine the destination- and basepoint color.
    [SerializeField]
    protected Enums.Color _pointColor;
    	
    //-1 signifies no figure
	protected int _figureIndex;
    protected bool _occupied;
    //----------------------------------------------------------------------------//

    public void SetPointColor(Enums.Color Color)
    {
        //We never want to change the color of a base, or destination point
        //They are assigned manually in the editor.
        if (this is Waypoint || this is StartingPoint)
        {
            _pointColor = Color;
        }
    }

    //----------------------------------------------------------------------------//

    public Enums.Color GetPointColor()
	{
		return _pointColor;
	}

    //----------------------------------------------------------------------------//
    
    public int GetFigureIndex()
	{
		return _figureIndex;
	}

    //----------------------------------------------------------------------------//

    public void SetFigureIndex(int Index)
	{
		_figureIndex = Index;
	}

    //----------------------------------------------------------------------------//

    public bool GetOccupied()
    {
        return _occupied;
    }

    //----------------------------------------------------------------------------//

    public void SetOccupied(bool NewStatus)
    {
        _occupied = NewStatus;
    }
}
