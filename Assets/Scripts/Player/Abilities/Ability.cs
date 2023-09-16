using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    #region Editor Variables
    [SerializeField] protected AbilityInfo m_info;
    #endregion

    #region Cached Components
    protected ParticleSystem cc_ps;
    #endregion

    #region Initialization
    private void Awake() {
        cc_ps = GetComponent<ParticleSystem>();
    }
    #endregion

    #region Use Methods
    public abstract void Use(Vector3 spawnPos);
    #endregion
}
