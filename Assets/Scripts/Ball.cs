using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] Paddle paddle1 = null;
    [SerializeField] float clickVelocityX = 0.0f;
    [SerializeField] float clickVelocityY = 15.0f;
    [SerializeField] AudioClip[] ballBounceSounds = null;
    [SerializeField] float randomFactor = 0.2f;

    // State
    Vector2 paddleToBallVector;
    bool ballLaunched = false;

    // Cached Reference
    AudioSource audioSource;
    Rigidbody2D ballRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        ballRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballLaunched)
        {
            LockBallToPaddle();
            LaunchBallOnMouseClick();
        }
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ballRigidbody2D.velocity = new Vector2(clickVelocityX, clickVelocityY);
            ballLaunched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballLaunched)
        {
            AudioClip clip = ballBounceSounds[UnityEngine.Random.Range(0, ballBounceSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (ballLaunched)
        {
            Vector2 randomizedVelocityAdder = new Vector2(UnityEngine.Random.Range(-randomFactor, randomFactor), UnityEngine.Random.Range(-randomFactor, randomFactor));
            ballRigidbody2D.velocity += randomizedVelocityAdder;
        }
    }

    public bool IsBallLaunched()
    {
        return ballLaunched;
    }
}
