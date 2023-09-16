using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPill : MonoBehaviour
{
    #region Editor Variables
    [SerializeField] private int m_healthgain;
    public int HealthGain {
        get {
            return m_healthgain;
        }
    }
    #endregion
}
