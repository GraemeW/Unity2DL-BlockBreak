using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float screenWidthInUnits = 16.0f;
    [SerializeField] float minX = 1.0f;
    [SerializeField] float maxX = 15.0f;

    // Cached reference
    GameStatus gameStatus;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStatus.IsAutoTestingEnabled() || !ball.IsBallLaunched())
        {
            mouseControlledMovement();
        }
        else
        {
            automatedMovement();
        }
    }

    private void automatedMovement()
    {
        transform.position = new Vector2(Mathf.Clamp(ball.transform.position.x, minX, maxX), transform.position.y);
    }

    private void mouseControlledMovement()
    {
        float mousePositionInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(mousePositionInUnits, minX, maxX);
        transform.position = paddlePosition;
    }
}
