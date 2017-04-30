using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI : MonoBehaviour {

    [SerializeField]
    private GameObject m_panel;

    [SerializeField]
    private GameObject m_playButton;

    [SerializeField]
    private GameObject m_rulesButton;

    [SerializeField]
    private GameObject m_exitButton;

    [SerializeField]
    private GameObject m_title;

    [SerializeField]
    private GameObject m_playerTurnText;

    [SerializeField]
    private GameObject m_infoText;

    [SerializeField]
    private GameController m_gameController;

    [SerializeField]
    private GameObject m_gameRulesHeader;

    [SerializeField]
    private GameObject m_gameRules;

    [SerializeField]
    private GameObject m_returnButton;

    [SerializeField]
    private GameObject m_onePlayerButton;

    [SerializeField]
    private GameObject m_twoPlayersButton;

    [SerializeField]
    private GameObject m_threePlayersButton;

    [SerializeField]
    private GameObject m_fourPlayersButton;

    [SerializeField]
    private GameObject m_winnerText;

    private bool _playing;

    //----------------------------------------------------------------------------//

    // Use this for initialization
    void Start () 
    {       
       m_panel.SetActive(false);      
       m_playerTurnText.SetActive(false);      
       m_infoText.SetActive(false);
       m_infoText.GetComponent<Text>().color = Color.black;          
       m_gameRulesHeader.SetActive(false);       
       m_gameRules.SetActive(false);      
       m_returnButton.SetActive(false);       
       m_onePlayerButton.SetActive(false);       
       m_twoPlayersButton.SetActive(false);       
       m_threePlayersButton.SetActive(false);       
       m_fourPlayersButton.SetActive(false);      
       m_winnerText.SetActive(false);
       _playing = false;
        
    }

    //----------------------------------------------------------------------------//

    // Update is called once per frame
    void Update () 
    {
	    if (Input.GetKeyDown(KeyCode.Mouse0) && _playing)
        {
           m_gameController.DeterminePlayerTurn();
        }
	}

    //----------------------------------------------------------------------------//

    //Sets the playing value in the game controller to true,
    //hides the menu screen and shows the game ui
    public void StartNewGame(int Players)
    {        
        HidePlayerButtons();
        ShowGameUI();
        m_returnButton.SetActive(true);        
        _playing = true;        
    }

    //----------------------------------------------------------------------------//

    //Shows the game's menu
    private void ShowMenu()
    {
        m_playButton.SetActive(true);
        m_rulesButton.SetActive(true);
        m_exitButton.SetActive(true);
        m_title.SetActive(true);
    }

    //----------------------------------------------------------------------------//

    //Hides the game's menu
    private void HideMenu()
    {
        print(m_playButton.activeSelf);
        m_playButton.SetActive(false);
        m_rulesButton.SetActive(false);
        m_exitButton.SetActive(false);
        m_title.SetActive(false);
    }

    //----------------------------------------------------------------------------//

    //Shows the game's UI
    private void ShowGameUI()
    {
        m_playerTurnText.SetActive(true);
        m_infoText.SetActive(true);
    }

    //----------------------------------------------------------------------------//

    //Hides the game's UI
    private void HideGameUI()
    {
        m_playerTurnText.SetActive(false);
        m_infoText.SetActive(false);
    }

    //----------------------------------------------------------------------------//

    //Hides the menu and shows the game's rules 
    public void ShowRules()
    {
        HideMenu();
        m_gameRulesHeader.SetActive(true);
        m_gameRules.SetActive(true);
        m_returnButton.SetActive(true);
    }

    //----------------------------------------------------------------------------//

    //Hides the game's rules 
    public void HideRules()
    {
        m_gameRulesHeader.SetActive(false);
        m_gameRules.SetActive(false);
        m_returnButton.SetActive(false);
    }

    //----------------------------------------------------------------------------//

    //Shows who won the game
    //and gives the option to return to the main screen, or play another game
    public void ShowGameEndScreen(int Winner)
    {
        HideGameUI();
        m_winnerText.SetActive(true);
        m_winnerText.GetComponent<Text>().text = "Player " + Winner + " won the game";
        _playing = false;
        
        if (Winner == 1)
        {
            m_winnerText.GetComponent<Text>().color = Color.red;
        }
        else if (Winner == 2)
        {
            m_winnerText.GetComponent<Text>().color = Color.blue;
        }
        else if (Winner == 3)
        {
            m_winnerText.GetComponent<Text>().color = Color.green;
        }
        else if (Winner == 4)
        {
            m_winnerText.GetComponent<Text>().color = Color.yellow;
        }
        m_returnButton.SetActive(true);
    }

    //----------------------------------------------------------------------------//

    //Hides the screen showing who won the game
    public void HideGameEndScreen()
    {
        m_winnerText.SetActive(false);
    }

    //----------------------------------------------------------------------------//

    public void CloseGame()
    {
        Application.Quit();
    }

    //----------------------------------------------------------------------------//

    //Returns to the main menu when the return button is pressed
    public void ReturnToMenu()
    {
        ShowMenu();
        //If the return button is pressed inside the rules screen
        if(m_gameRulesHeader.activeSelf)
        {
            HideRules();
        }
        //If the return button is pressed when the number of players is determined
        else if (m_onePlayerButton.activeSelf)
        {
            HidePlayerButtons();
        }
        //If the return button is pressed during game
        else if (m_playerTurnText.activeSelf)
        {            
            m_gameController.ResetGame();
            OutputCurrentPlayerTurn(Enums.Color.RED);
            m_infoText.GetComponent<Text>().text = "Please role your dye.";
            HideGameUI();
            _playing = false;
        }
        else if (m_winnerText.activeSelf)
        {
            HideGameEndScreen();            
        }
        
        m_returnButton.SetActive(false);
    }

    //----------------------------------------------------------------------------//

    //Hides the menu, shows the player number buttons and the return button
    public void ShowPlayerButtons()
    {
        HideMenu();
        m_onePlayerButton.SetActive(true);
        //m_twoPlayersButton.SetActive(true);
        //m_threePlayersButton.SetActive(true);
        //m_fourPlayersButton.SetActive(true);
        m_returnButton.SetActive(true);
    }

    //----------------------------------------------------------------------------//

    //Hides the player buttons and the return button
    public void HidePlayerButtons()
    {
        m_onePlayerButton.SetActive(false);
        //m_twoPlayersButton.SetActive(false);
        //m_threePlayersButton.SetActive(false);
        //m_fourPlayersButton.SetActive(false);
        m_returnButton.SetActive(false);
    }

    //----------------------------------------------------------------------------//

    //Gets called when 1 player button is pressed
    //Initializes a new game with one player and 3 ai opponents
    public void OnePlayerGame()
    {
        StartNewGame(1);
    }

    //----------------------------------------------------------------------------//
    
    /*MULTIPLAYER MAY BE ADDED LATER ON
    //Gets called when 2 players button is pressed
    //Initializes a new game with two players and 2 ai opponents
    public void TwoPlayerGame()
    {
        StartNewGame(2);
    }

    //----------------------------------------------------------------------------//

    //Gets called when 3 players button is pressed
    //Initializes a new game with three players and 1 ai opponents
    public void ThreePlayerGame()
    {
        StartNewGame(3);
    }

    //----------------------------------------------------------------------------//

    //Gets called when 4 players button is pressed
    //Initializes a new game with 4 players and 0 ai opponents
    public void FourPlayerGame()
    {
        StartNewGame(4);
    }
    */
    //----------------------------------------------------------------------------//

    //Prints the current player's turn and sets the color accordingly during the game
    public void OutputCurrentPlayerTurn(Enums.Color CurrentPlayerTurn)
    {
        if (CurrentPlayerTurn == Enums.Color.RED)
        {
            m_playerTurnText.GetComponent<Text>().text = "Player " + CurrentPlayerTurn;
            m_playerTurnText.GetComponent<Text>().color = Color.red;
        }
        else if (CurrentPlayerTurn == Enums.Color.BLUE)
        {
            m_playerTurnText.GetComponent<Text>().text = "Player " + CurrentPlayerTurn;
            m_playerTurnText.GetComponent<Text>().color = Color.blue;
        }
        else if (CurrentPlayerTurn == Enums.Color.GREEN)
        {
            m_playerTurnText.GetComponent<Text>().text = "Player " + CurrentPlayerTurn;
            m_playerTurnText.GetComponent<Text>().color = Color.green;
        }
        else if (CurrentPlayerTurn == Enums.Color.YELLOW)
        {
            m_playerTurnText.GetComponent<Text>().text = "Player " + CurrentPlayerTurn;
            m_playerTurnText.GetComponent<Text>().color = Color.yellow;
        }
    }

    //----------------------------------------------------------------------------//

    //Outputs any new information for the players during the game
    public void OutputInfoText(string NewInfo)
    {
        m_infoText.GetComponent<Text>().text = NewInfo;
    }

    //----------------------------------------------------------------------------//

    //Hides the current game's ui and returns to the menu
    public void GameOver()
    {
        HideGameUI();
        ShowMenu();
    }

    //----------------------------------------------------------------------------//

    public void SetPlaying(bool Status)
    {
        _playing = Status;
    }

    //----------------------------------------------------------------------------//
}
