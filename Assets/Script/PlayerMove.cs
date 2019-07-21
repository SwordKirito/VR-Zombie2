using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    // Use this for initialization
    void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Transform = gameObject.GetComponent<Transform>();
        //m_Rigidbody.MovePosition(m_Transform.position + Vector3.forward*10);
    }

    // Update is called once per frame
    void Update()
    {
        // m_Rigidbody.MovePosition(Vector3.forward);

        if (Input.GetKey(KeyCode.W))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.left * 0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.back * 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.right * 0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.forward * 0.1f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.down * 0.1f);
        }




    }
}
