using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject TapToStartBtn;
    [SerializeField] private GameObject HueBtn;
    [SerializeField] private GameObject HueWheel;

    #region Initialization

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState gameState)
    {
        TapToStartBtn.SetActive(gameState == GameState.Menu);
        HueBtn.SetActive(gameState != GameState.Menu);
        HueWheel.SetActive(gameState == GameState.SelectColor);
    }

    #endregion

    public void TapToStartFunc()
    {
        GameManager.instance.UpdateState(GameState.Play);
        GameManager.instance.isPlaying = true;
    }

    public void HueBtnFunc()
    {
        GameManager.instance.UpdateState(GameState.SelectColor);
    }

    public void HueColorBtnsFunc()
    {
        GameManager.instance.UpdateState(GameState.Play);
    }
}