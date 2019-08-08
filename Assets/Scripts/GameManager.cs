using UnityEngine;

public enum DifficultyLevel { Easy, Medium, Hard };

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject _pauseMenuUI;

    private DifficultyLevel _selectedDifficulty;
    [EnumAction(typeof(DifficultyLevel))]
    public void SetDifficultyLevel(int levelIndex)
    {
        _selectedDifficulty = (DifficultyLevel)levelIndex;
    }
    public DifficultyLevel GetDifficultyLevel()
    {
        return _selectedDifficulty;
    }

    private ShipBehaviour _player;

    private int[,] _currentScore = new int[3, 10];

    private void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        //SaveSystem.ClearScore();
        _currentScore = SaveSystem.LoadScore();
    }

    public void UpdateScore()
    {
        _player = GameObject.FindObjectOfType<ShipBehaviour>();
        if (_player != null)
        {
            if (_player.GetCurrentScore() > _currentScore[(int)_selectedDifficulty, GetCurrentLevelIndex()])
            {
                _currentScore[(int)_selectedDifficulty, GetCurrentLevelIndex()] = _player.GetCurrentScore();
                SaveSystem.SaveScore(_currentScore);
            }
        }
    }

    public int GetHighestScoreOfIndex(int index)
    {
        return _currentScore[(int)_selectedDifficulty, index];
    }

    private int GetCurrentLevelIndex()
    {
        return SceneController.Instance.GetCurrentSceneIndex() - 1;
    }

    public bool GameIsPaused
    {
        get;
        private set;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) ResumeGame();
            else PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (_pauseMenuUI != null)
        {
            _pauseMenuUI.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        if (_pauseMenuUI != null)
        {
            _pauseMenuUI.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
