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
    [Header("何回目の漫画ですか？(0 Start)")]
    [SerializeField] int comicNum;
    [Header("この漫画が終わったときに移動するシーン名")]
    [SerializeField] string nextSceneName;
    [Header("透明化する画像")]
    [SerializeField] GameObject[] changeImg;
    Renderer[] changeImgRenderer = new Renderer[2];
    [Header("透明化するタイミング")]
    [SerializeField] float[] changeImgTiming;
    [Header("透明化する速度")]
    [SerializeField] float[] changeImgSpeed;
    [Header("暗転タイミング")]
    [SerializeField] float[] blackoutTiming;
    [Header("暗転する速度")]
    [SerializeField] float[] blackoutSpeed;
    [Header("暗転するとき（明転も）の最終的な透明度")]
    [SerializeField] float[] blackoutDeep;
    [Header("基本のカメラ移動スピード")]
    [SerializeField] float defaultIncreaseSpeed = 1;
    [Header("X = startpos, Y = endpos, Z = intervalSpeed")] 
    [SerializeField] Vector3[] pathInterval = new Vector3[1];
    [Header("終了時のPathPosition")]
    [SerializeField]float endPos = 14f;
    [Header("カメラのローテーションをOFFにするタイミング")]
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
        //パスポジションの増加割合変更
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

        //画像切り替え
        if(changingNum < changeImgTiming.Length)
            if(changeImgTiming[changingNum]  < position)
            {
                changeImgRenderer[changingNum].material.color -= new Color(0, 0, 0, changeImgSpeed[changingNum] * Time.deltaTime);
                if (changeImgRenderer[changingNum].material.color.a <= 0)
                    changingNum++;
            }

        //ブラックスクリーン調整
        if (blackOutNum < blackoutTiming.Length)
            if (position > blackoutTiming[blackOutNum])
            {
                blackScreen.color += new Color(0, 0, 0, blackoutSpeed[blackOutNum] * Time.deltaTime);
                if ((blackoutDeep[blackOutNum] <= blackScreen.color.a && blackoutSpeed[blackOutNum] > 0) || (blackoutDeep[blackOutNum] >= blackScreen.color.a && blackoutSpeed[blackOutNum] < 0))
                    blackOutNum++;
            }

        //カメラのLookAt対象をなくす
        if(position > rotationChangeTiming)
        {
            vcam.LookAt = null;
            vcam.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //終了処理
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
