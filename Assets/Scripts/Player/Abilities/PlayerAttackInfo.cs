using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerAttackInfo
{
    #region Editor Varaibles
    [SerializeField] private string m_name;
    public string AttackName {
        get {
            return m_name;
        }
    }

    [SerializeField] private string m_button;
    public string Button {
        get {
            return m_button;
        }
    }

    [SerializeField] private string m_trigger;
    public string Trigger {
        get {
            return m_trigger;
        }
    }

    [SerializeField] private GameObject m_ability;
    public GameObject Ability {
        get {
            return m_ability;
        }
    }

    [SerializeField] private Vector3 m_offset;
    public Vector3 Offset {
        get {
            return m_offset;
        }
    }

    [SerializeField] private float m_winduptime;
    public float WindUpTime {
        get {
            return m_winduptime;
        }
    }

    [SerializeField] private float m_frozentime;
    public float FrozenTime {
        get {
            return m_frozentime;
        }
    }

    [SerializeField] private float m_cooldown;

    [SerializeField] private int m_healthcost;
    public int HealthCost {
        get {
            return m_healthcost;
        }
    }

    [SerializeField] private Color m_color;
    public Color AbilityColor {
        get {
            return m_color;
        }
    }

    [SerializeField] private Text m_text;
    public Text AbilityText {
        get {
            return m_text;
        }
    }
    #endregion

    #region Public Variables
    public float Cooldown {
        get;
        set;
    }
    #endregion

    #region Cooldown Methods
    public void ResetCooldown() {
        Cooldown = m_cooldown;
    }

    public bool IsReady() {
        return Cooldown <= 0;
    }
    #endregion
}
