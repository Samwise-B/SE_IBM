using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter Called");
        if (other.CompareTag("Player")) {
            triggerActive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exit Called");
        if (other.CompareTag("Player")) {
            triggerActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E)) {
            gameObject.SetActive(false);
        }
    }


}
