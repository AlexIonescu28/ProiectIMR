using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfilingFormController : MonoBehaviour
{
    [System.Serializable]
    public struct Question
    {
        public string questionText;
        public string[] answers;
    }

    [Header("Monitor & Panouri")]
    public GameObject powerLED;
    public Collider powerButtonCollider;
    public GameObject screenOffPanel;
    public GameObject formPanel;
    public GameObject finishPanel;

    [Header("Elemente Formular")]
    public TMP_Text questionTextUI;
    public Button[] answerButtons;
    public TMP_Text[] answerButtonTexts;
    public Button continueButton;

    [Header("Culori Selectie")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.6f, 0.85f, 1f);

    public List<string> userAnswers = new List<string>();

    private List<Question> questions = new List<Question>();
    private int currentQuestionIndex = 0;
    private int selectedAnswerIndex = -1;
    private bool isMonitorOn = false;

    void Start()
    {
        questions.Add(new Question
        {
            questionText = "1. Iti plac cateii?",
            answers = new string[] { "a) Da", "b) Nu", "c) Nu stiu", "d) Ma abtin" }
        });
        questions.Add(new Question
        {
            questionText = "2. Cat timp petreci zilnic pe telefon?",
            answers = new string[] { "a) Sub 1 ora", "b) 1-3 ore", "c) 3-5 ore", "d) Peste 5 ore" }
        });
        questions.Add(new Question
        {
            questionText = "3. Ce continut urmaresti cel mai des?",
            answers = new string[] { "a) Gaming", "b) Meme-uri", "c) Stiri", "d) Moda / Lifestyle" }
        });

        if (powerLED != null) powerLED.SetActive(false);
        screenOffPanel.SetActive(true);
        formPanel.SetActive(false);
        finishPanel.SetActive(false);
        continueButton.interactable = false;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => SelectAnswer(index));
        }

        continueButton.onClick.AddListener(OnContinueClicked);
    }

    public void TurnOnMonitor()
    {
        if (powerButtonCollider != null) powerButtonCollider.enabled = false;
        if (isMonitorOn) return;

        isMonitorOn = true;
        if (powerLED != null) powerLED.SetActive(true);
        screenOffPanel.SetActive(false);
        formPanel.SetActive(true);
        LoadQuestion(0);
    }

    private void LoadQuestion(int index)
    {
        currentQuestionIndex = index;
        selectedAnswerIndex = -1;
        continueButton.interactable = false;

        Question q = questions[index];
        questionTextUI.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtonTexts[i].text = q.answers[i];
            answerButtons[i].image.color = normalColor;
        }
    }

    private void SelectAnswer(int answerIndex)
    {
        selectedAnswerIndex = answerIndex;
        continueButton.interactable = true;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].image.color = (i == answerIndex) ? selectedColor : normalColor;
        }
    }

    private void OnContinueClicked()
    {
        if (selectedAnswerIndex == -1) return;

        string chosen = questions[currentQuestionIndex].answers[selectedAnswerIndex];
        userAnswers.Add(chosen);
        Debug.Log($"Intrebarea {currentQuestionIndex + 1} - Raspuns: {chosen}");

        if (currentQuestionIndex + 1 < questions.Count)
        {
            LoadQuestion(currentQuestionIndex + 1);
        }
        else
        {
            formPanel.SetActive(false);
            finishPanel.SetActive(true);
        }
    }
}