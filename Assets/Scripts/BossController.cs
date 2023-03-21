using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool triggerActive;
    private int questionCount = 0;

    public GameObject overlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive) {
            // activate question overlay
            overlay.SetActive(true);
            
            // get question for overlay
            overlay.GetComponent<ModalMCQ>().getQuestion();

            // disable trigger after MCQ display
            triggerActive = false;
        }
        
        if (overlay.GetComponent<ModalMCQ>().correctFlag) {
            // reset the correct flag
            overlay.GetComponent<ModalMCQ>().correctFlag = false;

            if (questionCount == 4) {
                gameObject.SetActive(false);
            }
            triggerActive = true;
            questionCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        triggerActive = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        triggerActive = false;
    }
}
