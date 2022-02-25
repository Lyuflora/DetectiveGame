using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

    public GameObject carrot, car, grapes, carrotBlack, carBlack, grapesBlack, blockPanel;


    Vector3 initialCarrotPosition, initialCarPosition, initialGrapesPosition, initialDogPosition, initialSheepPosition;

    bool carrotBool, carBool, grapesBool, dogBool, sheepBool = false;

    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    public GameObject QuestionImage;
    public Sprite answerImage;

    public GameObject PausePanel;
    public static bool gameIsPaused;

    public static QuizManager m_Instance;

    private void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        initialCarrotPosition = carrot.transform.position;
        initialCarPosition = car.transform.position;
        initialGrapesPosition = grapes.transform.position;

    }


    public void ShowAnswer()
    {
        QuestionImage.GetComponent<Image>().sprite = answerImage;
    }


    public void DragCarrot()
    {
        
        carrot.transform.position = Input.mousePosition;

    }


    public void DragCar()
    {

       
        car.transform.position = Input.mousePosition;

    }

    public void DragGrapes()
    {

       
        grapes.transform.position = Input.mousePosition;

    }
    


    public void DropCarrot()
    {

        float distance = Vector3.Distance(carrot.transform.position, carrotBlack.transform.position);
        if (distance < 50)
        {
            carrot.transform.position = carrotBlack.transform.position;
            Score.scoreNumber += 1;
            carrotBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            
        }
        else

        {
            carrot.transform.position = initialCarrotPosition;
            source.clip = incorrect;
            source.Play();
        }




    }

    public void DropCar()
    {

        float distance = Vector3.Distance(car.transform.position, carBlack.transform.position);
        if (distance < 50)
        {
            car.transform.position = carBlack.transform.position;
            Score.scoreNumber += 1;
            carBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
        }
        else
        {
            car.transform.position = initialCarPosition;
            source.clip = incorrect;
            source.Play();
        }

    }

    public void DropGrapes()
    {

        float distance = Vector3.Distance(grapes.transform.position, grapesBlack.transform.position);
        if (distance < 50)
        {
            grapes.transform.position = grapesBlack.transform.position;
            Score.scoreNumber += 1;
            grapesBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
        }
        else
        {
            grapes.transform.position = initialGrapesPosition;
            source.clip = incorrect;
            source.Play();
        }

    }


    
   


     void Update()
    {
        if(carrotBool && carBool && grapesBool && dogBool && sheepBool || Timer.time<=0)
        {

            //StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        blockPanel.SetActive(true);
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
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
