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


    /// <summary> �C���Q�[�����̃f�t�H���g�̃p�l���i�����j </summary>
    private GameObject _DefaultPanel;
    private InGame _CurrentInGameState;
    private InGame _PrevInGameState;

    /// <summary> �N���A��ʕ\���O�̃y�[�W�߂��艉�o�̃p�l���i�����j </summary>
    private BookUI _ClearBookUI;
    private GameObject _WarpEffectObj;
    private UI_WarpEffect _WarpEffect;
    private DirectingScript _DirectingScript;

    /// <summary> Stage�ւ̑J�ڎ��̃y�[�W�߂��艉�o�i�����j </summary>
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

        // ����
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
            // �Q�[���N���A������Q�[�����̉��o������ǉ������ۂ�
            // �|�[�Y��ʂɍs�����ǂ����̏������ύX�̉\����
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick button 7"))
            {
                PauseActiveChange();
            }
        }

        // �ЂƂO�̃C���Q�[���X�e�[�g�Ƃ��ĕۑ�
        _PrevInGameState = _CurrentInGameState;
    }

    #region private function
    /// <summary>
    /// ��Ԃ̕ύX
    /// </summary>
    private void ChangeState()
    {
        // ���O���o��
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
                    ////���o�ǉ��̂��߁A���̎��_�ŃN���A�p�l�����I���ɂ���i�����j
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
    /// ��Ԗ��̖��t���[���Ă΂�鏈��
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
                    // �`�[�g�R�}���h
                    if (Input.GetKey(KeyCode.C) && Input.GetKeyDown(KeyCode.Return))
                    {
                        StageClear();
                    }

                    // �������Ԃ��X�V����
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
                    // �J��������������Ă�����
                    if(_DirectingScript.IsStartWarpEffect && !_WarpEffectObj.activeSelf)
                    {
                        //���o�ǉ��̂��߁A���̎��_�ŃN���A�p�l�����I���ɂ���i�����j
                        clearPanel.SetActive(true);
                        _WarpEffectObj.SetActive(true);
                        // _WarpEffect.StartEffect();
                        StartCoroutine(StayClearUI());
                    }
                }
                break;
            case InGame.GoalCompletion:
                {
                    // �y�[�W���߂��鉉�o���s���i�����j
                    _ClearBookUI.TurnPageUpdate();

                    // �}�E�X���������ۂɁA�i�s�s�\�ɂȂ邱�Ƃ�h���B
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                        _ClearBookUI.ToUseableButton();
                }
                break;
        }
    }

    /// <summary>
    /// ���傤�ǂ��̃X�e�[�g�ɓ����������ǂ���
    /// </summary>
    /// <returns></returns>
    private bool IsEntryThisState()
    {
        return (_PrevInGameState != _CurrentInGameState);
    }

    /// <summary>
    /// �J�b�g�C���G�t�F�N�g���X�V����
    /// </summary>
    private void CutInEffectUpdate()
    {
        // �I����Ă�����
        if (_FirstCutInUI.IsFinishEffect)
        {
            gameData.ChangeStartViewTransition();
            FirstCutInPanel.SetActive(false);
            return;
        }
    }

    /// <summary>
    /// �X�e�[�W�N���A�܂ň�莞�ԑ҂�
    /// </summary>
    /// <returns></returns>
    IEnumerator StayClearUI()
    {
        yield return new WaitForSeconds(clearStayTime);
        _ClearBookUI.DisplayBGImage();
        // �y�[�W���߂��鉉�o���J�n������i�����j
        _ClearBookUI.GoToNextPage();
        StageClear();
    }

    // �N���A�������̏���
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
                // �N���A�����X�e�[�W�̏���ۑ�����
                // �A�C�e���@�\�͂܂�����Ă��Ȃ��̂� false ������悤���Ă���i�v�ύX�j
                gameData.ClearStageDataStorage(i, false);
                return;
            }
        }
    }

    /// <summary>
    /// �t�F�[�h�G�t�F�N�g����
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
    /// ���g���C�p�R���[�`��
    /// </summary>
    private IEnumerator RetryCoroutine()
    {
        yield return new WaitForSeconds(_ClearBookUI.TurnTime);

        // ���݂̃V�[�����ēǂݍ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Button function
    // �|�[�Y�{�^�����������Ƃ��̏���
    void PauseActiveChange()
    {
        // �v���C���������̓|�[�Y��ʒ��Ȃ珈�����s��Ȃ�
        if (gameData.InGameState != InGame.PlayGame && gameData.InGameState != InGame.Pause)
            return;

        var pauseUIAnim = pausePanel.GetComponent<PauseUIAnimation>();

        // �J�b�g�������Ȃ珈�����s��Ȃ�
        if (!pauseUIAnim.CutOK)
            return;

        // �|�[�Y��ʂ�\�������Ƃ��̏���
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

    // ---------- �|�[�Y��ʒ��̃{�^������ ---------- //
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
    // ---------- �N���A��ʒ��̃{�^������ ---------- //
    public void ClickNextStageButton()
    {
        // �y�[�W�߂��艉�o���I����Ă��Ȃ���΁A�ȉ��͎��s���Ȃ��i�����j
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

    // ---------- �|�[�Y�E�N���A��ʋ��ʂ̃{�^������ ---------- //
    public void ClickStageSelectButton()
    {
        // �y�[�W�߂��艉�o���I����Ă��Ȃ���΁A�ȉ��͎��s���Ȃ��i�����j
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            return;
        }

        Time.timeScale = 1f;
        gameData.StageSelectTransition();
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// ���C�����j���[�ֈړ�����
    /// �X�e�[�W�N���A���ɕ\��
    /// </summary>
    public void ClickMeinMenuButton()
    {
        // �y�[�W�߂��艉�o���I����Ă��Ȃ���΁A�ȉ��͎��s���Ȃ��i�����j
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            return;
        }

        Time.timeScale = 1f;
        gameData.MeinMenuTransition();
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// ���݂̃X�e�[�W�ɍĒ��킷��
    /// �X�e�[�W�N���A���ɕ\��
    /// </summary>
    public void ClickRetryButton()
    {
        // �y�[�W�߂��艉�o���I����Ă��Ȃ���΁A�ȉ��͎��s���Ȃ�
        if (gameData.InGameState == InGame.GoalCompletion && !_ClearBookUI.IsFinishEffect)
        {
            Debug.Log("wwwwwwwwwwwwwwwwwwww");
            return;
        }

        Time.timeScale = 1f;
        gameData.ChangeCutInTransition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        //_ClearBookUI.GoToNextPage();

        //// ���g���C�R���[�`�����N��
        //StartCoroutine(RetryCoroutine());
    }
    #endregion
}
