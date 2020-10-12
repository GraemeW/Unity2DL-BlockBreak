using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // Tunables
    [SerializeField] float gameSpeedMin = 0.1f;
    [SerializeField] float gameSpeedMax = 10.0f;
    [Range(0.1f, 10.0f)] [SerializeField] float gameSpeed = 1.0f;
    [SerializeField] TextMeshProUGUI scoreUI = null;
    [SerializeField] bool automateTesting = false;

    // State
    [SerializeField] int currentScore = 0; // Serialized to monitor in Unity for debug

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IncrementScore(0); // Re-use increment code to print score initially to UI
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = Mathf.Clamp(gameSpeed, gameSpeedMin, gameSpeedMax);
    }

    public void IncrementScore(int pointAdder)
    {
        currentScore += pointAdder;
        scoreUI.text = currentScore.ToString();
    }

    public void DestroyGameStatus()
    {
        Destroy(gameObject);
    }

    public bool IsAutoTestingEnabled()
    {
        return automateTesting;
    }
}
