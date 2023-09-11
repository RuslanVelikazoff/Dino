using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public Button startButton;

    public GameObject pausePanel;
    public Button pauseButton;
    public Button continueButton;
    public Button restartButton;
    public Button soundButton;

    public Sprite onSoundsSprite;
    public Sprite offSoundsSprite;

    public GameObject losePanel;
    public Button loseButton;

    public Text scoreText;
    public Text highScoreText;

    private void Start()
    {
        ButtonFunc();
        highScoreText.text = "High score " + PlayerPrefs.GetFloat("HighScore");
    }

    private void ButtonFunc()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() =>
            {
                startPanel.SetActive(false);
                GameManager.Instance.NewGame();
            });
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                SetSoundButtonSprite();
                pausePanel.SetActive(true);
            });
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            });
        }

        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            {
                GameManager.Instance.NewGame();
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            });
        }

        if (soundButton != null)
        {
            soundButton.onClick.RemoveAllListeners();
            soundButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("Volume") == 1f)
                {
                    AudioManager.instance.OffSound();
                    SetSoundButtonSprite();
                }
                else
                {
                    AudioManager.instance.OnSound();
                    SetSoundButtonSprite();
                }
            });
        }

        if (loseButton != null)
        {
            loseButton.onClick.RemoveAllListeners();
            loseButton.onClick.AddListener(() =>
            {
                losePanel.SetActive(false);
                GameManager.Instance.NewGame();
            });
        }
    }

    private void Update()
    {
        int score = (int)GameManager.Instance.score;
        scoreText.text = "" + score;

        if (GameManager.Instance.score >= PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", GameManager.Instance.score);
        }

        highScoreText.text = "High score " + (int)PlayerPrefs.GetFloat("HighScore");
    }

    private void SetSoundButtonSprite()
    {
        if (PlayerPrefs.GetFloat("Volume") == 1f)
        {
            soundButton.GetComponent<Image>().sprite = onSoundsSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = offSoundsSprite;
        }
    }
}
