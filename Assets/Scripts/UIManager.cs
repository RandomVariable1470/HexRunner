using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject TapToStartBtn;
    [SerializeField] private GameObject HueBtn;
    [SerializeField] private GameObject HueWheel;

    private Animator hueWheelAnim;

    private readonly int IN_TAG = Animator.StringToHash("In");

    public static UIManager Instance { get; private set; }

    #region Initialization

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        hueWheelAnim = HueWheel.GetComponent<Animator>();
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

    public void HueWheelIn()
    {
        hueWheelAnim.SetBool(IN_TAG, true);
    }

    public void HueWheelOut()
    {
        hueWheelAnim.SetBool(IN_TAG, false);
    }
}