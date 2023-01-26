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

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button[] Buttons = {button1,button2,button3,button4};


    public TMP_Text question;


    public string correctAnswer;
    int correctAnswer_idx;

    // Start is called before the first frame update
    void Start()
    {

        AllQuestionStruct exampleQuestions = JsonUtility.FromJson<AllQuestionStruct>(jsonFile.text);

        // foreach (QuestionStruct question in exampleQuestions.AllQuestions){
        //     Debug.Log(question.CorrectAnswer);
        // }

        Random random = new Random();

        // Debug.Log(exampleQuestions.AllQuestions.Count());

        int RandomQuestionIndex = random.Next(0, exampleQuestions.AllQuestions.Count());
        // int RandomQuestionIndex = 6;


        question.text = exampleQuestions.AllQuestions[RandomQuestionIndex].Question;
        correctAnswer = exampleQuestions.AllQuestions[RandomQuestionIndex].CorrectAnswer;



        correctAnswer_idx = random.Next(0, 4);
        Buttons[correctAnswer_idx].GetComponentInChildren<TMP_Text>().text = correctAnswer;

        List<string> Answers = new List<string>();

        foreach (string Answer in exampleQuestions.AllQuestions[RandomQuestionIndex].OtherAnswers){
            Answers.Add(Answer);
        }
        

        int i = 0;
        foreach (Button button in Buttons){

            if (i!=correctAnswer_idx){
            int Answer_idx = random.Next(0, Answers.Count());
            string Answer = Answers[Answer_idx];
            Answers.RemoveAt(Answer_idx);
            button.GetComponentInChildren<TMP_Text>().text = Answer;
            }
            var colors = GetComponent<Button> ().colors;
            colors.normalColor = Color.red;
            GetComponent<Button> ().colors = colors;
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int selectedAnswer;
    public void ButtonSelection(int _idx) {
        // selectedAnswer = _idx;
        // Buttons[_idx].colors = Color.red;
        // // var colors = GetComponent<Button> ().colors;
        // // colors.normalColor = Color.red;
        // // button.colors = Color.red;
        
    }

    public void Confirm(){
        if(selectedAnswer == correctAnswer_idx){
            Debug.Log("YAYAYAY");
        } else {
            Debug.Log("nope");
        }
    }
}
