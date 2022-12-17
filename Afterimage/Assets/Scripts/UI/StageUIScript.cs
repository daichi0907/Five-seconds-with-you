using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;

public class StageUIScript : UI_Effect
{
    [SerializeField] private GameData gameData;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pausePlayButton;

    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject[] howToPlayPagePanel;
    [SerializeField] private Button cancelButton;
    [SerializeField] float fadeSec = 2f;
    FadeState fadeState = FadeState.None;
    bool howToPlayFlag = false;
    bool nowEffecting = false;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float time = 500f;
    float timeLimit;

    [SerializeField] private GameObject clearPanel;
    [SerializeField] private Button nextStageButton;
    [SerializeField] private float clearStayTime = 2.0f;
    Scene nowStage;


    /// <summary> インゲーム時のデフォルトのパネル（松島） </summary>
    private GameObject _DefaultPanel;
    private InGame _CurrentInGameState;
    private InGame _PrevInGameState;

    /// <summary> クリア画面表示前のページめくり演出のパネル（松島） </summary>
    private BookUI _ClearBookUI;
    private GameObject _WarpEffectObj;
    private UI_WarpEffect _WarpEffect;
    private DirectingScript _DirectingScript;

    /// <summary> Stageへの遷移時のページめくり演出（松島） </summary>
    //private bool _IsStartFirstCutIn = false;
    private GameObject FirstCutInPanel;
    private BookUI _FirstCutInUI;
    //private float _FirstCutInTime;

    public float ClearStayTime { get { return clearStayTime; } }

    // Start is called before the first frame update
    void Start()
    {
        gameData.EditorStart();

        pausePanel.GetComponent<PauseUIAnimation>().PauseUISetUp();

        timeLimit = time;

        nowStage = SceneManager.GetActiveScene();

        // 松島
        FirstCutInPanel = transform.Find("FirstCutInPanel").gameObject;
        _FirstCutInUI = FirstCutInPanel.GetComponent<BookUI>();
        _DefaultPanel = transform.Find("DefaultPanel").gameObject;
        _DefaultPanel.SetActive(false);
        _ClearBookUI = clearPanel.GetComponent<BookUI>();

        _CurrentInGameState = InGame.None;
        _PrevInGameState = _CurrentInGameState;

        _WarpEffectObj = transform.Find("WarpEffectPanel").gameObject;
        _WarpEffect = _WarpEffectObj.GetComponent<UI_WarpEffect>();

        _DirectingScript = GameObject.Find("DirectingObj").GetComponent<DirectingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _CurrentInGameState = gameData.InGameState;
        UpdateState();

        FadeEffect();

        if (howToPlayFlag)
        {
            cancelButton.Select();
        }
        else
        {
            // ゲームクリア条件やゲーム中の演出処理を追加した際に
            // ポーズ画面に行くかどうかの条件文変更の可能性大
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick button 7"))
            {
                PauseActiveChange();
            }
        }

        // ひとつ前のインゲームステートとして保存
        _PrevInGameState = _CurrentInGameState;
    }

    #region private function
    /// <summary>
    /// 状態の変更
    /// </summary>
    private void ChangeState()
    {
        // ログを出す
        Debug.Log("ChangeState " + _PrevInGameState + "-> " + _CurrentInGameState);

        switch (_CurrentInGameState)
        {
            case InGame.CutIn:
                {
                    _DefaultPanel.SetActive(false);
                }
                break;
            case InGame.ChangeStartView:
                {
                    FirstCutInPanel.SetActive(false);
                }
                break;
            case InGame.EntryPlayer:
                {
                }
                break;
            case InGame.PlayGame:
                {
                    _DefaultPanel.SetActive(true);
                }
                break;
            case InGame.Pause:
                {
                }
                break;
            case InGame.EntryGoal:
                {
                    _DefaultPanel.SetActive(false);
                }
                break;
            case InGame.InGoal:
                {
                    ////演出追加のため、この時点でクリアパネルをオンにする（松島）
                    //clearPanel.SetActive(true);
                    //_WarpEffectObj.SetActive(true);
                    //// _WarpEffect.StartEffect();
                    //StartCoroutine(StayClearUI());
                }
                break;
            case InGame.GoalCompletion:
                {
                }
                break;
        }

    }

