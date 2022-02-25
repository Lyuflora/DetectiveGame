using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject panel;
    public static bool gameIsPaused = false;

    public GameObject blockPanel;

    public AudioClip[] correct;
    public AudioClip[] voices;
    public AudioClip incorrect;
    public AudioClip EndOfScene;
    public AudioSource source;

    public Questions[] questions;
    private List<Questions> unansweredQuestion;

    public static Questions currentQuestion;

    [SerializeField]
    private Text questionText;

    private float timeBetweenQuestions = 2f;

    private int numberOfQuestion, testNumberOfQuestion;
        
        

	void Start ()
    {
        source = gameObject.GetComponent<AudioSource>();

        if (unansweredQuestion == null || unansweredQuestion.Count == 0 )
        {
            unansweredQuestion = questions.ToList<Questions>();
        }
        numberOfQuestion = unansweredQuestion.Count;
        SetRandomQuestions();
	}

    void SetRandomQuestions()
    {
      
            int randomQuestionIndex = Random.Range(0, unansweredQuestion.Count);
        currentQuestion = unansweredQuestion[randomQuestionIndex];
       
            questionText.text = currentQuestion.question;

   
            for (int i = 0; i < voices.Count(); i++)
        {
            if (voices[i].name == currentQuestion.name)
            {
                source.clip = voices[i];

            }

        }

        source.Play();
        
    }

    public void SelectedButton(Button btn)
    {
        if (btn.name == GameManager.currentQuestion.name)
        {
            source.clip = correct[Random.Range(0,correct.Length)];
            source.Play();
            btn.gameObject.SetActive(false);
            StartCoroutine(TransitionToNextQuestion());
            testNumberOfQuestion++;
            Score.scoreNumber += 1;
        }
        else
        {
            source.clip = incorrect;
            source.Play();
        }

        if (testNumberOfQuestion == numberOfQuestion)
        {
            source.clip = EndOfScene;
            source.Play();         
            blockPanel.SetActive(true);
            StartCoroutine(StartNewScene());
        }


       

    }


    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestion.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        SetRandomQuestions();
            
    }
    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(timeBetweenQuestions);
        SetRandomQuestions();

    }
    IEnumerator StartNewScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }




    public void Setting()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    
        
    }

    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Exit()
    {
        
        Application.Quit();
    }


    void Update()
    {

        if (Timer.time <= 0)
        {
         
            blockPanel.SetActive(true);
            StartCoroutine(StartNewScene());

        }
    }
    


}
