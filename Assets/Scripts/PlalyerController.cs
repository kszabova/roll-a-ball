using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlalyerController : MonoBehaviour {

    public float speed;
    public GameObject pickUp;
    public Text scoreText;
    public Text winText;
    public Text strikeText;

    private Rigidbody rb;
    private int score;
    private int count;
    private int strikes;
    private bool gameInProgress;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        count = 0;
        strikes = 0;
        gameInProgress = true;
        winText.text = "";
        strikeText.text = "";
        UpdateScore();
        UpdateStrikes();
        Invoke("AddPickUp", Random.Range((float)0.2, (float)1.5));
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameInProgress)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                count += 1;
                score += 1;
                UpdateScore();
                speed += 1;
                for (int i = 0; i < Random.Range(1, 4); ++i)
                {
                    Invoke("AddPickUp", Random.Range(0.2f, 2.0f));
                }
        }

            if (other.gameObject.CompareTag("Wall"))
            {
                strikes += 1;
                UpdateStrikes();
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateStrikes()
    {
        strikeText.text = "Strikes: " + strikes.ToString();
        if (strikes >= 3)
        {
            winText.text = "GAME OVER!\nYour score is " + score.ToString();
            gameInProgress = false;
        }
    }

    void AddPickUp()
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), 1, Random.Range(-9, 9));
        Instantiate(pickUp, pos, transform.rotation);
    }

}
