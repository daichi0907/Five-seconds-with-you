using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using State = StateMachine<PlayerBehaviour>.State;

#region define
/// <summary>
/// ���N���X����v���C���[�̃X�e�[�g���擾���邽�߂�enum
/// </summary>
public enum PlayerState : int
{
    Idle,
    Move,
    Action,
    Action_PushOrPll,
    Ride,
    Hint,
    Dead
}

/// <summary>
/// �v���C���[�ɃZ�b�g����Ă���A�N�V�����^�C�v
/// �A�N�V�����{�^���������ƁA�Z�b�g���ꂽ�^�C�v�̃A�N�V�������s��
/// </summary>
public enum ActionType : int
{
    Default,
    Button,
    Torch,
    PushOrPull
}
#endregion


/// <summary>
/// �X�e�[�g�Ǘ��p�B
/// </summary>
public partial class PlayerBehaviour
{
    #region define
    /// <summary> �X�e�[�g�}�V�[���̃g�����W�V�����p�̃C�x���g�L�[ </summary>
    private enum Event : int
    {
        // �ړ��J�n
        StartMove,
        // �ړ����~
        StopMove,

        // �U���J�n
        StartAction,
        // �U���I��
        FinishAction,

        // �����E�����J�n
        StartAction_PushOrPll,
        // �����E�����I��
        FinishAction_PushOrPll,

        // �q���g�J�n
        StartHint,
        // �q���g�I��
        FinishHint,

        // ���J�n
        StartRide,
        // ��蒆�~
        StopRide,

        // ���S
        Dead,
    }
    #endregion

    #region field
    /// <summary> �v���C���[�����Ǘ��̂��߂̃X�e�[�g�}�V�[�� </summary>
    private StateMachine<PlayerBehaviour> _StateMachine;

    /// <summary> �ǂ̃X�e�[�g��������enum </summary>
    private PlayerState _State;

    /// <summary> �Z�b�g����Ă���A�N�V�����^�C�v������enum </summary>
    private ActionType _ActionType;
    #endregion

    #region property
    /// <summary> �O������v���C���[�̌��݂̏�Ԃ��擾���� </summary>
    public PlayerState State { get { return _State; } }

    /// <summary> �O������Z�b�g���ꂽ�A�N�V�����^�C�v���擾���� </summary>
    public ActionType ActionType { get { return _ActionType; } }
    #endregion

    #region private function
    /// <summary>
    /// �X�e�[�g�}�V���̐ݒ���s���iStart���\�b�h�ŌĂяo���悤�j
    /// </summary>
    private void SetUpStateMachine()
    {
        _StateMachine = new StateMachine<PlayerBehaviour>(this);

        // �iIdel��Move�j
        _StateMachine.AddTransition<StateIdle, StateMove>((int)Event.StartMove);
        // �iMove��Idel�j
        _StateMachine.AddTransition<StateMove, StateIdle>((int)Event.StopMove);

        // �iIdel��Action�j�w��̃{�^���������ꂽ��A�N�V�����J�n
        _StateMachine.AddTransition<StateIdle, StateAction>((int)Event.StartAction);
        // �iMove��Action�j�w��̃{�^���������ꂽ��A�N�V�����J�n
        _StateMachine.AddTransition<StateMove, StateAction>((int)Event.StartAction);

        // �iAction��Move�j�U�����[�V�������I������A�ҋ@�ɖ߂�
        _StateMachine.AddTransition<StateAction, StateIdle>((int)Event.FinishAction);

        // �iAction��Action_PushOrPll�j
        _StateMachine.AddTransition<StateAction, StateAction_PushOrPull>((int)Event.StartAction_PushOrPll);
        // �iAction_PushOrPll��Idle�j
        _StateMachine.AddTransition<StateAction_PushOrPull, StateIdle>((int)Event.FinishAction_PushOrPll);

        // �iIdel��Hint�j�w��̃{�^���������ꂽ��q���g�J�n�iIdel��Hint�j
        _StateMachine.AddTransition<StateIdle, StateHint>((int)Event.StartHint);
        // �iMove��Hint�j�w��̃{�^���������ꂽ��q���g�J�n�iMove��Hint�j
        _StateMachine.AddTransition<StateMove, StateHint>((int)Event.StartHint);

        // �iHint��Idel�j�q���g�\�����I������A�ҋ@�ɖ߂�
        _StateMachine.AddTransition<StateHint, StateIdle>((int)Event.FinishHint);

        // �iIdel��Ride�j
        _StateMachine.AddTransition<StateIdle, StateRide>((int)Event.StartRide);
        // �iRide��Move�j
        _StateMachine.AddTransition<StateRide, StateMove>((int)Event.StopRide);

        // ���S
        _StateMachine.AddAnyTransition<StateDead>((int)Event.Dead);

        _StateMachine.Start<StateIdle>();
        _State = PlayerState.Idle;
    }
    #endregion


