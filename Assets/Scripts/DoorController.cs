using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public GameObject overlay;
    private GameObject[] doors;
    public ModalMCQ overlayScript;
    //private int doorCount = 0;
    //public OtherScript overlayScript = ;

    void Start() {
        
    }

/*
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter Called");
        if (other.CompareTag("Player")) {
            triggerActive = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //Debug.Log("triggered"+ triggerActive.ToString());
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("get question");
            // get question for overlay
            overlay.GetComponent<ModalMCQ>().getQuestion();
            // activate question overlay
            overlay.SetActive(true);
        }
        // when correctflag is true, close overlay
        if (overlay.GetComponent<ModalMCQ>().correctFlag) {
            overlay.SetActive(false);
            //Debug.Log(overlay.GetComponent<ModalMCQ>().correctFlag);
            doors = GameObject.FindGameObjectsWithTag("doorCollision");
            //Debug.Log(doors.Length);
            // set door object to false
            doors[0].SetActive(false);
            //overlay.GetComponent<ModalMCQ>().correctFlag = false;
            //doorCount++;
            overlay.GetComponent<ModalMCQ>().correctFlag = false;
            //Debug.Log(overlay.GetComponent<ModalMCQ>().correctFlag);
            //Debug.Log(doorCount);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exit Called");
        if (other.CompareTag("Player")) {
            triggerActive = false;
        }
    }
*/

    // Update is called once per frame
    void Update()
    {   

    }


}
