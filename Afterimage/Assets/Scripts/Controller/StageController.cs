using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    #region serialize field
    /// <summary> �O������擾����I�u�W�F�N�g���܂Ƃ߂Ă��� </summary>
    [SerializeField] public GameObject _Player;
    [SerializeField] public GameObject _Afterimage;

    /// <summary> �c���̍��x���� </summary>
    [SerializeField] private GameObject _HighestPoint;
    #endregion

    #region field
    private RideSencor _RideSencor;
    #endregion

    #region property
    public Transform _PlayerTransform { get; private set; }
    public Transform _AfterimageTransform { get; private set; }

    public RideSencor RideSencor_ { get { return _RideSencor; } }

    public float _HighestPosition { get; private set; }
    #endregion

    #region Unity function
    private void Start()
    {
        _PlayerTransform = _Player.GetComponent<Transform>();
        _AfterimageTransform = _Afterimage.GetComponent<Transform>();

        _RideSencor = _Afterimage.transform.GetChild(1).gameObject.GetComponent<RideSencor>();

        _HighestPosition = _HighestPoint.GetComponent<Transform>().position.y;
    }
    #endregion
}
