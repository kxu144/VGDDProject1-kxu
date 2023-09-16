using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    #region Private Variables
    private int m_score;
    #endregion

    #region Initialization
    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else if (singleton != this) {
            Destroy(gameObject);
        }
        m_score = 0;
    }
    #endregion

    #region Score
    public void IncreaseScore(int amount) {
        m_score += amount;
    }

    private void UpdateHighScore() {
        if (!PlayerPrefs.HasKey("HS")) {
            PlayerPrefs.SetInt("HS", m_score);
            return;
        }
        int hs = PlayerPrefs.GetInt("HS");
        if (m_score > hs) {
            PlayerPrefs.SetInt("HS", m_score);
        }
    }
    #endregion

    #region Destruction
    private void OnDisable() {
        UpdateHighScore();
    }
    #endregion
}
