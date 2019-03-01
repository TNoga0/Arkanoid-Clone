using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{

    private Rigidbody ball;
    private float ballSpeed = 15f;
    Vector3 reflectionVelocity;
    float reflectionDir;
    private AudioSource source;
    public AudioClip paddleSound;
    public AudioClip boundarySound;
    public GameObject gameOverText;
    private int score = 0;
    public Text scoreText;
    public GameObject victoryText;
    public GameScript Game;

    // Start is called before the first frame update
    void Start()
    {
        ballSpeed = PlayerPrefs.GetFloat("ballSpeed");
        ball = GetComponent<Rigidbody>();
        ball.velocity = Vector3.up * ballSpeed;
        source = GetComponent<AudioSource>();
        Game = gameObject.AddComponent(typeof(GameScript)) as GameScript;   //adding GameScript object to check Victory
    }

    // Update is called once per frame
    void Update()
    {
        reflectionVelocity = ball.velocity; //saving the velocity value just before collision, to reflect it later on.
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "PlayerPad") //wall does not cause any twists and tweaks
        {
            reflectionDir = hitDirection(collision.collider.transform.position, transform.position, collision.collider.bounds.size.x);
            Vector3 newDir = new Vector3(reflectionDir, 1, 0).normalized;
            ball.velocity = newDir * ballSpeed; //inserting new velocity
            source.PlayOneShot(paddleSound, 0.11f);
        }
        else if(collision.gameObject.tag == "Boundary")
        {
            Vector3 newVelocity = Vector3.Reflect(reflectionVelocity, collision.contacts[0].normal); //reflection
            ball.velocity = newVelocity; //inserting new velocity
            source.PlayOneShot(boundarySound, 0.11f);
        }
        else if(collision.gameObject.tag == "Block")
        {
            Vector3 newVelocity = Vector3.Reflect(reflectionVelocity, collision.contacts[0].normal); //reflection
            ball.velocity = newVelocity; //inserting new velocity
            source.PlayOneShot(boundarySound, 0.11f);
            Destroy(collision.gameObject);
            score += 1;
            scoreText.text = score.ToString();
            if (CheckVictory(ref Game))
            {
                ball.velocity = Vector3.zero;
                VictoryScreen();
            }
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            Vector3 newVelocity = Vector3.Reflect(reflectionVelocity, collision.contacts[0].normal); //reflection
            ball.velocity = newVelocity; //inserting new velocity
            source.PlayOneShot(boundarySound, 0.11f);
            score += 1;
            scoreText.text = score.ToString();
            if (CheckVictory(ref Game))
            {
                ball.velocity = Vector3.zero;
                VictoryScreen();
            }
        }

        if (collision.gameObject.name == "BottomBoundary")
        {

            Destroy(GameObject.Find("Ball"));  //destroy ball if player fails to hit the ball with a paddle
            gameOverText.SetActive(true);
        }

        
    }

    private float hitDirection(Vector3 paddlePos, Vector3 ballPos, float paddleWidth) //calculating where the ball should go (direction coefficient), based on the part of paddle it hits
    {
        return (ballPos.x - paddlePos.x) / paddleWidth;  //equation gives:
        //  -1  -0.5  0  0.5  1    <- where the ball hits
        //   ==================    <-  paddle
    }

    private bool CheckVictory(ref GameScript game)
    {
        for (int w = 0; w < game.gridDimensions[0] - 1; w++)
        {
            for (int k = 0; k < game.gridDimensions[1] - 1; k++)
            {
                if (score < 85)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void VictoryScreen()
    {
        victoryText.SetActive(true);
    }

}
