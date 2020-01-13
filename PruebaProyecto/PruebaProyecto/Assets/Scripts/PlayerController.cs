using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static float speedUp = 3500.0f;
    public static float speedDown = -200f;
    public static float speedRightAndLeft = 30;
    public Rigidbody rb;
    public GameObject rocked;
    public bool finished;
    public bool check;
    public float respawnTime;
    public static float time = 4;
    public GameObject player;
    public float maxPositionLeft;
    public float maxPositionRight;
    public float distanceTraveled;
    public int maxDistanceTraveled;
    public Text distance;
    public Canvas PlayAgain;
    public ParticleSystem particleRocked;

    public AudioSource audioMusicLoop;

    public BirdSound soundB;
    public Canvas onBoarding;
   
      
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0;
        respawnTime = time;
        check = false;
        finished = false;
        rb.AddForce(transform.up * speedUp);
      
        maxPositionLeft = -8.8f;
        maxPositionRight = 8.8f;
        distanceTraveled = 0;
        maxDistanceTraveled = 0;
        onBoarding.enabled = true;
        PlayAgain.enabled = false;
        audioMusicLoop = GetComponent<AudioSource>();
        audioMusicLoop.Stop();
    }

    private void Update()
    {
        distance.text = "Max Distance: " + maxDistanceTraveled;
        rocked.transform.Rotate(0.0f, 2.0f, 0.0f);
    
        CalculateMaxDistanceTraveled();
        


        if (!check) {
            respawnTime -= Time.deltaTime;
            if (respawnTime < 0)
            {
                finished = true;
            } 
        }
        else
        {
            respawnTime = time;
            check = false;
        }

        
            
        
        if (!finished)
        {
            Movement();
           
        }
        else
        {
            Time.timeScale = 0;
             audioMusicLoop.Stop();
            PlayAgain.enabled = true;
        }
    }

   

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Coin")
        {
            check = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * speedUp);
            particleRocked.Play();
        }
        else if (col.tag == "Bird")
        {
            check = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * speedDown);
            soundB.check = true;
        }
        else
        {
            check = false;
        }
    }

 
    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(transform.right * -speedRightAndLeft);
            if (player.transform.position.x < maxPositionLeft)
            {
                player.transform.position = new Vector3(maxPositionRight, player.transform.position.y, player.transform.position.z);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(transform.right * speedRightAndLeft);
            if (player.transform.position.x > maxPositionRight)
            {
                player.transform.position = new Vector3 (maxPositionLeft, player.transform.position.y, player.transform.position.z);
            }
        }
    }

    void CalculateMaxDistanceTraveled()
    {
        distanceTraveled = transform.position.y;
        if (distanceTraveled > maxDistanceTraveled)
        {
            maxDistanceTraveled = (int)distanceTraveled;
        }
    }


    public void ReplayScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartGame()
    {
        onBoarding.enabled = false;
        audioMusicLoop.Play();
        Time.timeScale = 1;
    }
}