    /// <summary>
    /// 状態毎の毎フレーム呼ばれる処理
    /// </summary>
    private void UpdateState()
    {
        if (IsEntryThisState()) { ChangeState(); return; }

        switch (_CurrentInGameState)
        {
            case InGame.CutIn:
                {
                    CutInEffectUpdate();
                }
                break;
            case InGame.ChangeStartView:
                {
                }
                break;
            case InGame.EntryPlayer:
                {
                }
                break;
            case InGame.PlayGame:
                {
                    // チートコマンド
                    if (Input.GetKey(KeyCode.C) && Input.GetKeyDown(KeyCode.Return))
                    {
                        StageClear();
                    }

                    // 制限時間を更新する
                    timeLimit -= Time.deltaTime;
                    timerText.text = "Time:" + (int)timeLimit;
                }
                break;
            case InGame.Pause:
                {
                }
                break;
            case InGame.EntryGoal:
                {
                }
                break;
            case InGame.InGoal:
                {
                    // カメラが上を向いていたら
                    if(_DirectingScript.IsStartWarpEffect && !_WarpEffectObj.activeSelf)
                    {
                        //演出追加のため、この時点でクリアパネルをオンにする（松島）
                        clearPanel.SetActive(true);
                        _WarpEffectObj.SetActive(true);
                        // _WarpEffect.StartEffect();
                        StartCoroutine(StayClearUI());
                    }
                }
                break;
            case InGame.GoalCompletion:
                {
                    // ページをめくる演出を行う（松島）
                    _ClearBookUI.TurnPageUpdate();

                    // マウスを押した際に、進行不能になることを防ぐ。
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                        _ClearBookUI.ToUseableButton();
                }
                break;
        }
    }

    /// <summary>
    /// ちょうどそのステートに入った所かどうか
    /// </summary>
    /// <returns></returns>
    private bool IsEntryThisState()
    {
        return (_PrevInGameState != _CurrentInGameState);
    }

    /// <summary>
    /// カットインエフェクトを更新する
    /// </summary>
    private void CutInEffectUpdate()
    {
        // 終わっていたら
        if (_FirstCutInUI.IsFinishEffect)
        {
            gameData.ChangeStartViewTransition();
            FirstCutInPanel.SetActive(false);
            return;
        }
    }

    /// <summary>
    /// ステージクリアまで一定時間待つ
    /// </summary>
    /// <returns></returns>
    IEnumerator StayClearUI()
    {
        yield return new WaitForSeconds(clearStayTime);
        _ClearBookUI.DisplayBGImage();
        // ページをめくる演出を開始させる（松島）
        _ClearBookUI.GoToNextPage();
        StageClear();
    }

    // クリアした時の処理
    void StageClear()
    {
        gameData.GoalCompletionTransition();
        clearPanel.SetActive(true);
        nextStageButton.Select();

        if (nowStage.name == "SampleScene")
            return;

        for (int i = 0; i < gameData.stageData.Length; i++)
        {
            if (gameData.stageData[i].sceneName == nowStage.name)
            {
                // クリアしたステージの情報を保存する
                // アイテム機能はまだ入れていないので false を入れるようしている（要変更）
                gameData.ClearStageDataStorage(i, false);
                return;
            }
        }
    }

    /// <summary>
    /// フェードエフェクト処理
    /// </summary>
    void FadeEffect()
    {
        if (fadeState == FadeState.HowToPlay)
        {
            nowEffecting = true;

            if (howToPlayFlag)
            {
                PanelToFade(ref pausePanel, ref howToPlayPanel, ref fadeState, pausePanel, fadeSec, true);
            }
            else
            {
                PanelToFade(ref howToPlayPanel, ref pausePanel, ref fadeState, pausePanel, fadeSec, false);
            }


            if (!howToPlayPanel.activeInHierarchy)
            {
                pausePlayButton.Select();
                HowToPlayEffectReset(ref howToPlayPanel);
            }
        }
        else
        {
            nowEffecting = false;
        }
    }

