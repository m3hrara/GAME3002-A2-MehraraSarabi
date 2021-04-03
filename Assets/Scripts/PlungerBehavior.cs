using UnityEngine;

public class PlungerBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_fSpringConstant;
    [SerializeField]
    private float m_fDampingConstant;
    [SerializeField]
    private Vector3 m_vRestPos;
    [SerializeField]
    private float m_fMass;
    [SerializeField]
    private Rigidbody m_attachedBody = null;

    private bool keyPressed = false;
    private Vector3 m_vForce;
    private Vector3 m_vPrevVel;

    private void Start()
    {
        m_fMass = m_attachedBody.mass;
    }

    private void FixedUpdate()
    {
        UpdateSpringForce();
    }

    private void Update()
    {
        if (!keyPressed)
        {
            m_attachedBody.isKinematic = true;
        }
        if (Input.GetKey(KeyCode.Space)) // holding down the space key will lower the plungerhead
        {
            m_attachedBody.transform.position = new Vector3(m_attachedBody.transform.position.x, m_attachedBody.transform.position.y, m_attachedBody.transform.position.z - Time.deltaTime);
        }
        if (m_attachedBody.velocity.magnitude == 0)
        {
            keyPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.Space) && keyPressed == false)
        {
            keyPressed = true;
            m_attachedBody.isKinematic = false;
        }

    }
    private void UpdateSpringForce()
    {
        // F = -kx
        // F = -kx -bv

        m_vForce = -m_fSpringConstant * (m_vRestPos - m_attachedBody.transform.position) -
            m_fDampingConstant * (m_attachedBody.velocity - m_vPrevVel);

        m_attachedBody.AddForce(m_vForce, ForceMode.Acceleration);

        m_vPrevVel = m_attachedBody.velocity;
    }
}
