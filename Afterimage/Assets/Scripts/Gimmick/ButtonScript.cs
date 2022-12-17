using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public float pushingtime = 1f;
    public bool isPushing;
    bool isTimeCounting;
    float time;

    GameObject buttonSwitch, pushedButtonSwitch, canPushArea;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        isPushing = false;
        isTimeCounting = false;
        buttonSwitch = transform.GetChild(0).gameObject;
        pushedButtonSwitch = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushing)
        {
            PushButton();
        }
        else
        {
            pushedButtonSwitch.SetActive(false);
            buttonSwitch.SetActive(true);
        }
        if (isTimeCounting)
            time += Time.deltaTime;

        if (time >= pushingtime)
        {
            isTimeCounting = false;
            isPushing = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerState = other.GetComponent<PlayerBehaviour>().State;
            IPlayGimmick playGimmick = other.gameObject.GetComponent<IPlayGimmick>();
            if (playGimmick != null) playGimmick.ActionTypeP = ActionType.Button;
            //if (playGimmick != null) playGimmick.Set_ActionType(ActionType.Torch);   // デバッグ用消してよし
            if (playerState == PlayerState.Action)
            {
                isPushing = true;
                time = 0;
            }
            //if (Input.GetButtonDown("Action"))
            //{
            //    isPushing = true;
            //    time = 0;
            //}
        }

        if (other.tag == "Afterimage")
        {
            if (other.GetComponent<AfterimageBehaviour>().AfterimageState == PlayerState.Action)
            {
                isPushing = true;
                time = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IPlayGimmick playGimmick = other.gameObject.GetComponent<IPlayGimmick>();
            if (playGimmick != null) playGimmick.ActionTypeP = ActionType.Default;
        }
    }

    void PushButton()
    {
        pushedButtonSwitch.SetActive(true);
        buttonSwitch.SetActive(false);
        isTimeCounting = true;
    }
}
