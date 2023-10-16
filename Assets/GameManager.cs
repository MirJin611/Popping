using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gameTime;
    public int currentTime;
    public int passedTime { get { return gameTime - currentTime; } }
    public TextMeshProUGUI timer;
    [Space]
    public int currentCombo;
    public int currentScore;
    public int bestScore;

    [SerializeField] private float dartPower;
    [SerializeField] private GameObject currentDart;
    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private Transform dartSpawnPoint;

    public BallonGenerator ballonGenerator;
    [SerializeField] private GameObject startButton;
    public GameObject startCount;
    public GameObject inGameUI;
    public TextMeshProUGUI score;

    public GameObject currentReusltPopup;
    public TextMeshProUGUI currentResult;

    public GameObject scoreHistoryPopup;
    public TextMeshProUGUI[] history = new TextMeshProUGUI[5];

    public static GameManager instance;
    private void Awake() => instance = this;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentDart.GetComponent<Rigidbody>().useGravity = true;
            currentDart.transform.parent = null;
            currentDart.GetComponent<Rigidbody>().AddForce(currentDart.transform.rotation * Vector3.back * dartPower);

            Invoke("SpawnDart", 1f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void SpawnDart()
    {
        Destroy(currentDart);
        GameObject dartGameObject = Instantiate(dartPrefab, dartSpawnPoint.position, dartSpawnPoint.rotation);
        currentDart = dartGameObject;
        currentDart.transform.parent = dartSpawnPoint;
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        currentDart.SetActive(true);
        currentTime = gameTime;
        currentScore = 0;
        score.text = currentScore.ToString();
        startCount.SetActive(true);
        scoreHistoryPopup.SetActive(false);
    }

    public void StartTimer()
    {
        inGameUI.SetActive(true);

        StartCoroutine(Timer());
        ballonGenerator.SetSpawnPoints();
        timer.text = gameTime.ToString();
        score.text = "0";
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        currentTime--;
        timer.text = currentTime.ToString();
        CheckTime();

        if (currentTime > 0)
            StartCoroutine(Timer());
        else
        {
            EndGame();
        }
    }

    private void CheckTime()
    {
        if (passedTime <= 10)
            ballonGenerator.currentSpawnTime = 0;
        else if (passedTime > 10 && passedTime <= 30)
            ballonGenerator.currentSpawnTime = 1;
        else
            ballonGenerator.currentSpawnTime = 2;
    }

    private void EndGame()
    {
        StopAllCoroutines();
        ballonGenerator.EndSpawnBallon();

        inGameUI.SetActive(false);
        CurrentResultOn();
    }

    public void ScoreUp(int score)
    {
        currentScore += score;
        this.score.text = currentScore.ToString();
    }

    public void CurrentResultOn()
    {
        currentReusltPopup.SetActive(true);
        currentResult.text = currentScore.ToString();
    }

    public void NextButton()
    {
        currentReusltPopup.SetActive(false);
        LoadScoreHistory();
        scoreHistoryPopup.SetActive(true);
    }

    void LoadScoreHistory()
    {
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("Score" + i))
            {
                if (PlayerPrefs.GetInt("Score" + i) < currentScore)
                {
                    PlayerPrefs.SetInt("Score" + i, currentScore);
                    break;
                }
            }
            else
            {
                PlayerPrefs.SetInt("Score" + i, 0);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            history[i].text = PlayerPrefs.GetInt("Score" + i).ToString();
        }
    }

    public void ComboUp()
    {
        currentCombo++;
        //curr.text = currentScore.ToString();
    }
}
