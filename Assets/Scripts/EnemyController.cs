using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public GameObject overlay;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter Called");
        if (other.CompareTag("Player")) {
            triggerActive = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //Debug.Log(triggerActive);
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exit Called");
        if (other.CompareTag("Player")) {
            triggerActive = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive) {
            // pause game time
            Time.timeScale = 0;
            Debug.Log("get question");
            // get question for overlay
            overlay.GetComponent<ModalMCQ>().getQuestion();
            // activate question overlay
            overlay.SetActive(true);
        }
        // when correctflag is true, close overlay
        if (overlay.GetComponent<ModalMCQ>().correctFlag) {
                // resume game
                Time.timeScale = 1;
                // hide enemy
                gameObject.SetActive(false);
                // hide overlay
                overlay.SetActive(false);
                // reset overlay correct flag
                overlay.GetComponent<ModalMCQ>().correctFlag = false;
            }
    }
}