    #region StateIdle class
    /// <summary>
    /// �ҋ@
    /// </summary>
    private class StateIdle : State
    {
        public StateIdle()
        {
            _name = "Idle";
        }

        protected override void OnEnter(State prevState)
        {
            //Debug.Log("Enter " + Name);
            Owner._State = PlayerState.Idle;
            Owner._Animator.ResetTrigger("ToMove");
        }

        protected override void OnUpdate()
        {
            if (Owner._StageManager.RideSencor_.CanRiding)
            {
                StateMachine.Dispatch((int)Event.StartRide);
                return;
            }

            if (Input.GetButtonDown("Action"))
            {
                StateMachine.Dispatch((int)Event.StartAction);
                return;
            }

            if (!HintGimmick._IsAliveHint) Owner.MovePlayer();
            Owner._Animator.SetFloat("DeltaTime", Owner._DeltaTime);

            // L�X�e�B�b�N����莞�Ԉȏ�X����ꂽ�ꍇ�AMove�X�e�[�g�Ɉړ�
            if (Owner._DeltaTime >= 0.1f) StateMachine.Dispatch((int)Event.StartMove);

            if (HintGimmick._IsAliveHint && Input.GetButtonDown("Hint"))
                StateMachine.Dispatch((int)Event.StartHint);

            // ���C���J�����̃t�H�[�J�X��𒲐�����
            Owner.UpdateLensVerticalFOV();

            //if (!HintController.IsAliveHint && Owner._deltaTime >= 0.1f) StateMachine.Dispatch((int)Event.StartMove);
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
        }
    }
    #endregion

    #region StateMove class
    /// <summary>
    /// �ړ�
    /// </summary>
    private class StateMove : State
    {
        public StateMove()
        {
            _name = "Move";
        }

        protected override void OnEnter(State prevState)
        {
            //Debug.Log("Enter " + Name);
            Owner._State = PlayerState.Move;
            //Owner._animator.ResetTrigger("ToRide");
        }

        protected override void OnUpdate()
        {
            // ��q��胂�[�V�����ɐ؂�ւ��邩�ǂ���
            JudgeClimbLadder();

            if (Input.GetButtonDown("Action"))
            {
                StateMachine.Dispatch((int)Event.StartAction);
                return;
            }

            Owner.MovePlayer();
            //Owner._animator.SetFloat("DeltaTime", Owner._deltaTime);

            if (Owner._DeltaTime < 0.1f) StateMachine.Dispatch((int)Event.StopMove);

            if (HintGimmick._IsAliveHint && Input.GetButtonDown("Hint"))
                StateMachine.Dispatch((int)Event.StartHint);

            // ���C���J�����̃t�H�[�J�X��𒲐�����
            Owner.UpdateLensVerticalFOV();
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
            Owner._Animator.ResetTrigger("ToRide");
        }

        /// <summary>
        /// ��q������Ă��邩�ǂ������肷��
        /// ���ʂɉ����āA���[�V������؂�ւ���
        /// </summary>
        private void JudgeClimbLadder()
        {
            // ��q�ɏ��n�߂��烂�[�V������؂�ւ���
            if (!Owner._Animator.GetBool("IsLadder") && Owner._IsLadder)
                Owner._Animator.SetBool("IsLadder", true);
            // ��q����~�肽�烂�[�V������؂�ւ���
            else if (Owner._Animator.GetBool("IsLadder") && !Owner._IsLadder)
                Owner._Animator.SetBool("IsLadder", false);
        }
    }
    #endregion

    #region StateAction class
    /// <summary>
    /// �A�N�V����
    /// </summary>
    private class StateAction : State
    {
        ActionType action;

        public StateAction()
        {
            _name = "Action";
        }

        protected override void OnEnter(State prevState)
        {
            action = ActionType.Default;
            Owner._Animator.SetTrigger("ToAction");
            switch (Owner._ActionType)
            {
                case ActionType.Default:
                    action = ActionType.Default;
                    break;
                case ActionType.Button:
                    action = ActionType.Button;
                    break;
                case ActionType.Torch:
                    action = ActionType.Torch;
                    break;
                case ActionType.PushOrPull:
                    action = ActionType.PushOrPull;
                    break;
                default:
                    action = ActionType.Default;
                    Debug.Log("�s���̎��� in Actoin�X�e�[�g");
                    break;
            }
            Owner._Animator.SetInteger("ActionType", (int)action);
            Owner._State = PlayerState.Action;
        }

