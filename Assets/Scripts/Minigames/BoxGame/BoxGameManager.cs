using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxGameManager : MonoBehaviour
{
    public static bool ResetMinigameBox = false;

    [SerializeField] private Transform BoxCollection;
    [SerializeField] private Transform BallCollection;
    [SerializeField] private Transform ObstacleCollection;
    [SerializeField] private int winThreshold = 5;
    public BoxDetector[] boxes;
    public BallDetector[] balls;
    public GameObject[] obstacles;

    public bool won = false;
    public float BoxesKnocked = 0;
    public float BallsUsed = 0;

    private void Start()
    {
       boxes = new BoxDetector[BoxCollection.childCount];
       for (int i = 0; i < boxes.Length; i++)
       {
           boxes[i] = BoxCollection.GetChild(i).GetComponent<BoxDetector>();
       }
       
       balls = new BallDetector[BallCollection.childCount];
       for (int i = 0; i < balls.Length; i++)
       {
           balls[i] = BallCollection.GetChild(i).GetComponent<BallDetector>();
       }
       
       obstacles = new GameObject[ObstacleCollection.childCount];
       for (int i = 0; i < balls.Length; i++)
       {
           obstacles[i] = ObstacleCollection.GetChild(i).gameObject;
       }
    }
    private void Update()
    {
        // Check for points
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].boxHasMoved == true) // Checks if a box has moved
            {
                BoxesKnocked += 1; // Adds a point
                boxes[i].boxHasMoved = false; // avoids double counting
            }
        }
        
        // Win or loose
        if ( BoxesKnocked >= winThreshold && !won ) { WinGame(); }
        else if ( BallsUsed >= balls.Length){ ResetGame(); }
    }

    private void ResetGame()
    {
        
        for (int i = 0; i < boxes.Length; i++) // Resets Boxes 
        {
            boxes[i].ResetTransform(); // Sets box to start position
        }
        
        for (int i = 0; i < balls.Length; i++) // Resets Balls
        {
            balls[i].ResetTransform();  // Sets balls to start position
        }
        
        // Resets Score
        BallsUsed = 0; 
        BoxesKnocked = 0; 
    }

    private void WinGame()
    {
        won = true;
        // obstacle.SetActive(false);
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].GetComponent<c4>().Explode();
        }
        this.gameObject.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Balls") // Checks if a ball hits the area 
        {
            BallDetector ball = other.gameObject.GetComponent<BallDetector>();
            if (ball.isUsed == false)
            {
                BallsUsed += 1; // Makes it count towards used balls count
                ball.isUsed = true; // Avoids double counting
            }
        }
    }

}
