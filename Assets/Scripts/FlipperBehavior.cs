using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperBehavior : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_kKey;
    [SerializeField]
    private float m_fTargetPos = 0f;

    private float m_fFirstPos = 0f;
    private float m_fSpring = 10000f;
    private float m_fDamper = 60f;
    private HingeJoint m_hingeJoint = null;
    private JointSpring m_jointSpring;

    void Start()
    {
        m_hingeJoint = GetComponent<HingeJoint>();
        m_hingeJoint.useSpring = true;

        m_jointSpring = new JointSpring();
        m_jointSpring.spring = m_fSpring;
        m_jointSpring.damper = m_fDamper;

        m_hingeJoint.spring = m_jointSpring;
    }

    void Update()
    {

        if (Input.GetKeyDown(m_kKey))
        {
            m_jointSpring.targetPosition = m_fTargetPos;
        }
        if (Input.GetKeyUp(m_kKey))
        {
            m_jointSpring.targetPosition = m_fFirstPos;
        }
        m_hingeJoint.spring = m_jointSpring;
    }
}
