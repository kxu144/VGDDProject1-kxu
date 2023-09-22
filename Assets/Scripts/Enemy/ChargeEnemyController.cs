using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyController : EnemyController
{
    [SerializeField] private float aggroDist;
    protected override void FixedUpdate() {
        Vector3 dir = cr_player.position - transform.position;
        if (dir.magnitude < aggroDist) {
            Debug.Log(dir.magnitude);
            dir.Normalize();
            cc_rb.MovePosition(cc_rb.position + dir * m_speed * Time.fixedDeltaTime);
        }
    }
}