    /// <summary>
    /// リトライ用コルーチン
    /// </summary>
    private IEnumerator RetryCoroutine()
    {
        yield return new WaitForSeconds(_ClearBookUI.TurnTime);

        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Button function
    // ポーズボタンを押したときの処理
    void PauseActiveChange()
    {
        // プレイ中もしくはポーズ画面中なら処理を行わない
        if (gameData.InGameState != InGame.PlayGame && gameData.InGameState != InGame.Pause)
            return;

        var pauseUIAnim = pausePanel.GetComponent<PauseUIAnimation>();

        // カット処理中なら処理を行わない
        if (!pauseUIAnim.CutOK)
            return;

        // ポーズ画面を表示したときの処理
        if (gameData.InGameState != InGame.Pause)
        {
            gameData.PauseTransition();
            pausePanel.SetActive(true);
            pauseUIAnim.CutOK = false;
            pauseUIAnim.CutInPause();
        }
        else
        {
            pauseUIAnim.CutOK = false;
            pauseUIAnim.CutOutPause();
        }
    }

    // ---------- ポーズ画面中のボタン処理 ---------- //
    public void ClickPlayButton()
    {
        pausePanel.GetComponent<PauseUIAnimation>().CutOutPause();
    }

    public void ClickHowToPlayButton()
    {
        //howToPlayFade = true;
        fadeState = FadeState.HowToPlay;
        howToPlayFlag = true;
        cancelButton.Select();
    }

    public void MoveNextPage()
    {
        if (nowEffecting)
            return;

        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("R_Horizontal") > 0f || Input.GetAxis("D-Pad_Horizontal") > 0f)
        {
            GoNextPageHTP(ref howToPlayPagePanel);
        }
    }

    public void MoveBackPage()
    {
        if (nowEffecting)
            return;

        if (Input.GetAxis("Horizontal") < 0f || Input.GetAxis("R_Horizontal") < 0f || Input.GetAxis("D-Pad_Horizontal") < 0f)
        {
            GoBackPageHTP(ref howToPlayPagePanel);
        }
    }

    public void ClickCancelButton()
    {
        if (nowEffecting)
            return;

        fadeState = FadeState.HowToPlay;
        howToPlayFlag = false;
    }

    //if(_PaperFlip == -_PaperRange)
    // ---------- クリア画面中のボタン処理 ---------- //
    public void ClickNextStageButton()
    {
        // ページめくり演出が終わっていなければ、以下は実行しない（松島）
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            return;
        }

        if (nowStage.name == "SampleScene")
        {
            Time.timeScale = 1f;
            gameData.ChangeCutInTransition();
            SceneManager.LoadScene(nowStage.name);
            return;
        }

        for (int i = 0; i < gameData.stageData.Length; i++)
        {
            if (nowStage.name == gameData.stageData[i].sceneName)
            {
                Time.timeScale = 1f;
                gameData.ChangeCutInTransition();
                SceneManager.LoadScene(gameData.stageData[i + 1].sceneName);
                return;
            }
        }
    }

    // ---------- ポーズ・クリア画面共通のボタン処理 ---------- //
    public void ClickStageSelectButton()
    {
        // ページめくり演出が終わっていなければ、以下は実行しない（松島）
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            return;
        }

        Time.timeScale = 1f;
        gameData.StageSelectTransition();
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// メインメニューへ移動する
    /// ステージクリア時に表示
    /// </summary>
    public void ClickMeinMenuButton()
    {
        // ページめくり演出が終わっていなければ、以下は実行しない（松島）
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            return;
        }

        Time.timeScale = 1f;
        gameData.MeinMenuTransition();
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// 現在のステージに再挑戦する
    /// ステージクリア時に表示
    /// </summary>
    public void ClickRetryButton()
    {
        // ページめくり演出が終わっていなければ、以下は実行しない
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            Debug.Log("wwwwwwwwwwwwwwwwwwww");
            return;
        }

        Time.timeScale = 1f;
        gameData.ChangeCutInTransition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        //_ClearBookUI.GoToNextPage();

        //// リトライコルーチンを起動
        //StartCoroutine(RetryCoroutine());
    }
    #endregion
}
