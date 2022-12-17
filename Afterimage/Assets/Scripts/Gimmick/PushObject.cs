using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    GameObject parent, child, player;
    PlayerBehaviour playerController;
    public float power = 3000;
    public bool X;
    float calcX = 0, calcZ = 0;
    bool  canHold;
    Rigidbody parentRb;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        child = transform.GetChild(0).gameObject;
        parentRb = parent.GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerBehaviour>();
        if (X)
        {
            calcX = 1;
        }
        else
        {
            calcZ = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canHold)
        {
            var cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1));
            var moveVec =  cameraForward * Input.GetAxis("Vertical") + mainCamera.transform.right * Input.GetAxis("Horizontal");
            IPlayGimmick playGimmick = player.gameObject.GetComponent<IPlayGimmick>();
            if (playGimmick != null)
            {
                playGimmick.ActionTypeP = ActionType.PushOrPull;
            }

            if (Input.GetButtonDown("Action"))
            {
                playerController.OFF_CharacterController();
                if(X)
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                else 
                    player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
                player.transform.LookAt(new Vector3(parent.transform.position.x, player.transform.position.y, parent.transform.position.z));
                playerController.ON_CharacterController();

            }

            if (Input.GetButton("Action"))
            {
                playerController.OFF_PlayerRotate();
                parentRb.AddForce(new Vector3(calcX * moveVec.x, 0, calcZ * moveVec.z) * power * Time.deltaTime);
                child.SetActive(true);
            }
            else
            {
                playerController.ON_PlayerRotate();
                child.SetActive(false);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
             canHold = true;
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        canHold = false;

        if (other.tag == "Player")
        {
            IPlayGimmick playGimmick = other.gameObject.GetComponent<IPlayGimmick>();
            if (playGimmick != null) playGimmick.ActionTypeP = ActionType.Default;
            playerController.ON_PlayerRotate();
            child.SetActive(false);
        }

    }
}
