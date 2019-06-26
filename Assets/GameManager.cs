using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Menu Variables
    [SerializeField] private GameObject m_PauseMenu;
    #endregion

    #region UI Variables
    [SerializeField] private TextMeshProUGUI m_FundText;
    #endregion

    #region Game Variables
    [SerializeField] private int m_Fund = 0;
    #endregion
    
    private void Awake()
    {
        m_PauseMenu.SetActive(false);
    }

    private void Update()
    {
        #region Pause Menu Action
        if (Input.GetKeyDown(KeyCode.Escape) && m_PauseMenu != null) {
            if (m_PauseMenu.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
        #endregion

    }

    #region Menu Button Actions
    private void PauseGame()
    {
        Time.timeScale = 0;
        m_PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        m_PauseMenu.SetActive(false);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region UI
    private void UpdateFund()
    {
        Debug.Log("Updating fund");
        m_FundText.SetText("$" + m_Fund);
    }
    #endregion

    #region Ingame Actions
    public void CompleteDelivery(int _money)
    {
        Debug.Log("Delivery completed");
        AddFund(_money);
        UpdateFund();
    }

    public void AddFund(int _fund)
    {

        m_Fund += _fund;
        UpdateFund();
    }

    public void MinusFund(int _fund)
    {
        m_Fund -= _fund;
        UpdateFund();
    }

    public int GetFund()
    {
        return m_Fund;
    }

    #endregion


}
