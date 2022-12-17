using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HintButton : MonoBehaviour
{
    /// <summary> �\�[�X�������Ƃ��̃����v���[�g </summary>

    #region define
    private enum ShowHint : int
    {
        Close,
        Open,
    }
    #endregion

    #region serialize field
    //�q���g�{�^���̉摜�e�[�u��
    [SerializeField] private  Sprite[] _Images;
    #endregion

    #region field
    private PlayerBehaviour _Player;

    private Button _Button;
    private AnimatorStateInfo _AnimStateInfo;
    private float _IntervalTime = 0.0f;
    private bool _CanUseButton  = true;

    private bool _PrevCanShowHint;

    // �摜�𓮓I�ɕς������{�^���̐錾
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
    /// �{�^���ɕ\������Image�̍X�V
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
    /// �{�^����������Ă��邩�ǂ������́A�{�^���̏�Ԃ��X�V
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
                    // �q���g�{�^���������ꂽ��
                    if (Input.GetButtonDown("Hint"))
                    {
                        // ���ɉ�����Ă����Ԃł���΃��^�[��
                        if (_AnimStateInfo.IsName("Pressed")) return;
                        // ���Ƀq���g�I�u�W�F�N�g���o�肵�Ă���ꍇ�ɂ̓��^�[��
                        if (!_CanUseButton) return;
                        // �v���C���[���A�N�V�����{�^���𗘗p�ł��Ȃ��X�e�[�g�ł���΃��^�[��
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
    /// Normal�X�e�[�g�ɕύX
    /// </summary>
    private void ToNormal_ButtonState()
    {
        _Button.animator.SetTrigger("Normal");
        _Button.animator.ResetTrigger("Pressed");
    }
    #endregion
}
