using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //All the points from base- to way- to starting points are in this array.
    [SerializeField]
    private Point[] m_allWayPoints;

    [SerializeField]
    private Player[] m_players;

    [SerializeField]
    private UI m_ui;

    [SerializeField]
    private Arrow m_arrow;

    public const int _PLAYERS = 4;
	public const int _FIGURES = 4;	    
	private Enums.Color _currentPlayerTurn;
	private int _dice;
	private int _click;	   
    
    
	//Will be set to according to the outcome of the if statements for the figures on the board
	// is used to make the figure selection work
	private bool _canChoose = false;
	//Gets set to true in play whenever a starting figure leaves the base and the next turn is initiated
	//Is used to make this figure the tempFigure for the following turn.
	private bool _figureOnStartingPoint = false;


	//The index of the figure being played
	private int _tempFigureIndex;

    //private bool gameplay;

    //----------------------------------------------------------------------------//

    // Use this for initialization
    void Start () 
	{
		//gameplay = true;
        
        m_arrow.GetComponent<Arrow>().SetArrowVisibility(false);			
		_currentPlayerTurn = Enums.Color.RED;		
		_click = 1;		
		_tempFigureIndex = 0;

        //Fill the array with all waypoints and startingpoints
        // index 0 == RedStartingPoint
        // index 10 == BlueStartingPoint
        // index 20 == GreenStartingPoint
        // index 30 == YellowStartingPoint
        for(int i = 0;i < _PLAYERS; i++)
        {
            m_players[i].InitiatePlayer();
        }
        m_ui.OutputCurrentPlayerTurn(_currentPlayerTurn);
        m_ui.OutputInfoText("Please roll your dice");
	}

    //----------------------------------------------------------------------------//

    public void DeterminePlayerTurn()
    {
        //First Click Red Player
        if (_currentPlayerTurn == Enums.Color.RED && _click == 1)
        {
            ClickOne();
        }
 
        //Second Click Red Player
        else if (_currentPlayerTurn == Enums.Color.RED && _click == 2)
        {
            ClickTwo();
        }
 
        //If Blue Player is ai and first click
        else if (_currentPlayerTurn == Enums.Color.BLUE && m_players[(int)_currentPlayerTurn].GetAI() && _click == 1)
        {
            AiTurn1();
        }
 
        //If Blue Player is ai and second click
        else if (_currentPlayerTurn == Enums.Color.BLUE && m_players[(int)_currentPlayerTurn].GetAI() && _click == 2)
        {
 
            AiTurn2();
        }
 
        //First Click Blue Player
        else if (_currentPlayerTurn == Enums.Color.BLUE && _click == 1 && !m_players[(int)_currentPlayerTurn].GetAI())
        {
            ClickOne();
        }
 
        //Second Click Blue Player
        else if (_currentPlayerTurn == Enums.Color.BLUE && _click == 2 && !m_players[(int)_currentPlayerTurn].GetAI())
        {
            ClickTwo();
        }
 
        //If Green Player is ai and first click
        else if (_currentPlayerTurn == Enums.Color.GREEN && m_players[(int)_currentPlayerTurn].GetAI() && _click == 1)
        {
            AiTurn1();
        }
 
        //If Green Player is ai and second click
        else if (_currentPlayerTurn == Enums.Color.GREEN && m_players[(int)_currentPlayerTurn].GetAI() && _click == 2)
        {
            AiTurn2();
        }
 
        //First Click Green Player
        else if (_currentPlayerTurn == Enums.Color.GREEN && _click == 1 && !m_players[(int)_currentPlayerTurn].GetAI())
        {
            ClickOne();
        }
 
        //Second Click Green Player
        else if (_currentPlayerTurn == Enums.Color.GREEN && _click == 2 && !m_players[(int)_currentPlayerTurn].GetAI())
        {
            ClickTwo();
        }
 
        //If Yellow Player is ai and first click
        else if (_currentPlayerTurn == Enums.Color.YELLOW && m_players[(int)_currentPlayerTurn].GetAI() && _click == 1)
        {
            AiTurn1();
        }
 
         //If Yellow Player is ai and second click
        else if (_currentPlayerTurn == Enums.Color.YELLOW && m_players[(int)_currentPlayerTurn].GetAI() && _click == 2)
        {
            AiTurn2();
        }
 
        //First Click Yellow Player
        else if (_currentPlayerTurn == Enums.Color.YELLOW && _click == 1 && !m_players[(int)_currentPlayerTurn].GetAI())
        {
            ClickOne();
        }
 
        //Second Click Yellow Player
        else if (_currentPlayerTurn == Enums.Color.YELLOW && _click == 2 && !m_players[(int)_currentPlayerTurn].GetAI())
        {
            ClickTwo();
        }
 
        //Third Click anyone
        else if (_click == 3)
        {
            ClickThree();
        }
    }

    //----------------------------------------------------------------------------//

    //Variable that imitates the dice throw
    private int DiceThrow(int Dice)
	{
		return Dice = Random.Range (1, 7);
	}

    //----------------------------------------------------------------------------//

    /*
    Rolls the dice, tells the player the rolled number
    Then checks what step needs to follow, does the player need to be skipped,
    Can the player play a figure etc.
     */
    public void ClickOne()
	{
		_dice = DiceThrow (_dice);
		print((int)_currentPlayerTurn + " Roll " + _dice);        
 
        //If this is true, the player must have put a new figure on the field
        //and therefore now has to play this figure again to remove it from the starting field
		if(_figureOnStartingPoint == true)
		{
            m_ui.OutputInfoText("You rolled a " + _dice + "\nFigure on starting point gets played");
			_figureOnStartingPoint = false;
			_click = 2;
		}
 
		//When the player rolls six and the player's base is empty
		//the player can choose the figure to play
		else if(_dice == 6 && m_players[(int)_currentPlayerTurn].GetNextToGo() == -1)
		{
            m_ui.OutputInfoText("You rolled a " + _dice + "\nPlease choose your figure");
			m_players[(int)_currentPlayerTurn].SetFiguresActive((int)_currentPlayerTurn);
			_canChoose = true;
			_click = 2;
		}

		//When the player rolls six and the base is not empty
		else if(_dice == 6 && m_players[(int)_currentPlayerTurn].GetNextToGo() > -1)
		{
            m_ui.OutputInfoText("You rolled a " + _dice + "\nA new figure enters the board");
			_tempFigureIndex = m_players[(int)_currentPlayerTurn].GetNextToGo();
			_click = 2;
		}

		//When the player rolls lower six and there is at least one figure on the field
		else if(_dice < 6 && m_players[(int)_currentPlayerTurn].GetFiguresInBase() < 4)
		{
            m_ui.OutputInfoText("You rolled a " + _dice + "\nPlease choose your figure");
			m_players[(int)_currentPlayerTurn].SetFiguresActive((int)_currentPlayerTurn);
			_canChoose = true;
			_click = 2;
		}
			
		//When the player rolls lower than six and no figures are on the board
		else 
		{
            m_ui.OutputInfoText("You rolled a " + _dice + "\nYou have to skip this round");
			_click = 3;
		}		       
	}

    //----------------------------------------------------------------------------//

    //The figure gets played
    //If the figure was chosen by the player it gets set here
    //else the figure that was determined gets played
    void ClickTwo()
	{
		if(_canChoose)
		{
			_tempFigureIndex = m_players[(int)_currentPlayerTurn].GetChosenFigure();
		}
		Play ();
		_canChoose = false;
       if (_click != 3)
       {
           _click = 1;
       }
	}

    //----------------------------------------------------------------------------//

    void ClickThree()
    {
        if (m_arrow.GetArrowVisibility())
        {
            m_arrow.SetArrowVisibility(false);
        }
        _click = 1;
        NextPlayerTurn();
    }

    //----------------------------------------------------------------------------//

    /*
   Rolls the dice, tells the player the rolled number
   Then checks what step needs to follow, does the AI need to be skipped,
   Can the AI play a figure etc.
    */
    private void AiTurn1()
    {
        _dice = DiceThrow(_dice);
        print("AI Player " + ((int)_currentPlayerTurn) + " " + _dice);
        m_ui.OutputInfoText("AI rolled " + _dice + "\nChoosing figure");
        //If this is true, the AI must have put a new figure on the field
        //and therefore now has to play this figure again to remove it from the starting field
        if (_figureOnStartingPoint == true)
        {
            _figureOnStartingPoint = false;
            m_arrow.SetArrowVisibility(true);
            m_arrow.SetArrowLocation(m_players[(int)_currentPlayerTurn].GetFigureLocation(_tempFigureIndex));
            _click = 2;
        }
 
        //When the AI rolls six and its base is empty
        //the AI can choose the figure to play
        else if (_dice == 6 && m_players[(int)_currentPlayerTurn].GetNextToGo() == -1)
        {
            Debug.Log("A");
            _tempFigureIndex = m_players[(int)_currentPlayerTurn].ChooseFigure(_dice);
            m_arrow.SetArrowVisibility(true);
            m_arrow.SetArrowLocation(m_players[(int)_currentPlayerTurn].GetFigureLocation(_tempFigureIndex));
 
            _click = 2;
        }
 
        //When the AI rolls six and the base is not empty
        else if (_dice == 6 && m_players[(int)_currentPlayerTurn].GetNextToGo() > -1)
        {
            Debug.Log("B");
            m_ui.OutputInfoText("AI rolled " + _dice + "\nNew figure enters the board");
            _tempFigureIndex = m_players[(int)_currentPlayerTurn].GetNextToGo();
            m_arrow.SetArrowVisibility(true);
            m_arrow.SetArrowLocation(m_players[(int)_currentPlayerTurn].GetFigureLocation(_tempFigureIndex));
            _click = 2;
        }
 
        //When the AI rolls lower six and there is at least one figure on the field
        else if (_dice < 6 && m_players[(int)_currentPlayerTurn].GetFiguresInBase() < 4)
        {
            Debug.Log("C");
            
            _tempFigureIndex = m_players[(int)_currentPlayerTurn].ChooseFigure(_dice);
            m_arrow.SetArrowVisibility(true);
            m_arrow.SetArrowLocation(m_players[(int)_currentPlayerTurn].GetFigureLocation(_tempFigureIndex));
 
            _click = 2;
        }
 
        //When the AI rolls lower than six and no figures are on the board
        else
        {
            m_ui.OutputInfoText("AI rolled " + _dice + "\nHas to skip this round");
            _click = 3;
        }
    }

    //----------------------------------------------------------------------------//

    private void AiTurn2()
    {
        Debug.Log("D");
        print("ChosenTempFigIn " + _tempFigureIndex);
        Play();
        if (_click != 3)
        {
            _click = 1;
        }
    }

    //----------------------------------------------------------------------------//

    //Handling the current player's turn
    private void Play ()
	{		
		//If the figure being played is outside of the base
		if(!m_players[(int)_currentPlayerTurn].GetFigureInBase(_tempFigureIndex))
		{
			//Play figure and set it inactive afterwards
           //If the player is AI only play the figure skip setting active and inactive
			//Inactive means the player cannot select it
			EnterDestination(_tempFigureIndex, _dice);
           
           if (!m_players[(int)_currentPlayerTurn].GetAI())
           {
               m_players[(int)_currentPlayerTurn].SetFiguresInactive((int)_currentPlayerTurn);
           }

           //WINNING CONDITION OF THE GAME
           //If getFiguresAtDestination() returns true the game ends
			if(m_players[(int)_currentPlayerTurn].GetFiguresAtDestination())
			{
               m_arrow.SetArrowVisibility(false);
               m_ui.SetPlaying(false);
               m_ui.ShowGameEndScreen((int)_currentPlayerTurn);
               ResetGame();
               
			}

           //If the roll is not six go to the next player
			else if(_dice != 6)
			{
               m_ui.OutputInfoText("Click to Continue");
               _click = 3;
			}

			else 
			{
               if (m_players[(int)_currentPlayerTurn].GetAI())
               {
                   m_ui.OutputInfoText("AI Player " + (int)_currentPlayerTurn + " got another turn");
               }
               else
               {
                   m_ui.OutputInfoText("You got another turn");
               }
			}
          
		}

		//The figure being played is inside the base
		else
		{
			m_players[(int)_currentPlayerTurn].SetFigureOutOfBox(_tempFigureIndex);
			m_players[(int)_currentPlayerTurn].SetBasePoint(false, _tempFigureIndex);
			NextFieldCheckAndSet(m_players[(int)_currentPlayerTurn].GetStartingPoint());
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[m_players[(int)_currentPlayerTurn].GetStartingPoint()].transform.position, m_players[(int)_currentPlayerTurn].GetStartingPoint(), _tempFigureIndex);
           if (m_players[(int)_currentPlayerTurn].GetAI())
           {
               m_ui.OutputInfoText("AI Player " + _currentPlayerTurn + " got another turn");
           }
           else
           {
               m_ui.OutputInfoText("You got another turn");
           }
          	_figureOnStartingPoint = true;
		}
       m_arrow.SetArrowLocation(m_players[(int)_currentPlayerTurn].GetFigureLocation(_tempFigureIndex));

	}

    //----------------------------------------------------------------------------//

    //Switching the turn to the next player and setting the text and color
    private int NextPlayerTurn()
	{
		m_ui.OutputInfoText("Please roll your dice");
		if (_currentPlayerTurn == Enums.Color.RED) 
		{
			_currentPlayerTurn = Enums.Color.BLUE;
           m_ui.OutputCurrentPlayerTurn(_currentPlayerTurn);
		} 

		else if (_currentPlayerTurn == Enums.Color.BLUE) 
		{
			_currentPlayerTurn = Enums.Color.GREEN;
           m_ui.OutputCurrentPlayerTurn(_currentPlayerTurn);
		}

		else if (_currentPlayerTurn == Enums.Color.GREEN) 
		{
			_currentPlayerTurn = Enums.Color.YELLOW;
           m_ui.OutputCurrentPlayerTurn(_currentPlayerTurn);
		}
		else 
		{
			_currentPlayerTurn = Enums.Color.RED;
           m_ui.OutputCurrentPlayerTurn(_currentPlayerTurn);
		}
		return (int)_currentPlayerTurn;
	}

    //----------------------------------------------------------------------------//

    //When the figure reaches the end of the array waypoints set the position to the beginning array according to the dice
    void CriticalZone()
	{
		//Index 34 in waypoint array
       if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 34 && _dice == 6)
		{
			NextFieldCheckAndSet((int)Enums.Startingpoints.REDSTARTINGFIELD);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].transform.position, (int)Enums.Startingpoints.REDSTARTINGFIELD, _tempFigureIndex);
		}

		//Index 35 in waypoint array
       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 35 && _dice == 6)
		{
			NextFieldCheckAndSet(1);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[1].transform.position, 1, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 35 && _dice == 5)
		{
			NextFieldCheckAndSet((int)Enums.Startingpoints.REDSTARTINGFIELD);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].transform.position, (int)Enums.Startingpoints.REDSTARTINGFIELD, _tempFigureIndex);
		}

		//Index 36 in waypoint array
       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 36 && _dice == 6)
		{
			NextFieldCheckAndSet(2);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[2].transform.position, 2, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 36 && _dice == 5)
		{
			NextFieldCheckAndSet(1);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[1].transform.position, 1, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 36 && _dice == 4)
		{
			NextFieldCheckAndSet((int)Enums.Startingpoints.REDSTARTINGFIELD);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].transform.position, (int)Enums.Startingpoints.REDSTARTINGFIELD, _tempFigureIndex);
		}

		//Index 37 in waypoint array
       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 37 && _dice == 6)
		{
			NextFieldCheckAndSet(3);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[3].transform.position, 3, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 37 && _dice == 5)
		{
			NextFieldCheckAndSet(2);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[2].transform.position, 2, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 37 && _dice == 4)
		{
			NextFieldCheckAndSet(1);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[1].transform.position, 1, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 37 && _dice == 3)
		{
			NextFieldCheckAndSet((int)Enums.Startingpoints.REDSTARTINGFIELD);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].transform.position, (int)Enums.Startingpoints.REDSTARTINGFIELD, _tempFigureIndex);
		}

		//Index 38 in waypoint array
       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 38 && _dice == 6)
		{
			NextFieldCheckAndSet(4);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[4].transform.position, 4, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 38 && _dice == 5)
		{
			NextFieldCheckAndSet(3);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[3].transform.position, 3, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 38 && _dice == 4)
		{
			NextFieldCheckAndSet(2);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[2].transform.position, 2, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 38 && _dice == 3)
		{
			NextFieldCheckAndSet(1);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[1].transform.position, 1, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 38 && _dice == 2)
		{
			NextFieldCheckAndSet((int)Enums.Startingpoints.REDSTARTINGFIELD);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].transform.position, (int)Enums.Startingpoints.REDSTARTINGFIELD, _tempFigureIndex);
		}

		//Index 39 in waypoint array
       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 39 && _dice == 6)
		{
			NextFieldCheckAndSet(5);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[5].transform.position, 5, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 39 && _dice == 5)
		{
			NextFieldCheckAndSet(4);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[4].transform.position, 4, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 39 && _dice == 4)
		{
			NextFieldCheckAndSet(3);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[3].transform.position, 3, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 39 && _dice == 3)
		{
			NextFieldCheckAndSet(2);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[2].transform.position, 2, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 39 && _dice == 2)
		{
			NextFieldCheckAndSet(1);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[1].transform.position, 1, _tempFigureIndex);
		}

       else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) == 39 && _dice == 1)
		{
			NextFieldCheckAndSet((int)Enums.Startingpoints.REDSTARTINGFIELD);
			m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].transform.position, (int)Enums.Startingpoints.REDSTARTINGFIELD, _tempFigureIndex);
		}
	}

    //----------------------------------------------------------------------------//

    //Sets the waypoint the figure is leaving behind to 
    //color neutral, index -1, occupied to false 
    private void ClearWaypoint()
	{
        m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex)].SetFigureIndex(-1);
        m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex)].SetPointColor(Enums.Color.NEUTRAL);        
	}

    //----------------------------------------------------------------------------//

    //Does the same as clearWaypoint() above,
    //but takes a player index and a figure index to clear the occupied waypoints on the board at the end of the game
    private void ClearBoard(int PlayerIndex, int FigureIndex)
    {
        m_allWayPoints[m_players[PlayerIndex].GetFigureLocationIndex(FigureIndex)].SetFigureIndex(-1);
        m_allWayPoints[m_players[PlayerIndex].GetFigureLocationIndex(FigureIndex)].SetPointColor(Enums.Color.NEUTRAL);        
    }

    //----------------------------------------------------------------------------//

    //checks if the way point the figure is going to rest on next is occupied or not 
    private bool NextWaypointOccupied(int Index)
	{
		bool occupied;
		if(m_allWayPoints[Index].GetPointColor() != Enums.Color.NEUTRAL)
		{
			occupied = true;
		}
		else 
		{
			occupied = false;
		}	
		return occupied;
	}

    //----------------------------------------------------------------------------//

    //Sets the next waypoint according to the figure's 
    //color, index and sets occupied to true
    private void SetWaypoint(int PointIndex, int FigureIndex)
	{
        m_allWayPoints[PointIndex].SetPointColor(_currentPlayerTurn);		
		m_allWayPoints[PointIndex].SetFigureIndex(FigureIndex);
	}

    //----------------------------------------------------------------------------//

    //Gets called when a figure rests on another one
    //The figure that came first gets put back to its base
    private void KickFigureFromBoard(int PointIndex)
	{
		int figureColor;
		int figureIndex;
		figureColor = (int)m_allWayPoints[PointIndex].GetPointColor();
		figureIndex = m_allWayPoints[PointIndex].GetFigureIndex();
		m_players[figureColor].ReturnFigureToBase(figureIndex);
	}

    //----------------------------------------------------------------------------//

    //Calls nextWayPointOccupied(),
    // kickFigureFromBoard(),
    //and setWaypoint() in one go
    private void NextFieldCheckAndSet(int PointIndex)
	{
		if(NextWaypointOccupied(PointIndex))
		{
			KickFigureFromBoard(PointIndex);
		}
		SetWaypoint(PointIndex, _tempFigureIndex);
	}

    //----------------------------------------------------------------------------//

    //Checks if the starting point is occupied and 
    //automatically sets the right figure index according to the outcome
    private int StartingPointOccupied()
	{
		//Checks if a figure of the player's team is already on the starting point.
		//Returns the figure's index if yes and if not returns -1
		_tempFigureIndex = m_players[(int)_currentPlayerTurn].CheckIfFigureOnStartPoint();
		

		//If there is no figure on the starting point calls next to go 
		//and assigns that figure's index as the tempfigure
		if(_tempFigureIndex == -1 && m_players[(int)_currentPlayerTurn].GetFiguresInBase() > 0)
		{			
			_tempFigureIndex = m_players[(int)_currentPlayerTurn].GetNextToGo();
		}		
		return _tempFigureIndex;
	}

    //----------------------------------------------------------------------------//

    public bool EnemyFigureOnField(int WaypointIndex)
    {
        bool enemyOnField = false;
        //m_allWayPoints[WaypointIndex].GetPointStatus();
        if (m_allWayPoints[WaypointIndex].GetPointColor() != _currentPlayerTurn)
        {
            enemyOnField = true;
        }
        return enemyOnField;
    }

    //----------------------------------------------------------------------------//

    //Places the figure on the destination according to the roll if the point is not occupied
    //If the point is occupied it does nothing
    //I had to hard code the fields close to the destination point and look at each roll and color individually
    //I should look for a better solution
    public void EnterDestination(int FigureIndex, int Roll)
	{
        //Handles the red player
		if(_currentPlayerTurn == (int)Enums.Color.RED)
		{
            if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 34 && Roll == 6)				
			{
                if(m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
                {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
                }				
			}
 
            else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 35 && Roll == 5)
			{
                if(m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
                {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);	
                }
							
			}
            else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 35 && Roll == 6)
			{
                if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
                {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);		
                }						
			}
            else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && Roll == 4)
			{
                if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
                {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
                }
			}
            else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && Roll == 5)
			{
                if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
                {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
                }
			}
            else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && Roll == 6)
			{
                if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
                {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
                }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                   ClearWaypoint();
                   m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                   ClearWaypoint();
                   m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                   ClearWaypoint();
                   m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                   ClearWaypoint();
                   m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                   ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && Roll == 6)
			{
				print("Roll too high.");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && Roll == 1)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && Roll == 5)
			{
				print("Roll too high.");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && Roll == 6)
			{
				print("Roll too high.");
			}
			else 
			{
                ClearWaypoint();
                NextFieldCheckAndSet(m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll);
                m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll].transform.position, m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll, FigureIndex);				
			}
		}
		else if(_currentPlayerTurn == Enums.Color.BLUE)
		{
           if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 4 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 5 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 5 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 6 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 6 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 6 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 7 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 7 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 7 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 7 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 8 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 8 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 8 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 8 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 8 && Roll == 6)
			{
				print("The Roll too high");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 9 && Roll == 1)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 9 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 9 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 9 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 9 && Roll == 5)
			{
				print("The Roll too high");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 9 && Roll == 6)
			{
				print("The Roll too high");
			}
			else 
			{
                ClearWaypoint();
               if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) + Roll <= 39)
				{
                   NextFieldCheckAndSet(m_players[(int)_currentPlayerTurn].GetComponent<Player>().GetFigureLocationIndex(FigureIndex) + Roll);
                    m_players[(int)_currentPlayerTurn].GetComponent<Player>().SetFigureLocation(m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll].transform.position, m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll, FigureIndex);
				}
				else
				{
					CriticalZone();
				}
			}
		}
		else if(_currentPlayerTurn == Enums.Color.GREEN)
		{
           if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 14 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 15 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 15 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 16 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 16 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 16 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 17 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 17 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 17 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 17 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 18 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 18 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 18 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 18 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 18 && Roll == 6)
			{
				print("The Roll too high");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 19 && Roll == 1)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 19 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 19 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 19 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 19 && Roll == 5)
			{
				print("The Roll too high");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 19 && Roll == 6)
			{
				print("The Roll too high");
			}
			else 
			{
				ClearWaypoint();

               if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) + Roll <= 39)
				{
                   NextFieldCheckAndSet(m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll);
                    m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll].transform.position, m_players[(int) _currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll, FigureIndex);
				}
				else
				{
					CriticalZone();
				}
			}
		}
		else if(_currentPlayerTurn == Enums.Color.YELLOW)
		{
           if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 24 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 25 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 25 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 26 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 26 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 26 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 27 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 27 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 27 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 27 && Roll == 6)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 28 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 28 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 28 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 28 && Roll == 5)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 28 && Roll == 6)
			{
				print("The Roll too high");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 29 && Roll == 1)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(0))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(0, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 29 && Roll == 2)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(1))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(1, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 29 && Roll == 3)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(2))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(2, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 29 && Roll == 4)
			{
               if (m_players[(int)_currentPlayerTurn].CanEnterDestination(3))
               {
                    ClearWaypoint();
                    m_players[(int)_currentPlayerTurn].PlaceFigureOnDestinationPoint(3, FigureIndex);
               }
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 29 && Roll == 5)
			{
				print("The Roll too high");
			}
           else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 29 && Roll == 6)
			{
				print("The Roll too high");
			}
			else 
			{
                ClearWaypoint();
               if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(_tempFigureIndex) + Roll <= 39)
				{
                   NextFieldCheckAndSet(m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll);
                    m_players[(int)_currentPlayerTurn].SetFigureLocation(m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll].transform.position, m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + Roll, FigureIndex);
				}
				else
				{
					CriticalZone();
				}
			}
		}
	}

    //----------------------------------------------------------------------------//

    /*
    Sets the click to 1
    Returns all figures to their bases,
    Clears all waypoints 
    Sets the currentPlayers turn to the first player
    Sets the output for currentplayer and the info text to display the appropriate information 
    */
    public void ResetGame()
    {
        _click = 1;
        for (int i = 0; i < _PLAYERS; i++)
        {
            for (int j = 0; j < _FIGURES; j++)
            {
                ClearBoard(i, j);
                m_players[i].GetFigureLocationIndex(j);
                m_players[i].ReturnFigureToBase(j);                
            }            
        }
        _currentPlayerTurn = Enums.Color.RED ;
        m_ui.OutputCurrentPlayerTurn(_currentPlayerTurn);
        m_ui.OutputInfoText("Please roll your dye");
    }

    //----------------------------------------------------------------------------//

    /**
    I might use this at a later point, but for now I have to seperate it into clicks.
    private void aiTurn()
    {
        dice = diceThrow(dice);
        print(currentPlayerTurn + " Roll " + dice);
        screen.GetComponent<Screen>().outputInfoText(" Player 2 rolled " + dice);
 
        if (figureOnStartingPoint == true)
        {
            figureOnStartingPoint = false;
            click = 2;
        }
 
        //When the ai rolls six and the its base is empty
        //the ai can choose the figure to play
        else if (dice == 6 && players[currentPlayerTurn - 1].GetComponent<Player>().getNextToGo() == -1)
        {
            Debug.Log("A");
            screen.GetComponent<Screen>().outputInfoText("Player 2 rolled " + dice + "\nChoosing figure");           
            tempFigureIndex = players[currentPlayerTurn - 1].GetComponent<Player>().chooseFigure(dice);
            click = 2;
        }
 
        //When the player rolls six and the base is not empty
        else if (dice == 6 && players[currentPlayerTurn - 1].GetComponent<Player>().getNextToGo() > -1)
        {
            Debug.Log("B");
            tempFigureIndex = players[currentPlayerTurn - 1].GetComponent<Player>().getNextToGo();
            click = 2;
        }
 
        //When the player rolls lower six and there is at least one figure on the field
        else if (dice < 6 && players[currentPlayerTurn - 1].GetComponent<Player>().getFiguresInBase() < 4)
        {
            Debug.Log("C");
            screen.GetComponent<Screen>().outputInfoText("Player 2 rolled " + dice + "\nChoosing figure");
            //Hier vielleicht eine funktion machen die automatisch waehlt.
           
           tempFigureIndex = players[currentPlayerTurn - 1].GetComponent<Player>().chooseFigure(dice);
            click = 2;
        }
 
        if (click == 2)
        {
            Debug.Log("D");
            print("ChosenTempFigIn " + tempFigureIndex);
            play();
            click = 1;
            
            screen.GetComponent<Screen>().outputInfoText("Please role your dye");
        }
 
        //When the player rolls lower than six and no figures are on the board
        else
        {
            nextPlayerTurn();
        }                
    }
     **/

    //----------------------------------------------------------------------------//

    //Checks if the next point the figure is going to is in the ciritcal zone
    //and returns whether it is occupied by a rival player or not 
    //else checks the next point outside of the citical zone and return whether it is occupied by a rival player or not
    public bool CriticalZoneAI(int FigureIndex)
    {
        bool pointStatus = false;
        //Index 34 in waypoint array
        if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 34 && _dice == 6)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 35 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 35 && _dice == 6)
        {
            if (m_allWayPoints[1].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 35 && _dice == 5)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 36 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && _dice == 6)
        {
            if (m_allWayPoints[2].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && _dice == 5)
        {
            if (m_allWayPoints[1].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && _dice == 4)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 37 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 6)
        {
            if (m_allWayPoints[3].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[3].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 5)
        {
            if (m_allWayPoints[2].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 4)
        {
            if (m_allWayPoints[1].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 3)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 38 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 6)
        {
            if (m_allWayPoints[4].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[4].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 5)
        {
            if (m_allWayPoints[3].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[3].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 4)
        {
            if (m_allWayPoints[2].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 3)
        {
            if (m_allWayPoints[1].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 2)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 39 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 6)
        {
            if (m_allWayPoints[5].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[5].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 5)
        {
            if (m_allWayPoints[4].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[4].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 4)
        {
            if (m_allWayPoints[3].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[3].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 3)
        {
            if (m_allWayPoints[2].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 2)
        {
            if (m_allWayPoints[1].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 1)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
        else
        {
            if (m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + _dice].GetPointColor() != _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + _dice].GetOccupied();
            }
        }
        return pointStatus;
    }

    //----------------------------------------------------------------------------//

    //Checks if the next point the figure is going to is in the ciritcal zone
    //and returns whether it is occupied by a teammate or not 
    //else checks the next point outside of the citical zone and return whether it is occupied by a teammate or not
    public bool CriticalZoneAITeammates(int FigureIndex)
    {
        bool pointStatus = false;
        //Index 34 in waypoint array
        if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 34 && _dice == 6)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 35 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 35 && _dice == 6)
        {
            if (m_allWayPoints[1].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 35 && _dice == 5)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 36 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && _dice == 6)
        {
            if (m_allWayPoints[2].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && _dice == 5)
        {
            if (m_allWayPoints[1].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 36 && _dice == 4)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 37 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 6)
        {
            if (m_allWayPoints[3].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[3].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 5)
        {
            if (m_allWayPoints[2].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 4)
        {
            if (m_allWayPoints[1].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 37 && _dice == 3)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 38 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 6)
        {
            if (m_allWayPoints[4].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[4].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 5)
        {
            if (m_allWayPoints[3].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[3].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 4)
        {
            if (m_allWayPoints[2].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 3)
        {
            if (m_allWayPoints[1].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 38 && _dice == 2)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
 
        //Index 39 in waypoint array
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 6)
        {
            if (m_allWayPoints[5].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[5].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 5)
        {
            if (m_allWayPoints[4].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[4].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 4)
        {
            if (m_allWayPoints[3].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[3].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 3)
        {
            if (m_allWayPoints[2].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[2].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 2)
        {
            if (m_allWayPoints[1].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[1].GetOccupied();
            }
        }
 
        else if (m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) == 39 && _dice == 1)
        {
            if (m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[(int)Enums.Startingpoints.REDSTARTINGFIELD].GetOccupied();
            }
        }
        else
        {
            if (m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + _dice].GetPointColor() == _currentPlayerTurn)
            {
                pointStatus = m_allWayPoints[m_players[(int)_currentPlayerTurn].GetFigureLocationIndex(FigureIndex) + _dice].GetComponent<Point>().GetOccupied();
            }
        }
        return pointStatus;
    }

    //----------------------------------------------------------------------------//
}
