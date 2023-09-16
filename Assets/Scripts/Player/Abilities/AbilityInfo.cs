using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    #region Editor Variables
    [SerializeField] private int m_power;
    public int Power {
        get {
            return m_power;
        }
    }

    [SerializeField] private float m_range;
    public float Range {
        get {
            return m_range;
        }
    }
    #endregion
}
