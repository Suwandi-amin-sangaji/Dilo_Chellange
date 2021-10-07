using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null) Debug.LogError("No Game Manager Found!!!");
            }
            return _instance;
        }
    }
    #endregion

    public int Score { get; private set; }

    [Header("Box Coin Controller")]
    public int coinSpawn;
    [SerializeField] BoxCoin boxCoinPrefab;
    private List<BoxCoin> boxCoinsPool = new List<BoxCoin>();
    public bool isRespawnBox;

    [Header("Obstacle Controller")]
    public Obstacle obstaclePf;
    private List<Obstacle> obstaclesPool = new List<Obstacle>();
    public float spawnTime;
    public bool isSpawningObstacle;

    [Header("Game area constraint")]
    public float areaConstraintValue = 5f;
    private Vector2 screenWidhtPosition;
    private Transform playerPos;

    #region GAME STATE

    private bool isGameOver;
    private bool isPaused;

    #endregion

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject tutorialPanel;
    public GameObject pausePanel;

    private void Start()
    {
        screenWidhtPosition = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        playerPos = FindObjectOfType<PlayerMovement>().transform;

        for (int i = 0; i < coinSpawn; i++)
        {
            BoxCoin coin = GetBox();
            coin.Spawn();
        }

        if(isSpawningObstacle)
            StartCoroutine(SpawnObstacle());
    }

    #region SPAWNING OBSTACLE
    public void RespawnBox() 
    {
        if (isRespawnBox)
        {
            StartCoroutine(ReSpawnBox());
        }
    }
    IEnumerator ReSpawnBox()
    {
        yield return new WaitForSeconds(3);
        BoxCoin coin = GetBox();
        coin.Spawn();
    }

    IEnumerator SpawnObstacle()
    {
        while (!isGameOver)
        {
            int spawnDirection = Random.Range(0, 3);
            Vector2 randomSpawn = GetObstacleDirection(spawnDirection);

            Obstacle obstacle = GetObstacle();
            obstacle.transform.position = randomSpawn;
            obstacle.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            obstacle.MoveTo((Vector2)playerPos.position - randomSpawn);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public Vector2 GetRandomPosition()
    {
        float xPosition = Random.Range(-areaConstraintValue, areaConstraintValue);
        float yPosition = Random.Range(-areaConstraintValue, areaConstraintValue);

        return new Vector2(xPosition, yPosition);
    }

    private Vector2 GetObstacleDirection(int dir)
    {
        switch (dir)
        {
            case 0: //UP
                return new Vector2(
                    Random.Range(-screenWidhtPosition.x, screenWidhtPosition.x),
                    screenWidhtPosition.y
                );

            case 1: //DOWN
                return new Vector2(
                    Random.Range(-screenWidhtPosition.x, screenWidhtPosition.x),
                    -screenWidhtPosition.y
                );

            case 2: //RIGHT
                return new Vector2(
                    screenWidhtPosition.x,
                    Random.Range(-screenWidhtPosition.y, screenWidhtPosition.y)
                );
            default: //LEFT
                return new Vector2(
                    -screenWidhtPosition.x,
                    Random.Range(-screenWidhtPosition.y, screenWidhtPosition.y)
                );
        }
    }
    #endregion

    #region OBJECT POOL
    /// <summary>
    /// Mengambil coin didalam pool
    /// </summary>
    /// <returns></returns>
    public BoxCoin GetBox()
    {
        for (int i = 0; i < boxCoinsPool.Count; i++)
        {
            if (!boxCoinsPool[i].gameObject.activeSelf)
            {
                boxCoinsPool[i].gameObject.SetActive(true);
                return boxCoinsPool[i];
            }
        }

        BoxCoin boxObject = Instantiate(boxCoinPrefab, transform);
        boxCoinsPool.Add(boxObject);
        return boxObject;
    }

    public Obstacle GetObstacle()
    {
        for (int i = 0; i < obstaclesPool.Count; i++)
        {
            if (!obstaclesPool[i].gameObject.activeSelf)
            {
                obstaclesPool[i].gameObject.SetActive(true);
                return obstaclesPool[i];
            }
        }

        Obstacle boxObject = Instantiate(obstaclePf, transform);
        obstaclesPool.Add(boxObject);
        return boxObject;
    }
    #endregion

    #region GAME STATE
    public void GameOver()
    {
        isGameOver = true;
        ScoreManager.Instance.SetHighScore();

        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void TutorialPanel()
    {
        if (!isPaused)
        {
            isPaused = true;
            tutorialPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            tutorialPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    #endregion
}
