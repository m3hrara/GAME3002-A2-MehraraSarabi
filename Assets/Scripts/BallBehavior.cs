using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_Rigidbody = null;
    [SerializeField]
    private Transform m_tSpawn;
    private Vector3 previousVel;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void RespawnBall()
    {
        if (transform.position.z < -1.0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = m_tSpawn.position;
        }
    }

    void FixedUpdate()
    {
        previousVel = m_Rigidbody.velocity;
        
        // Respawns the ball only if needed
        RespawnBall();
        FlattenVel();
    }

    void FlattenVel()
    {
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BashToy")
        {
            ContactPoint contactPoint = collision.contacts[0];
            ScoreManager.score += 100;
            // reflect the velocity and add score points
            m_Rigidbody.velocity = Vector3.Reflect(previousVel, contactPoint.normal);
        }
        if (collision.gameObject.tag == "ActiveBumper")
        {
            ContactPoint contactPoint = collision.contacts[0];
            ScoreManager.score += 40;
            // reflect the velocity, and increase the velocity
            m_Rigidbody.velocity = Vector3.Reflect(previousVel, contactPoint.normal);
            m_Rigidbody.velocity += contactPoint.normal * 6.5f;
        }
        if (collision.gameObject.tag == "PassiveBumper")
        {
            ContactPoint contactPoint = collision.contacts[0];
            ScoreManager.score += 30;
            // just reflect the velocity
            m_Rigidbody.velocity = Vector3.Reflect(previousVel, contactPoint.normal);
        }
        if (collision.gameObject.tag == "Wall")
        {
            ContactPoint contactPoint = collision.contacts[0];
            ScoreManager.score += 10;
            // reflect with a bit of speed boost
            m_Rigidbody.velocity = Vector3.Reflect(previousVel, contactPoint.normal);
            m_Rigidbody.velocity += contactPoint.normal * 2.5f;
        }
    }
}
