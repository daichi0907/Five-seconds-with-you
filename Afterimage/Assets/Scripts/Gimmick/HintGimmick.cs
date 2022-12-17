using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class HintGimmick : MonoBehaviour
{
    /// <summary> �\�[�X�������Ƃ��̃����v���[�g </summary>

    #region define
    private enum HintViewEdge : int
    {
        PlayerSide,
        HintObjSide
    }
    #endregion

    #region serialize field
    [SerializeField] private Transform _SpawnPoint;
    [SerializeField] private GameObject _HintObject;

    [SerializeField] private CinemachineVirtualCamera _HintVcamera;

    [SerializeField] private GameObject[] _HintViewEdge;

    [Header("�q���g��p�̈�����𒲐�")]
    [SerializeField, Range(0, 10)] private float _EdgeDistance = 5.0f;
    #endregion

    #region field
    private Transform _PlayerTransform;

    private GameObject _HintBody;

    public static bool _CanShowHint;
    public static bool _IsAliveHint;
    #endregion

    #region property

    #endregion

    #region Unity function
    // Start is called before the first frame update
    void Start()
    {
        _CanShowHint = false;
        _IsAliveHint = false;
        _HintBody = _HintObject.transform.GetChild(0).gameObject;
        _HintBody.SetActive(false);

        _PlayerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Hint"))
        {
            GenerateHintObj();
        }

        UpdateHintViewEdge();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _CanShowHint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _CanShowHint = false;
        }
    }
    #endregion

    #region public function

    #endregion

    #region private function
    /// <summary>
    /// �q���g�p�I�u�W�F�N�g��\������
    /// </summary>
    private void GenerateHintObj()
    {
        if (!_CanShowHint) return;
        if (_IsAliveHint) return;

        _HintBody.SetActive(true);
        _HintObject.transform.position = _SpawnPoint.transform.position;
        _IsAliveHint = true;
        _HintVcamera.Priority += 10;

        StartCoroutine(DelayCoroutine());
    }

    /// <summary>
    /// �q���g�I�u�W�F�N�g�𐶐����Ă���5�b��ɁA��\���ɂ���
    /// </summary>
    private IEnumerator DelayCoroutine()
    {
        // 5�b�ԑ҂�
        yield return new WaitForSeconds(5);

        // 5�b��Ƀq���g�I�u�W�F�N�g������
        _IsAliveHint = false;
        _HintBody.SetActive(false);
        _HintVcamera.Priority -= 10;
    }

    /// <summary>
    /// �q���g��p�̒[���X�V
    /// </summary>
    private void UpdateHintViewEdge()
    {
        _HintViewEdge[(int)HintViewEdge.PlayerSide].transform.position = _PlayerTransform.position;
        _HintViewEdge[(int)HintViewEdge.HintObjSide].transform.position = _HintObject.transform.position;

        Vector3 gapN = (_PlayerTransform.position - _HintObject.transform.position).normalized;

        _HintViewEdge[(int)HintViewEdge.PlayerSide].transform.position += (gapN * _EdgeDistance);
        _HintViewEdge[(int)HintViewEdge.HintObjSide].transform.position -= (gapN * _EdgeDistance);
    }
    #endregion
}
