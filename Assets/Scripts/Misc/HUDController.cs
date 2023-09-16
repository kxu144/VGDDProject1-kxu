using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField] private RectTransform m_healthbar;
    #endregion

    #region Private Variables
    private float p_maxwidth;
    #endregion

    #region Initialization
    private void Awake() {
        p_maxwidth = m_healthbar.sizeDelta.x;
    }
    #endregion

    #region Update Health Bar
    public void UpdateHealth(float percent) {
        m_healthbar.sizeDelta = new Vector2(p_maxwidth * percent, m_healthbar.sizeDelta.y);
    }
    #endregion
}
