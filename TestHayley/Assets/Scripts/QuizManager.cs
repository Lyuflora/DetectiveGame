using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dec {
    public class QuizManager : MonoBehaviour {

        public static QuizManager m_Instance;
        public GameObject carrot, car, grapes, carrotBlack, carBlack, grapesBlack, blockPanel;
        Vector3 initialCarrotPosition, initialCarPosition, initialGrapesPosition, initialDogPosition, initialSheepPosition;
        bool carrotBool, carBool, grapesBool, dogBool, sheepBool = false;
        
        [Header("Sounds")]
        public AudioSource source;
        public AudioClip[] correct;
        public AudioClip incorrect;

        [Header("Quiz UI Components")]
        public TMP_Text questionSentence;
        public Image m_QuestionImage;
        public Sprite answerImage;

        public GameObject PausePanel;
        public static bool gameIsPaused;

        public GameObject m_CheckButton;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        [Header("Quiz Contents")]
        [SerializeField] private Questions m_CurrentQuestion;
        public List<Questions> m_QuestionList;

        public Transform ChoiceSlotParent;
        public Transform ChoiceDragableParent;
        [SerializeField] private GameObject m_ChoiceSlotPrefab;
        [SerializeField] private GameObject m_ChoiceDragablePrefab;

        [SerializeField]
        private string choiceCode;
        private List<Item> choiceList;

        private void Awake()
        {
            m_Instance = this;
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Start()
        {
            //initialCarrotPosition = carrot.transform.position;
            m_CheckButton.SetActive(false);
            ClearQuestion();
            LoadQuestion();
            choiceList = new List<Item>();
        }

        public void OnCheckButtonClick()
        {
            Debug.Log("Check if the answer is right");
            Questions q = m_CurrentQuestion;
            // Compare choices and the correct answer
            for(int i = 0; i < q.m_CorrectChoices.Count; i++)
            {
                if (q.m_CorrectChoices.Contains(choiceList[i]))
                {
                    continue;
                }
                else
                {
                    WrongAnswer();
                    return;
                } 
            }
            CorrectAnswer();
        }

        void CorrectAnswer()
        {
            Debug.Log("Correct! You got it!");
            // replace answer image
            answerImage = m_CurrentQuestion.icon;
            m_QuestionImage.sprite = answerImage;
            // add the insight to inventory
            PlayerInventory.m_Instance.AddItem(m_CurrentQuestion.m_AnswerItem);
        }

        void WrongAnswer()
        {
            Debug.Log("Wrong answer...");
        }

        public void ClearChoice()
        {
            choiceList.Clear();
        }
        public void UpdateChoice()
        {

        }

        // Assign current quesiton to the UI
        public void LoadQuestion()
        {
            Debug.Log("Load Quiz Panel");
            // Question
            questionSentence.text = m_CurrentQuestion.question;

            // Choice Slot
            int slotCount = m_CurrentQuestion.m_CorrectChoices.Count;
            for (int i = 0; i < slotCount; i++)
            {
                GameObject newChoiceSlot = Instantiate(m_ChoiceSlotPrefab, ChoiceSlotParent);
            }


            // Choices
            int choiceCount = m_CurrentQuestion.m_DragableChoices.Count;
            for (int i = 0; i < choiceCount; i++)
            {
                GameObject newChoice = Instantiate(m_ChoiceDragablePrefab, ChoiceDragableParent);
                newChoice.GetComponentInChildren<InventorySlot>().DisplayItemUI(m_CurrentQuestion.m_DragableChoices[i]);

            }
            ChoiceDragableParent.GetComponent<HorizontalLayoutGroup>().enabled = true;

            StartCoroutine(CallDialogue());


            // Right Answer

        }
        IEnumerator CallDialogue()
        {
            Debug.Log("Dialogue");
            yield return new WaitForSeconds(0.5f);
            ChoiceDragableParent.GetComponent<HorizontalLayoutGroup>().enabled = false;
        }

        public void ClearQuestion()
        {
            Debug.Log("Load Quiz Panel");
            Score.scoreNumber = 0;
            foreach (Transform child in ChoiceSlotParent)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in ChoiceDragableParent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void DragChoice(PointerEventData pointerData)
        {
            Debug.Log("Pick an answer");
            rectTransform.anchoredPosition = Input.mousePosition;
            //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void AddChoice(Item item)
        {
            if (m_CurrentQuestion == null)
                return;

            if(choiceList.Count<m_CurrentQuestion.question.Length)
                choiceList.Add(item);
        }

        public void EndDragChoice()
        {
            Score.scoreNumber += 1;
            if (CheckScore())
            {
                ShowCheckButton();
            }
        }
        bool CheckScore()
        {
            // if fill all slots, return true, else return false
            return Score.scoreNumber == m_CurrentQuestion.m_CorrectChoices.Count;
        }

        public void ShowAnswer()
        {
            m_QuestionImage.GetComponent<Image>().sprite = m_CurrentQuestion.m_AnswerItem.icon;
        }
        public void ShowCheckButton()
        {
            m_CheckButton.SetActive(true);
        }


        // no use
        public void DropChoice()
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
            else{
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
            if (Input.GetKeyDown(KeyCode.K))
            {
                ClearQuestion();
                LoadQuestion();
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
}