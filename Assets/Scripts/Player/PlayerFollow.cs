using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    #region Editor Variables
    [SerializeField] private Transform m_player;
    [SerializeField] private Vector3 m_offset;
    [SerializeField] private float m_rotspeed = 10;
    #endregion

    #region Main Updates
    private void Update() {
        Vector3 newPos = m_player.position + m_offset;
        transform.position = Vector3.Slerp(transform.position, newPos, 1);

        float rot = m_rotspeed * Input.GetAxis("Mouse X");
        transform.RotateAround(m_player.position, Vector3.up, rot);
        m_offset = transform.position - m_player.position;
    }
    #endregion
}