        protected override void OnUpdate()
        {
            if (action == ActionType.PushOrPull)
            {
                StateMachine.Dispatch((int)Event.StartAction_PushOrPll);
                return;
            }

            if (!Owner._CharaCon.isGrounded)
            {
                Owner._CharaCon.Move(new Vector3(0, Physics.gravity.y * Time.deltaTime, 0));
            }
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
            //Owner._actionType = ActionType.Default;
        }
    }

    /// <summary>
    /// �A�N�V�����̃��[�V�������I��������AStateAttack����StateIdel�֑J�ڂ���B
    /// �A�N�V�����̃A�j���[�V�����̎w�肵���N���b�v����Ăяo��
    /// </summary>
    public void OnAttackFinish()
    {
        _StateMachine.Dispatch((int)Event.FinishAction);
    }
    #endregion

    #region StateAction_PushOrPull class
    /// <summary>
    /// �����E�����A�N�V����
    /// </summary>
    private class StateAction_PushOrPull : State
    {
        float time = 0.0f;

        public StateAction_PushOrPull()
        {
            _name = "Action_PushOrPull";
        }

        protected override void OnEnter(State prevState)
        {
            Owner._State = PlayerState.Action_PushOrPll;
        }

        protected override void OnUpdate()
        {
            if (Owner._Velocity.magnitude == 0.0f)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0.0f;
            }

            Owner.MovePlayer();
            if (Input.GetButtonUp("Action")
                || Owner._ActionType != ActionType.PushOrPull
                || time > 1.0f)
            {
                StateMachine.Dispatch((int)Event.FinishAction_PushOrPll);
            }

        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
            //Owner._actionType = ActionType.Default;
            Owner._Animator.SetTrigger("ToIdle");
        }
    }
    #endregion

    #region StateRide class
    /// <summary>
    /// ���
    /// </summary>
    private class StateRide : State
    {
        private Vector3 ridePosition;
        private float inputTime = 0.0f;

        public StateRide()
        {
            _name = "Ride";
        }

        protected override void OnEnter(State prevState)
        {
            //Debug.Log("Enter " + Name);
            Owner._Animator.SetTrigger("ToRide");
            Owner._State = PlayerState.Ride;

            SetRidePosition();

            Owner.transform.position = ridePosition;

            Owner._CharaCon.enabled = false;
        }

        protected override void OnUpdate()
        {
            if (Owner._Velocity.magnitude > 0.2f)
            {
                inputTime += Time.deltaTime;
                if (inputTime > 0.001f)
                {
                    inputTime = 0.0f;
                    StateMachine.Dispatch((int)Event.StopRide);
                    return;
                }
            }
            else
            {
                inputTime = 0.0f;
            }

            //if (Owner._velocity.magnitude > 0.1f)
            //{
            //    StateMachine.Dispatch((int)Event.StopRide);
            //    return;
            //}

            SetRidePosition();
            Owner.transform.position = ridePosition;
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
            Owner._CharaCon.enabled = true;
            Owner._Animator.SetTrigger("ToMove");
        }

        private void SetRidePosition()
        {
            ridePosition.x = Owner._StageManager._AfterimageTransform.position.x;
            ridePosition.y = Owner._StageManager._AfterimageTransform.position.y + 2.1f;
            ridePosition.z = Owner._StageManager._AfterimageTransform.position.z;
        }
    }
    #endregion

    #region StateHint class
    /// <summary>
    /// �q���g������
    /// </summary>
    private class StateHint : State
    {
        public StateHint()
        {
            _name = "Hint";
        }

        protected override void OnEnter(State prevState)
        {
            Owner._State = PlayerState.Hint;
            Owner._Animator.SetFloat("DeltaTime", 0.0f);
            Owner._Animator.SetFloat("MoveBlend", 0.0f);
        }

        protected override void OnUpdate()
        {
            if (!HintGimmick._IsAliveHint) StateMachine.Dispatch((int)Event.FinishHint);

            // ���C���J�����̃t�H�[�J�X��𒲐�����
            Owner.UpdateLensVerticalFOV();
        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
        }
    }
    #endregion

    #region StateDead class
    //--- ���S ---//
    private class StateDead : State
    {
        public StateDead()
        {
            _name = "Dead";
        }

        protected override void OnEnter(State prevState)
        {
            Owner._State = PlayerState.Dead;
            Owner._Animator.SetTrigger("To" + Name);
            //Debug.Log("Enter " + Name);

            Owner._IsAlive = false;
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnExit(State nextState)
        {
            //Debug.Log("Exit " + Name);
        }
    }
    #endregion
}
