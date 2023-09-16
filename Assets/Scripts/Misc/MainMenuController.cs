using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField] private Text m_highscore;
    #endregion

    #region Private Variables
    private string m_defaultscoretext;
    #endregion

    #region Initialization
    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
        m_defaultscoretext = m_highscore.text;
    }

    private void Start() {
        UpdateHighScore();
    }
    #endregion

    #region Buttons
    public void PlayArena() {
        SceneManager.LoadScene("Arena");
    }

    public void Quit() {
        Application.Quit();
    }

    public void Reset() {
        PlayerPrefs.SetInt("HS", 0);
        UpdateHighScore();
    }
    #endregion

    #region High Score
    private void UpdateHighScore() {
        if (!PlayerPrefs.HasKey("HS")) {
            PlayerPrefs.SetInt("HS", 0);
            
        }
        m_highscore.text = m_defaultscoretext.Replace("%S", PlayerPrefs.GetInt("HS").ToString());
    }
    #endregion
}
