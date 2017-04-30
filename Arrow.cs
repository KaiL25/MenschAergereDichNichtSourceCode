using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    [SerializeField]
    private GameObject m_Arrow;

    [SerializeField]
    private float m_ySpeed = .04f;

    bool _ascend;

    //----------------------------------------------------------------------------//

    // Use this for initialization
    void Start () 
    {
        _ascend = true;       
	}

    //----------------------------------------------------------------------------//

    // Update is called once per frame
    // Takes care of the floating "animation"
    void FixedUpdate () 
    {
        if (m_Arrow.activeSelf)
        {
            if (m_Arrow.transform.position.y >= 3.7)
            {
                _ascend = false;
            }

            else if (m_Arrow.transform.position.y <= 2.6)
            {
                _ascend = true;
            }

            if (_ascend)
            {
                m_Arrow.transform.Translate(0, m_ySpeed, 0);
            }

            else if (!_ascend)
            {
                m_Arrow.transform.Translate(0, -m_ySpeed, 0);
            }
        }        
	}

    //----------------------------------------------------------------------------//

    //Sets the arrow mesh to visible or invisible according to the bool passed in
    public void SetArrowVisibility(bool status)
    {
        m_Arrow.SetActive(status);
    }

    //----------------------------------------------------------------------------//

    //Sets the arrow location according to the vector3 passed in
    //Should always float above a figure's head
    public void SetArrowLocation(Vector3 newLocation)
    {
        newLocation.y = 2.7f;
        m_Arrow.transform.position = newLocation;
    }

    //----------------------------------------------------------------------------//

    public bool GetArrowVisibility()
    {
        return m_Arrow.activeSelf;
    }
}
