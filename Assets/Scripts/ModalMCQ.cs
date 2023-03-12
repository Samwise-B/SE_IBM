using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;
using Random = System.Random;
using System.Linq;
using UnityEngine.SceneManagement;


public class ModalMCQ : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject timer;

    //private GameObject overlay;

    [System.Serializable]
    public class AllQuestionStruct
    {
        public QuestionStruct[] AllQuestions;
    };

    [System.Serializable]
    public class QuestionStruct
    {
        public string Topic;
        public string Question;
        public string CorrectAnswer;
        public string[] OtherAnswers;
    };

    public Canvas canvas;

    public Button Answer1;
    public Button Answer2;
    public Button Answer3;
    public Button Answer4;

    public Button Resource;

    public Button[] Buttons = new Button[4];
    // public Button[] Buttons = {Answer1, Answer2, Answer3, Answer4};

    public TMP_Text question;

    public AllQuestionStruct exampleQuestions;
    public List<QuestionStruct> questionList;

    public string correctAnswer;
    int correctAnswer_idx;

    private int RandomQuestionIndex;

    private string questionTopic;
    private string levelTopic;
    private bool correctTopic;

    public bool correctFlag;

    private string resourceLink;

    // Start is called before the first frame update
    void Awake()
    {
        //Gets the level Topic
        string currentLevel = SceneManager.GetActiveScene().name;
        switch (currentLevel)
        {
            case "Level1":
                {
                    levelTopic = "Cloud";
                    break;
                }
            case "Level2":
                {
                    levelTopic = "Data Science";
                    break;
                }
            default:
                {
                    levelTopic = "Cloud";
                    break;
                }
        }

        // get questions from JSON
        exampleQuestions = JsonUtility.FromJson<AllQuestionStruct>(jsonFile.text);

        // generate list of question structs of the correct topic for the level
        questionList = exampleQuestions.AllQuestions.Where<QuestionStruct>(x => x.Topic == levelTopic).ToList<QuestionStruct>();   
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int selectedIndex;

    // called by the player controller
    public void getQuestion() {
        // pause game time
        Time.timeScale = 0;

        correctFlag = false;
        Buttons[0] = Answer1;
        Buttons[1] = Answer2;
        Buttons[2] = Answer3;
        Buttons[3] = Answer4;

        Random random = new Random();  
        //correctTopic = false;
        /*
        do {
            //Gets random question
            RandomQuestionIndex = random.Next(0, exampleQuestions.AllQuestions.Count());

            //Checks if question is the correct topic
            questionTopic = exampleQuestions.AllQuestions[RandomQuestionIndex].Topic;
            if (questionTopic == levelTopic)
            {
                correctTopic = true;
            }
        } while (correctTopic == false);
        */
        
        RandomQuestionIndex = random.Next(0, questionList.Count());

        // set question text and correct answer
        question.text = questionList[RandomQuestionIndex].Question;
        correctAnswer = questionList[RandomQuestionIndex].CorrectAnswer;
        // generate correct answer index
        correctAnswer_idx = random.Next(0, 4);
        // set correct answer to button
        Buttons[correctAnswer_idx].GetComponentInChildren<TMP_Text>().text = correctAnswer;

        // generate list of answers
        List<string> Answers = new List<string>();
        foreach (string Answer in questionList[RandomQuestionIndex].OtherAnswers){
            Answers.Add(Answer);
        }
        
        // set remaining buttons to other answers
        int i = 0;
        foreach (Button button in Buttons){
            if (i!=correctAnswer_idx){
            int Answer_idx = random.Next(0, Answers.Count());
            string Answer = Answers[Answer_idx];
            Answers.RemoveAt(Answer_idx);
            button.GetComponentInChildren<TMP_Text>().text = Answer;
            }

            i++;
        }
        // remove question from list of questions
        questionList.RemoveAt(RandomQuestionIndex);
    }

    public void ButtonSelection(int _idx) {

        foreach(Button button in Buttons){
            button.GetComponent<Image>().color = Color.white;
        }
        Buttons[_idx].GetComponent<Image>().color = Color.blue;
        selectedIndex = _idx;
    }

    public void Confirm(){
        if(selectedIndex == correctAnswer_idx){
            correctFlag = true;
            Buttons[selectedIndex].GetComponent<Image>().color = Color.green;
            gameObject.SetActive(false);
            // resume game
            Time.timeScale = 1;
        } else {
            Buttons[selectedIndex].GetComponent<Image>().color = Color.red;
            //Time added for incorrect answer - 20 seconds
            timer.GetComponent<StopWatch>().currentTime += 20;

        }
        Buttons[selectedIndex].GetComponent<Image>().color = Color.white;
    }

    public void openResource()
    {
        //Gets the level Topic
        string currentLevel = SceneManager.GetActiveScene().name;
        switch (currentLevel)
        {
            case "Level1":
                {
                    //Cloud
                    resourceLink = "https://www.ibm.com/academic/topic/cloud";
                    break;
                }
            case "Level2":
                {
                    //Data Science
                    resourceLink = "https://www.ibm.com/academic/topic/data-science";
                    break;
                }
            default:
                {
                    //Defaults to cloud
                    resourceLink = "https://www.ibm.com/academic/topic/cloud";
                    break;
                }
        }
        //Opens Url
        Application.OpenURL(resourceLink);
    }

    public void TesterClick(){
        //Debug.Log("Tester Click");
    }
}
