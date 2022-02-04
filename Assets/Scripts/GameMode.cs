using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    #region MemberFields
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPint;
    [SerializeField]
    private Character player;
    [SerializeField]
    private Image progress;
    [SerializeField]
    private Text currentTxt;
    [SerializeField]
    private Text nextTxt;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private Text firstText;
    [SerializeField]
    private Text secondText;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private Text firText;
    [SerializeField]
    private Text secText;
    [SerializeField]
    private Text timeLeftText;

    private int currentNumber = 0;
    private float coveredUnit = 0.0f;
    private float timeLeft = 3.0f;
    public static List<Character> characterList = new List<Character>();
    public static bool start = false;
    public static bool isFinished = false;
    #endregion MemberFields


    #region MonoBehaviour Methods
    private void Awake()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        currentNumber = SceneManager.GetActiveScene().buildIndex;
        float distance = Vector3.Distance(startPoint.position, endPint.position);
        coveredUnit = 1 / distance;
        currentTxt.text = string.Format("{0}", currentNumber);
        nextTxt.text = string.Format("{0}", currentNumber + 1);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!start)
        {
            UpdateCounDown();
        }
        UpdateProgressBar();
    }
    #endregion MonoBehaviour Methods


    #region Public Methods

    public void UpdateCounDown()
    {
        timeLeft -= Time.deltaTime;
        timeLeftText.text = string.Format("{0}", timeLeft.ToString("0"));
        if (timeLeft <= 0)
        {
            start = true;
            timeLeftText.enabled = false;
        }
    }
    public void AddWhoFinished(Character character)
    {
        if (characterList.Count < 2)
        {
            if (!characterList.Contains(character))
            {
                characterList.Add(character);
                isFinished = true;
            }
        }

        CheckWinner();

    }

    public void CheckWinner()
    {
        if (characterList.Count > 0)
        {
            if (characterList[0].transform.name == player.transform.name)
            {
                winPanel.SetActive(true);
                firstText.text = string.Format("Reza Hashemi");
                secondText.text = string.Format("jack Snow");
            }
            else
            {
                losePanel.SetActive(true);
                firText.text = string.Format("jack Snow");
                secText.text = string.Format("Reza Hashemi");
            }
        }
        characterList.Clear();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        start = false;
        isFinished = false;
        timeLeft = 3.0f;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        start = false;
        isFinished = false;
        timeLeft = 3.0f;
    }
    #endregion Public Methods


    #region Private Methods
    private void UpdateProgressBar()
    {
        float dis = Vector3.Distance(startPoint.position, player.transform.position);
        progress.fillAmount = dis * coveredUnit;
    }

    #endregion Private Methods

}
