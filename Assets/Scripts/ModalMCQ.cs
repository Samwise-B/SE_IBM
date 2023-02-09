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

    public Button Answer1;
    public Button Answer2;
    public Button Answer3;
    public Button Answer4;

    public Button[] Buttons = new Button[4];
    // public Button[] Buttons = {Answer1, Answer2, Answer3, Answer4};

    public TMP_Text question;

    public string correctAnswer;
    int correctAnswer_idx;

    // Start is called before the first frame update
    void Start()
    {
        Buttons[0] = Answer1;
        Buttons[1] = Answer2;
        Buttons[2] = Answer3;
        Buttons[3] = Answer4;
        
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
        // Debug.Log(correctAnswer_idx);
        // Debug.Log(Buttons[correctAnswer_idx].GetComponentInChildren<TMP_Text>().text);

        Buttons[correctAnswer_idx].GetComponentInChildren<TMP_Text>().text = correctAnswer;
        // Answer4.GetComponentInChildren<TMP_Text>().text = "correctAnswer";

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

            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public int selectedIndex;

    public void ButtonSelection(int _idx) {
        foreach(Button button in Buttons){
            button.GetComponent<Image>().color = Color.white;
        }
        Buttons[_idx].GetComponent<Image>().color = Color.blue;
        selectedIndex = _idx;
    }

    public void Confirm(){
        if(selectedIndex == correctAnswer_idx){
            Buttons[selectedIndex].GetComponent<Image>().color = Color.green;
        } else {
            Buttons[selectedIndex].GetComponent<Image>().color = Color.red;
        }
    }
}
