using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HintButton : MonoBehaviour
{
    /// <summary> ソースを書くときのレンプレート </summary>

    #region define
    private enum ShowHint : int
    {
        Close,
        Open,
    }
    #endregion

    #region serialize field
    //ヒントボタンの画像テーブル
    [SerializeField] private  Sprite[] _Images;
    #endregion

    #region field
    private PlayerBehaviour _Player;

    private Button _Button;
    private AnimatorStateInfo _AnimStateInfo;
    private float _IntervalTime = 0.0f;
    private bool _CanUseButton  = true;

    private bool _PrevCanShowHint;

    // 画像を動的に変えたいボタンの宣言
    private Image _BtnImage;
    #endregion

    #region property

    #endregion

    #region Unity function
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();

        _Button = GetComponent<Button>();
        _AnimStateInfo = _Button.animator.GetCurrentAnimatorStateInfo(0);

        _BtnImage = this.GetComponent<Image>();
        _BtnImage.sprite = _Images[(int)ShowHint.Close];
        _PrevCanShowHint = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();

        UpdateButtonState();
    }
    #endregion

    #region public function

    #endregion

    #region private function
    /// <summary>
    /// ボタンに表示するImageの更新
    /// </summary>
    private void UpdateImage()
    {
        if (!_PrevCanShowHint && HintGimmick._CanShowHint)
        {
            _BtnImage.sprite = _Images[(int)ShowHint.Open];
        }
        else if (_PrevCanShowHint && !HintGimmick._CanShowHint)
        {
            _BtnImage.sprite = _Images[(int)ShowHint.Close];
        }

        _PrevCanShowHint = HintGimmick._CanShowHint;
    }

    /// <summary>
    /// ボタンが押されているかどうか等の、ボタンの状態を更新
    /// </summary>
    private void UpdateButtonState()
    {
        _AnimStateInfo = _Button.animator.GetCurrentAnimatorStateInfo(0);

        if (!_CanUseButton) _IntervalTime += Time.deltaTime;
        if(_IntervalTime > 5.0f)
        {
            _IntervalTime = 0.0f;
            _CanUseButton = true;
        }

        switch (HintGimmick._CanShowHint)
        {
            case true:
                {
                    // ヒントボタンが押されたら
                    if (Input.GetButtonDown("Hint"))
                    {
                        // 既に押されている状態であればリターン
                        if (_AnimStateInfo.IsName("Pressed")) return;
                        // 既にヒントオブジェクトが出願している場合にはリターン
                        if (!_CanUseButton) return;
                        // プレイヤーがアクションボタンを利用できないステートであればリターン
                        if (!(_Player.State == PlayerState.Idle || _Player.State == PlayerState.Move ||  _Player.State == PlayerState.Hint)) return;

                        _Button.animator.SetTrigger("Pressed");
                        _CanUseButton = false;
                        Invoke(nameof(ToNormal_ButtonState), 0.15f);
                    }
                }
                break;
            case false:
                {
                }
                break;
        }
    }

    /// <summary>
    /// Normalステートに変更
    /// </summary>
    private void ToNormal_ButtonState()
    {
        _Button.animator.SetTrigger("Normal");
        _Button.animator.ResetTrigger("Pressed");
    }
    #endregion
}
