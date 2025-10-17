using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI; //게임오버 UI 캔버스
    public TextMeshProUGUI finalScoreText; //점수 텍스트
    public TextMeshProUGUI bestScoreText; //최고 점수 텍스트

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

        finalScoreText.text = $"{score}";
        int bestScore = PlayerPrefs.GetInt("BestScore", 0); //PlayerPrefs는 유니티 게임 내에서 데이터를 저장하는 시스템
        if (score > bestScore) //score가 bestScore보다 크면
        {
            PlayerPrefs.SetInt("BestScore", score); //BestScore 키에 score 값을 저장
            bestScore = score; //bestScore를 score로 업데이트
        }
        bestScoreText.text = $"{bestScore}";
    }
    //추가 내용
    void OnEnable()
    {
        
       PlayerController.OnDied += GameOver;

       
    }
//추가 내용
    void OnDisable()
    {
        // 스크립트가 비활성화될 때 구독을 해제하여 오류를 방지
       PlayerController.OnDied -= GameOver;

       
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //SceneManager는 유니티 게임 내에서 씬을 관리하는 시스템
    }

    public void QuitGame()
    {
        Application.Quit(); //Application은 유니티 게임 내에서 애플리케이션을 관리하는 시스템
    }
}