using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionButton : MonoBehaviour
{
    /// <summary> �\�[�X�������Ƃ��̃����v���[�g </summary>

    #region define

    #endregion

    #region serialize field
    /// <summary> �A�N�V�����{�^���̉摜�e�[�u�� </summary>
    [SerializeField] private Sprite[] _Images;
    #endregion

    #region field
    private PlayerBehaviour _Player;
    private ActionType _PrevActionType;

    private Button _Button;
    private AnimatorStateInfo _AnimStateInfo;
    private bool _IsPushed = false;

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
        _PrevActionType = ActionType.Default;

        _Button = GetComponent<Button>();
        _AnimStateInfo = _Button.animator.GetCurrentAnimatorStateInfo(0);

        _BtnImage = this.GetComponent<Image>();
        _BtnImage.sprite = _Images[(int)ActionType.Default];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();

        UpdateButtonState();
    }
    #endregion

    #region public function
    public void DebugMessage()
    {
        Debug.Log("Action : " + _Player.ActionType);
    }
    #endregion

    #region private function
    /// <summary>
    /// �{�^���ɕ\������Image�̍X�V
    /// </summary>
    private void UpdateImage()
    {
        // �A�N�V�����^�C�v���ς���Ă�����
        if (_PrevActionType != _Player.ActionType)
        {
            _BtnImage.sprite = _Images[(int)_Player.ActionType];
            _PrevActionType = _Player.ActionType;   // �ЂƂO�̃A�N�V�����^�C�v��ۑ�
        }
    }

    /// <summary>
    /// �{�^����������Ă��邩�ǂ������́A�{�^���̏�Ԃ��X�V
    /// </summary>
    private void UpdateButtonState()
    {
        _AnimStateInfo = _Button.animator.GetCurrentAnimatorStateInfo(0);

        switch(_Player.ActionType)
        {
            case ActionType.Default:
            case ActionType.Button:
            case ActionType.Torch:
                {
                    if(!_IsPushed && _AnimStateInfo.IsName("Pressed")) _Button.animator.SetTrigger("Normal");

                    // �A�N�V�����{�^���������ꂽ��
                    if (Input.GetButtonDown("Action"))
                    {
                        // ���ɉ�����Ă����Ԃł���΃��^�[��
                        if (_AnimStateInfo.IsName("Pressed")) return;
                        // �v���C���[���A�N�V�����{�^���𗘗p�ł��Ȃ��X�e�[�g�ł���΃��^�[��
                        if (!(_Player.State == PlayerState.Idle || _Player.State == PlayerState.Move ||  _Player.State == PlayerState.Action)) return;

                        _IsPushed = true;
                        _Button.animator.ResetTrigger("Normal");
                        _Button.animator.SetTrigger("Pressed");
                        Invoke(nameof(ToNormal_ButtonState), 0.15f);
                    }
                }
                break;
            case ActionType.PushOrPull:
                {
                    // �������Ή�
                    if (Input.GetButtonDown("Action")) { _Button.animator.SetTrigger("Pressed"); _IsPushed = true; }
                    if (Input.GetButtonUp("Action"))   { _Button.animator.SetTrigger("Normal"); _IsPushed = false; }
                }
                break;
        }
    }

    /// <summary>
    /// Normal�X�e�[�g�ɕύX
    /// </summary>
    private void ToNormal_ButtonState()
    {
        _IsPushed = false;
        _Button.animator.SetTrigger("Normal");
        _Button.animator.ResetTrigger("Pressed");
    }
    #endregion
}
