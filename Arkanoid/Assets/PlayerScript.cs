using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    private float movementSpeed = 15f;
    private Rigidbody paddle;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        paddle = GetComponent<Rigidbody>();
        movementSpeed = PlayerPrefs.GetFloat("paddleSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxisRaw("Horizontal"); //if no key pressed, returns 0, else coefficient to move the paddle

        paddle.velocity = Vector3.right * movementSpeed * dir; //movement

        if (gameOverText && Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (gameOverText && Input.GetKeyDown("q"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Powerup")
        {
            Destroy(collision.collider.gameObject);
            gameObject.transform.localScale = new Vector3(1.3f * gameObject.transform.localScale.x, 0.3f, 1);
        }
    }
}
