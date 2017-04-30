using UnityEngine;
using System.Collections;




public class Figure : MonoBehaviour 
{
    [SerializeField]
	private Enums.Color _color;

	private int _field;
	private bool _inBox;
	private bool _atDestination;
	public Color _startColor;
	public Color _mouseOverColor;
	private bool _active;
	bool _mouseOver = false;

    //----------------------------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		_inBox = true;
		_atDestination = false;
		_field = 0;
		_active = false;		
	}

    //----------------------------------------------------------------------------//

    public bool GetInBox()
	{
		return _inBox;
	}

    //----------------------------------------------------------------------------//

    public void SetInBox(bool statement)
	{
		_inBox = statement;
	}

    //----------------------------------------------------------------------------//

    public void SetField(int NewField)
	{
		_field = NewField;
	}

    //----------------------------------------------------------------------------//

    public int GetField()
	{
		return _field;
	}

    //----------------------------------------------------------------------------//

    public void SetAtDestination(bool status)
	{
		_atDestination = status;
	}

    //----------------------------------------------------------------------------//

    public bool GetAtDestination()
	{
		return _atDestination;
	}

    //----------------------------------------------------------------------------//

    void OnMouseEnter()
	{		
		if(_active)
		{
			_mouseOver = true;
			this.GetComponent<Renderer>().material.SetColor("_Color", _mouseOverColor);
		}
	}

    //----------------------------------------------------------------------------//

    void OnMouseExit()
	{		
		_mouseOver = false;
		this.GetComponent<Renderer>().material.SetColor("_Color", _startColor);
	}

    //----------------------------------------------------------------------------//

    public void SetActive(bool status)
	{
		_active = status;
	}

    //----------------------------------------------------------------------------//

    public bool GetMouseOver()
	{
		return _mouseOver;
	}

    //----------------------------------------------------------------------------//
}
