using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
public class ComicCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] GameData gameData;
    [Header("����ڂ̖���ł����H(0 Start)")]
    [SerializeField] int comicNum;
    [Header("���̖��悪�I������Ƃ��Ɉړ�����V�[����")]
    [SerializeField] string nextSceneName;
    [Header("����������摜")]
    [SerializeField] GameObject[] changeImg;
    Renderer[] changeImgRenderer = new Renderer[2];
    [Header("����������^�C�~���O")]
    [SerializeField] float[] changeImgTiming;
    [Header("���������鑬�x")]
    [SerializeField] float[] changeImgSpeed;
    [Header("�Ó]�^�C�~���O")]
    [SerializeField] float[] blackoutTiming;
    [Header("�Ó]���鑬�x")]
    [SerializeField] float[] blackoutSpeed;
    [Header("�Ó]����Ƃ��i���]���j�̍ŏI�I�ȓ����x")]
    [SerializeField] float[] blackoutDeep;
    [Header("��{�̃J�����ړ��X�s�[�h")]
    [SerializeField] float defaultIncreaseSpeed = 1;
    [Header("X = startpos, Y = endpos, Z = intervalSpeed")] 
    [SerializeField] Vector3[] pathInterval = new Vector3[1];
    [Header("�I������PathPosition")]
    [SerializeField]float endPos = 14f;
    [Header("�J�����̃��[�e�[�V������OFF�ɂ���^�C�~���O")]
    [SerializeField]float rotationChangeTiming;
    CinemachineTrackedDolly dolly;
    GameObject canvas;
    Image blackScreen;
    float position, intervalIncreaseSpeed, timeCount, waitAfterWatchComic = 3f;
    
    int changingNum, blackOutNum;

    public float GetPathPosition() { return position; }
    // Start is called before the first frame update
    void Start()
    {
        gameData.EditorStart();

        timeCount = 0;
        blackOutNum = 0;
        position = 0;
        intervalIncreaseSpeed = 0;
        changingNum = 0;
        dolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();

        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        for (int i = 0; i < changeImg.Length; i++)
            changeImgRenderer[i] = changeImg[i].GetComponent<Renderer>();

        canvas = GameObject.Find("PressBtoStart");
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�p�X�|�W�V�����̑��������ύX
        bool isOverWrite = false;
        for(int i = 0; i < pathInterval.Length; i++)
            if (pathInterval[i].x <= position && pathInterval[i].y > position)
            {
                intervalIncreaseSpeed = pathInterval[i].z;
                isOverWrite = true;
            }
        
        if (!isOverWrite)
            intervalIncreaseSpeed = 0;

        position += (defaultIncreaseSpeed + intervalIncreaseSpeed) * Time.deltaTime;
        dolly.m_PathPosition = position;

        //�摜�؂�ւ�
        if(changingNum < changeImgTiming.Length)
            if(changeImgTiming[changingNum]  < position)
            {
                changeImgRenderer[changingNum].material.color -= new Color(0, 0, 0, changeImgSpeed[changingNum] * Time.deltaTime);
                if (changeImgRenderer[changingNum].material.color.a <= 0)
                    changingNum++;
            }

        //�u���b�N�X�N���[������
        if (blackOutNum < blackoutTiming.Length)
            if (position > blackoutTiming[blackOutNum])
            {
                blackScreen.color += new Color(0, 0, 0, blackoutSpeed[blackOutNum] * Time.deltaTime);
                if ((blackoutDeep[blackOutNum] <= blackScreen.color.a && blackoutSpeed[blackOutNum] > 0) || (blackoutDeep[blackOutNum] >= blackScreen.color.a && blackoutSpeed[blackOutNum] < 0))
                    blackOutNum++;
            }

        //�J������LookAt�Ώۂ��Ȃ���
        if(position > rotationChangeTiming)
        {
            vcam.LookAt = null;
            vcam.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //�I������
        if(position >= endPos)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= waitAfterWatchComic)
            {
                gameData.ShowStartStoryComp(comicNum);
                gameData.ChangeCutInTransition();
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
