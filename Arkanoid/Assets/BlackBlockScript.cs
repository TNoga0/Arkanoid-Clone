using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlockScript : MonoBehaviour
{

    public GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {
        powerUp = GameObject.FindGameObjectWithTag("Powerup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnPowerup();
    }

    void SpawnPowerup()
    {
        Instantiate(powerUp, gameObject.transform.position, Quaternion.identity);
        powerUp.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 3, 0));
    }
}
