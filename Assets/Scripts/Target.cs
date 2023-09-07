using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minForce = 12;
    private float maxForce = 16;
    private float Torque = 10;
    private float xRange = 4;
    private float yPosition = -2;

    public int Score;
    private GameManager gameManagerScript;
    public ParticleSystem explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(randomForce(), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(), randomTorque(),randomTorque(), ForceMode.Impulse);

        transform.position = randomPosition();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnMouseDown() 
    // {
    //     if (gameManagerScript.isGameActive && !gameManagerScript.gameIsPaused)
    //     {
    //         Destroy(gameObject);
    //         if (gameObject.CompareTag("Bad"))
    //         {
    //             gameManagerScript.GameOver();
    //         }
    //         gameManagerScript.updateScore(Score);
    //         Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);   
    //     }
    // }

    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
        if(!other.gameObject.CompareTag("Bad"))
        {
            gameManagerScript.GameOver();
        }
    }

    public void DestroyTarget()
    {
        if (gameManagerScript.isGameActive && !gameManagerScript.gameIsPaused)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Bad"))
            {
                gameManagerScript.GameOver();
            }
            Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
            gameManagerScript.updateScore(Score);
        }
    }

    Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    float randomTorque()
    {
        return Random.Range(-Torque, Torque);
    }

    Vector3 randomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), yPosition);
    }
}
