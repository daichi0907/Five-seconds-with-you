using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController: MonoBehaviour
{
    enum GroundType
    { 
        none = 0,
        sand = 1,
        stone = 2,
        wood = 3,
        water = 4,
    }

    IPlayGimmick playGimmick;

    [SerializeField] AudioSource walkSandSound;
    [SerializeField] AudioSource walkStoneSound;
    [SerializeField] AudioSource walkWoodSound;
    [SerializeField] AudioSource walkWaterSound;
    [SerializeField] AudioSource gimmickButtonSound;
    [SerializeField] AudioSource gimmickPullOrPushSound;
    [SerializeField] AudioSource hintSound;
    [SerializeField] AudioSource warpSound;
    [SerializeField] AudioSource UIChouseSound;
    [SerializeField] AudioSource UISelectSound;
    [SerializeField] AudioSource UITurnPageSound;
 
    [SerializeField]GroundType groundType;

    [SerializeField] bool playWarpSound;
    [SerializeField] bool playUITurnPageSound;
    [SerializeField] bool playUISelectSound;
    [SerializeField] bool playUIChouseSound;

    void PlayWarpSound() { playWarpSound = true; }
    void PlayUITurnPageSound() { playUITurnPageSound = true; }
    void PlayUISelectSound() { playUISelectSound = true; }
    void PlayUIChouseSound() { playUIChouseSound = true; }

    // Start is called before the first frame update
    void Start()
    {
        playGimmick = GameObject.Find("Player").GetComponent<IPlayGimmick>();
    }

    // Update is called once per frame
    void Update()
    {
        //�{�^�����������Ƃ�
        if(playGimmick.ActionTypeP == ActionType.Button )
        {
            gimmickButtonSound.Play();
        }
        //���̂�����
        if (playGimmick.ActionTypeP == ActionType.PushOrPull && !gimmickPullOrPushSound.isPlaying)
        {
            gimmickPullOrPushSound.Play();
        }
        else
        {
            gimmickPullOrPushSound.Stop();
        }

        //���邭
        if (playGimmick.Get_PlayerState() == PlayerState.Move && !CheckIsPlayingWarkSound())
        {
            switch(groundType)
            {
                case GroundType.sand:
                    walkSandSound.Play();
                    break; 
                case GroundType.stone:
                    walkStoneSound.Play();
                    break; 
                case GroundType.water:
                    walkWaterSound.Play();
                    break; 
                case GroundType.wood:
                    walkWoodSound.Play();
                    break;
            }
        }
        else if(playGimmick.Get_PlayerState() == PlayerState.Move)
        {
            walkSandSound.Stop();
            walkStoneSound.Stop();
            walkWaterSound.Stop();
            walkWoodSound.Stop();
        }
        //�Ђ��
        if (playGimmick.Get_PlayerState() == PlayerState.Hint)
        {
            hintSound.Play();
        }
        //��[��
        if(playWarpSound)
        {
            warpSound.Play();
            playWarpSound = false;
        }
        //�y�[�W�߂��鉹
        if(playUITurnPageSound)
        {
            UITurnPageSound.Play();
            playUITurnPageSound = false;
        }
        //UI�I����
        if (playUIChouseSound)
        {
            UIChouseSound.Play();
            playUIChouseSound = false;
        }
        //���ڌ��艹
        if (playUISelectSound)
        {
            UISelectSound.Play();
            playUISelectSound = false;
        }
    }

    //�����Ă��鉹���Đ����Ă��邩
    bool CheckIsPlayingWarkSound()
    {
        bool isPlayingWarkSound = false;

        if (walkSandSound.isPlaying)
            isPlayingWarkSound = true;
        if (walkStoneSound.isPlaying)
            isPlayingWarkSound = true;
        if (walkWaterSound.isPlaying)
            isPlayingWarkSound = true;
        if (walkWoodSound.isPlaying)
            isPlayingWarkSound = true;

        return isPlayingWarkSound;
    }
}
