using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;
using Random = System.Random;
using System.Linq;


public class ModalMCQ : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject timer;

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

    public Button[] Buttons = new Button[4];
    // public Button[] Buttons = {Answer1, Answer2, Answer3, Answer4};

    public TMP_Text question;

    public AllQuestionStruct exampleQuestions;

    public string correctAnswer;
    int correctAnswer_idx;

    public bool correctFlag;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("START");
        
        
        //Debug.Log(exampleQuestions.AllQuestions[0].Question);
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int selectedIndex;

    public void getQuestion() {
        // pause game time
        Time.timeScale = 0;

        correctFlag = false;
        Buttons[0] = Answer1;
        Buttons[1] = Answer2;
        Buttons[2] = Answer3;
        Buttons[3] = Answer4;

        // get questions from JSON
        exampleQuestions = JsonUtility.FromJson<AllQuestionStruct>(jsonFile.text);

        Debug.Log("getQuestion");
        Random random = new Random();
        // get random question index
        int RandomQuestionIndex = random.Next(0, exampleQuestions.AllQuestions.Count());

        Debug.Log(exampleQuestions.AllQuestions[RandomQuestionIndex].Question);
        // set question text and correct answer
        question.text = exampleQuestions.AllQuestions[RandomQuestionIndex].Question;
        correctAnswer = exampleQuestions.AllQuestions[RandomQuestionIndex].CorrectAnswer;
        // generate correct answer index
        correctAnswer_idx = random.Next(0, 4);
        // set correct answer to button
        Buttons[correctAnswer_idx].GetComponentInChildren<TMP_Text>().text = correctAnswer;

        // generate list of answers
        List<string> Answers = new List<string>();
        foreach (string Answer in exampleQuestions.AllQuestions[RandomQuestionIndex].OtherAnswers){
            Answers.Add(Answer);
            Debug.Log("Correct answer added");
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
            Debug.Log("Other answers added");

            i++;
        }
    }

    public void ButtonSelection(int _idx) {
        Debug.Log("Button selected");

        foreach(Button button in Buttons){
            button.GetComponent<Image>().color = Color.white;
        }
        Buttons[_idx].GetComponent<Image>().color = Color.blue;
        selectedIndex = _idx;
    }

    public void Confirm(){
        Debug.Log("Confirm Clicked");
        if(selectedIndex == correctAnswer_idx){
            correctFlag = true;
            Buttons[selectedIndex].GetComponent<Image>().color = Color.green;
            // resume game
            Time.timeScale = 1;
        } else {
            Buttons[selectedIndex].GetComponent<Image>().color = Color.red;
            Debug.Log(timer.GetComponent<StopWatch>().currentTime);
            timer.GetComponent<StopWatch>().currentTime += 2;

        }
        Buttons[selectedIndex].GetComponent<Image>().color = Color.white;
    }

    public void TesterClick(){
        Debug.Log("Tester Click");
    }
}
