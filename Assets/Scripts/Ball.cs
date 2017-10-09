using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Paddle paddle;
    private Vector3 paddleToBallVector;
    private Rigidbody2D rb2d;
    private bool gameStarted = false;

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted)
        {
            this.transform.position = paddle.transform.position + paddleToBallVector;

            if (Input.GetMouseButtonDown(0))
            {
                rb2d = GetComponent<Rigidbody2D>();
                rb2d.velocity = new Vector2(-5f, 10f);
                gameStarted = true;
            } else if (Input.GetMouseButtonDown(1))
            {
                rb2d = GetComponent<Rigidbody2D>();
                rb2d.velocity = new Vector2(5f, 10f);
                gameStarted = true;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameStarted)
        {
            Vector2 tweak = new Vector2(Random.Range(0.0f, 0.3f), Random.Range(0.0f, 0.3f));

            GetComponent<AudioSource>().Play();
            rb2d.velocity += tweak;
        }   
    }
}
