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
        //ボタンを押したとき
        if(playGimmick.ActionTypeP == ActionType.Button )
        {
            gimmickButtonSound.Play();
        }
        //ものを押す
        if (playGimmick.ActionTypeP == ActionType.PushOrPull && !gimmickPullOrPushSound.isPlaying)
        {
            gimmickPullOrPushSound.Play();
        }
        else
        {
            gimmickPullOrPushSound.Stop();
        }

        //あるく
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
        //ひんと
        if (playGimmick.Get_PlayerState() == PlayerState.Hint)
        {
            hintSound.Play();
        }
        //わーぷ
        if(playWarpSound)
        {
            warpSound.Play();
            playWarpSound = false;
        }
        //ページめくる音
        if(playUITurnPageSound)
        {
            UITurnPageSound.Play();
            playUITurnPageSound = false;
        }
        //UI選択時
        if (playUIChouseSound)
        {
            UIChouseSound.Play();
            playUIChouseSound = false;
        }
        //項目決定音
        if (playUISelectSound)
        {
            UISelectSound.Play();
            playUISelectSound = false;
        }
    }

    //歩いている音を再生しているか
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
