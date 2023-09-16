using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaLaserAbility : Ability
{
    public override void Use(Vector3 spawnPos)
    {
        RaycastHit[] hits = Physics.SphereCastAll(spawnPos, 1f, transform.forward, m_info.Range);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.CompareTag("Enemy")) {
                hit.collider.GetComponent<EnemyController>().DecreaseHealth(m_info.Power);
            }
        }
        var emitterShape = cc_ps.shape;
        emitterShape.length = m_info.Range;
        cc_ps.Play();
    }
}
