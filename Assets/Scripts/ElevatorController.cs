using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    private string currentScene;
    private string nextScene;
    private string[] allScenes = { "Menu", "Level1", "Level2", "Level6"};

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
            currentScene = SceneManager.GetActiveScene().name;
            print(currentScene);
            var currentSceneIndex = Array.FindIndex(allScenes, x => x.Contains(currentScene));
            print(currentSceneIndex);
            nextScene = allScenes[currentSceneIndex + 1];
            print(nextScene);
            SceneManager.LoadScene(nextScene);
            Debug.Log("Loading next scene");
        }
    }
}