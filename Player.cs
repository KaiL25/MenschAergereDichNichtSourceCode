using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {

    [SerializeField]
    private GameController m_gameController;
    
    [SerializeField]
    private Figure[] m_playerFigures;

    [SerializeField]
    private Basepoint[] m_playerBase;

    [SerializeField]
    private DestinationPoint[] m_playerDestination;

    [SerializeField]
    private StartingPoint m_playerStartingPoint;

    [SerializeField]
    private Enums.Color _color; 	
    
	private Figure _aFigure;	
	private int _wins;		
	private int _startingPoint;
    private int _endPoint;
    private bool _ai;
    private const int _MAXFIGURES = 4;

    //----------------------------------------------------------------------------//

    // Use this for initialization
    void Start () 
	{
        if(!m_gameController)
        {
            print("Gamecontroller has not been assigned to player.");
        }
        _ai = true;		        		
	}

    //----------------------------------------------------------------------------//

    //CURRENTLY NOT IN USE
    //function for manually animating the figures
    void MoveFigure(int currentFigureToMove, Vector3 newPosition)
    {
        float highPointX;
        float highPointY = 2.0f;
        float slope;
        float moveHeight;        
        highPointX = m_playerFigures[currentFigureToMove].GetComponent<Player>().transform.position.x - newPosition.x;
        slope = newPosition.y - highPointY / Mathf.Pow(newPosition.x - highPointX, 2);
        moveHeight = slope * Mathf.Pow(m_playerFigures[currentFigureToMove].GetComponent<Player>().transform.position.x - highPointX, 2) + highPointY;
        
    }

    //----------------------------------------------------------------------------//

    public void InitiatePlayer()
    {       
        //Set the field for each figure
        //Set the startingpoint index for each figure 
        //Set the player to ai or player        
        for (int i = 0; i < _MAXFIGURES; i++)
        {
           m_playerFigures[i].SetField(i);                               
           m_playerBase[i].SetFigureIndex(i);               
        }
       
        if (_color == Enums.Color.RED)
        {
            _startingPoint = (int)Enums.Startingpoints.REDSTARTINGFIELD;
            _endPoint = (int)Enums.Endpoints.REDENDFIELD;
            _ai = false;
        }     
        
        else if(_color == Enums.Color.BLUE)
        {
            _startingPoint = (int)Enums.Startingpoints.BLUESTARTINGFIELD;
            _endPoint = (int)Enums.Endpoints.BLUEENDFIELD;
        }

        else if (_color == Enums.Color.GREEN)
        {
            _startingPoint = (int)Enums.Startingpoints.GREENSTARTINGFIELD;
            _endPoint = (int)Enums.Endpoints.GREENENDFIELD;
        }

        else if (_color == Enums.Color.YELLOW)
        {
            _startingPoint = (int)Enums.Startingpoints.YELLOWSTARTINGFIELD;
            _endPoint = (int)Enums.Endpoints.YELLOWENDFIELD;
        }

        else
        {
            print("No color has been set for player: " + this.name);
        }
    }
  
    //----------------------------------------------------------------------------//
  
    //Returns whether the player is an npc or not
    public bool GetAI()
    {
        return _ai;
    }
  
    //----------------------------------------------------------------------------//   
  
    //Go through the figures in Box array and check if any are in the box
    //A return of 4 means all figures are in the box, 0 means none are in the box
    public int GetFiguresInBase()
	{
		int counter = 0;
		for (int i = 0; i < _MAXFIGURES; i++) 
		{
			if (m_playerFigures[i].GetInBox() == true || m_playerFigures[i].GetAtDestination()) 
			{
				counter += 1;
			}
		}
		return counter;
	}

    //----------------------------------------------------------------------------//

    //Checks if a specific figure is in base
    public bool GetFigureInBase(int FigureIndex)
	{
		return m_playerFigures [FigureIndex].GetInBox ();
	}

    //----------------------------------------------------------------------------//

    //Sets the value of inBox for the figure whose index was put in
    //and sets it to false
    public void SetFigureOutOfBox(int FigureIndex)
	{
		m_playerFigures[FigureIndex].SetInBox(false);
	}

    //----------------------------------------------------------------------------//

    //Sets the figures location according to the dice thrown
    public void SetFigureLocation(Vector3 NewPosition, int FieldIndex, int FigureIndex)
	{
		if(m_playerFigures[FigureIndex].GetInBox() == false)
		{
            /**
               Idea for giving the figures an "animation"
               MoveFigure(figureIndex, newPosition);
             **/
            m_playerFigures[FigureIndex].transform.position = NewPosition;
			m_playerFigures[FigureIndex].SetField(FieldIndex);
		}		
	}

    //----------------------------------------------------------------------------//

    //Gets the index of the field of the figure index that has been passed in
    public int GetFigureLocationIndex(int FigureIndex)
	{
        int currentField = 0;
		currentField = m_playerFigures[FigureIndex].GetField();  
		return currentField;
	}

    //----------------------------------------------------------------------------//

    //Gets the location of the figure index that has been passed in
    public Vector3 GetFigureLocation(int FigureIndex)
    {
        Vector3 currentLocation;
        currentLocation = m_playerFigures[FigureIndex].transform.position;  
        return currentLocation;
    }

    //----------------------------------------------------------------------------//

    //Sets whether the base point is occupied, or not
    //Sets the figure's index to -1 if it is not occupied
    public void SetBasePoint(bool Status, int Index)
	{
		if(Status == false)
		{
			m_playerBase[Index].SetOccupied(Status);
			m_playerBase[Index].SetFigureIndex(-1);
		}

		else
		{
			m_playerBase[Index].SetOccupied(Status);
			m_playerBase[Index].SetFigureIndex(Index);
		}
	}

    //----------------------------------------------------------------------------//

    //Returns the figure to the base
    public void ReturnFigureToBase(int Index)
	{
		m_playerFigures[Index].transform.position = m_playerBase[Index].transform.position;
		m_playerFigures[Index].SetInBox(true);
		m_playerFigures[Index].SetField(Index);
        m_playerFigures[Index].SetAtDestination(false);
	}

    //----------------------------------------------------------------------------//

    //check if one of the player's figures is on the starting point
    public int CheckIfFigureOnStartPoint()
	{
		int figureIndex = -1;
		for(int i = 0; i < _MAXFIGURES; i++)
		{
			if(m_playerFigures[i].GetField() == _startingPoint)
			{
				figureIndex = i;
				break;
			}
		}
		return figureIndex;
	}

    //----------------------------------------------------------------------------//

    //Returns the player's startingpoint index in the allWayPoints array
    public int GetStartingPoint()
	{
		return _startingPoint;
	}

    //----------------------------------------------------------------------------//

    //Goes through the base array
    //and returns the index of first figure it finds
    //returns -1 if the base is empty
    public int GetNextToGo()
	{
		int index = -1;
		for(int i = 0; i < _MAXFIGURES; i++)
		{
			if(m_playerFigures[i].GetInBox())
			{
				index = i;
				break;
			}
		}
		return index;
	}

    //----------------------------------------------------------------------------//

    //Set the destination point to occupied at the point index given for the figure index given
    public void SetDestinationPointOccupied(int PointIndex, int FigureIndex)
	{
		m_playerDestination[PointIndex].SetFigureIndex(FigureIndex);
		m_playerDestination[PointIndex].SetOccupied(true);
	}

    //----------------------------------------------------------------------------//

    //Returns whether the destination point is occupied or not
    public bool GetDestinationPointInfo(int PointIndex)
	{
		return m_playerDestination[PointIndex].GetOccupied();
	}

    //----------------------------------------------------------------------------//

    //Checks if the destination point at point index is already occupied or not
    public bool CanEnterDestination(int PointIndex)
    {
        bool canEnter = false;
        //If the player destination is NOT occupied set canEnter to true
        if (!m_playerDestination[PointIndex].GetOccupied())
        {
            canEnter = true;
        }
        return canEnter;
    }

    //----------------------------------------------------------------------------//

    //Places the figure at figureindex on the destination Point at point index
    public void PlaceFigureOnDestinationPoint(int PointIndex, int FigureIndex)
	{
       print("Destination point: " + PointIndex);
	   print("Figure at index: " + FigureIndex);
	   m_playerFigures[FigureIndex].transform.position = m_playerDestination[PointIndex].transform.position;
	   m_playerFigures[FigureIndex].SetAtDestination(true);
	   m_playerDestination[PointIndex].SetOccupied(true);
	   m_playerDestination[PointIndex].SetFigureIndex(FigureIndex);       
	}

    //----------------------------------------------------------------------------//

    //Returns the bool at destination
    //Which tells us whether the figure has left the game yet
    public bool GetAtDestination(int FigureIndex)
	{
		return m_playerFigures[FigureIndex].GetAtDestination();
	}

    //----------------------------------------------------------------------------//

    //Finds the first playable figure on the team
    //Figure is either in base, or outside, but not at destination
    public int GetPlayableFigure()
	{
		int playableFigure = -1;
		for(int i = 0; i < _MAXFIGURES; i++)
		{
			if(m_playerFigures[i].GetInBox() || !m_playerFigures[i].GetAtDestination())
			{
				playableFigure = i;
				break;
			}
		}
		return playableFigure;
	}

    //----------------------------------------------------------------------------//

    //Sets the player's figures active so that they respond to mouse enter and exit
    public void SetFiguresActive(int playerColor)
	{
		for(int i = 0; i < _MAXFIGURES; i++)
		{
			if(!m_playerFigures[i].GetInBox() && !m_playerFigures[i].GetAtDestination())
			{
                m_playerFigures[i].SetActive(true);
			}
		}
	}

    //----------------------------------------------------------------------------//

    //Sets the player's figures inactive so that they do not respond to mouse enter and exit
    public void SetFiguresInactive(int playerColor)
	{
		for(int i = 0; i < _MAXFIGURES; i++)
		{
            m_playerFigures[i].SetActive(false);
		}
	}

    //----------------------------------------------------------------------------//

    //Gets the figure that was chosen by being clicked on
    public int GetChosenFigure()
	{
		int chosenFigure = -1;
		for(int i = 0; i < _MAXFIGURES; i++)
		{
			if(m_playerFigures[i].GetMouseOver())
			{
				chosenFigure = i;
			}	
		}
		return chosenFigure;
	}

    //----------------------------------------------------------------------------//

    //Checks if the player has four figures at his destination
    //If yes the player has won the round return true
    public bool GetFiguresAtDestination()
	{
		int count = 0;
		bool winner = false;
		for(int i = 0; i < _MAXFIGURES; i++)
		{
			if(GetAtDestination(i))
			{
				count += 1;
			}
		}
		if(count == 4)
		{
			winner = true;
		}
		print(_color + " player has " + count + "figures at their destination.");
		return winner;
	}

    //----------------------------------------------------------------------------//

    //Logic for choosing a figure if the player is AI
    public int ChooseFigure(int Dice)
	{
        int figureToPlay = -1;
  
         //Check if figure is close to destination
        //and dice throw makes it possible to enter        
        figureToPlay = CheckIfFigureCanEnterDestination(Dice);
        print("Trying to enter destination " + figureToPlay);
       
        if(figureToPlay <= -1)
        {       
              //Check if figure is close to destination
              figureToPlay = CheckIfCloseToDestination();
              print("Close to destination " + figureToPlay);            
  
                if(figureToPlay <= -1)
                {
                    //Check if a figure can be thrown out off the board with the next step
                    figureToPlay = CheckIfFigureCanBeKicked();
                    print("Can Kick A Figure" + figureToPlay);
  
                    if(figureToPlay <= -1)
                    {
                        //Check if a figure does not have to kick its teammate on the next move
                        figureToPlay = CheckIfTeamMemberOnNextPoint();
                        print("Figure that does not kick" + figureToPlay);
  
                        if (figureToPlay <= -1)
                        {
                            //Check if figure is on a starting point
                            figureToPlay = CheckIfFigureOnAnyStartingPoint();
                            print("FigureOnAStartingPoint" + figureToPlay);
  
                            if(figureToPlay <=-1)
                            {
                                 //Select the first figure in the index that is on the board
                                 figureToPlay = GetFiguresOnBoard();
                                 print("Chosen Figure" + figureToPlay);                            
                            }
                        }
                    }
                }
        }          
        return figureToPlay;
	}

    //----------------------------------------------------------------------------//

    //Checks the figures array
    //and returns the first that is on a starting point
    //else returns -1
    private int CheckIfFigureOnAnyStartingPoint()
    {
        int figureOnStartingPoint = -1;
        for(int i = 0; i < _MAXFIGURES; i++)
        {
            int location = GetFigureLocationIndex(i);
            if (location == 0 || location == 10 || location == 20 || location == 30)
            {
                if (!m_playerFigures[i].GetInBox())
                {
                    figureOnStartingPoint = i;
                    break;
                }
            }
        }
        
        return figureOnStartingPoint;
    }

    //----------------------------------------------------------------------------//

    //Checks the figures array
    //returns a figure index if a figure is within 10 fields of the destination 
    private int CheckIfCloseToDestination()
    {
        int figureCloseToDestination = -1;
        for (int i = 0; i < _MAXFIGURES; i++)
        {
            //If the figure is at a point that is greater or equal to the endpoint - 6 and lower or equal to the endpoint's index
            //If the figure is not already at the destination, or still in the base choose it
            if (GetFigureLocationIndex(i) >= _endPoint - 6 && GetFigureLocationIndex(i) <= _endPoint && !m_playerFigures[i].GetAtDestination() && !m_playerFigures[i].GetInBox())
            {
                //Is there already a figure assigned?
                if (figureCloseToDestination != -1)
                {
                    //If yes is the new figure closer to the destination?
                    if (GetFigureLocationIndex(figureCloseToDestination) < GetFigureLocationIndex(i))
                    {
                        figureCloseToDestination = i;
                    }
                }
                //If a figure has not been assigned yet
                else
                {
                    figureCloseToDestination = i;
                }
            }
        }
        return figureCloseToDestination;
    }

    //----------------------------------------------------------------------------//

    //Checks whether the closest figure to the destination can enter
    //returns -1 if no, or the figure's index if yes
    private int CheckIfFigureCanEnterDestination(int Dice)
    {
        int remainingSteps;
        int figureIndex = -1;
        if (CheckIfCloseToDestination() > -1)
        {
            figureIndex = CheckIfCloseToDestination();
            if (GetFigureLocationIndex(figureIndex) + Dice > _endPoint)
            {
                remainingSteps = _endPoint - GetFigureLocationIndex(figureIndex);
                if (remainingSteps <= 4)
                {
                    if (GetDestinationPointInfo(remainingSteps))
                    {
                        figureIndex = -1;
                    }                    
                }
            }
        }
        return figureIndex;
    }

    //----------------------------------------------------------------------------//

    //Checks if any of the figures can kick the figure of another team
    private int CheckIfFigureCanBeKicked()
    { 
        int figureIndex = -1;
  
        for(int i = 0; i < _MAXFIGURES; i++)
        {
            //If the figure is not at the destination point or inside the base
            if (!m_playerFigures[i].GetAtDestination() && !m_playerFigures[i].GetInBox())
            {
                if (m_gameController.CriticalZoneAI(i))
                {
                    figureIndex = i;
                }
            }
        }
        return figureIndex;
    }

    //----------------------------------------------------------------------------//

    private int CheckIfTeamMemberOnNextPoint()
    {
        int figureIndex = -1;
  
        for(int i = 0;i < _MAXFIGURES; i++)
        {
             //If the figure is not at the destination point or inside the base
            if (!m_playerFigures[i].GetAtDestination() && !m_playerFigures[i].GetInBox())
            {
                //If the figure is not going to kick a teammate with its next move return it
                if (!m_gameController.CriticalZoneAITeammates(i))
                {
                    figureIndex = i;
                }
            }
        }
        return figureIndex;
    }

    //----------------------------------------------------------------------------//

    //Returns the first figure in the array 
    //that is not in the box and not at the destination
    //If there are none it returns -1, 
    //but that would also mean the player has already won
    private int GetFiguresOnBoard()
    {
        int figureIndex = -1;
  
        for (int i = 0; i < _MAXFIGURES; i++)
        {
            if (!m_playerFigures[i].GetAtDestination() && !m_playerFigures[i].GetInBox())
            {
                figureIndex = i;
                break;
            }
        }
        return figureIndex;
    }

    //----------------------------------------------------------------------------//
}
