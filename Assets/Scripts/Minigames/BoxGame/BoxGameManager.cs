using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxGameManager : MonoBehaviour
{
    public static bool ResetMinigameBox = false;

    [SerializeField] private Transform BoxCollection;
    [SerializeField] private Transform BallCollection;
    public BoxDetector[] boxes;
    public BallDetector[] balls;
    
    public GameObject obstacle;
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
    }

    // private void Update()
    // {
    //     if (BoxesKnocked < boxes.Length && BallsUsed == balls.Length) //the reset method checks if all 5 boxes have been knocked down and if all your balls have been used
    //     {
    //         for (int i = 0; i < boxes.Length; i++) //resets the boxes transform so you can try again
    //         {
    //             boxes[i].ResetTransform(); //sends a command to the boxDetector to invoke the ResetTransform method
    //             BoxesKnocked = 0; //sets the amount of boxes knocked to 0
    //             //boxes[i]._initialized = false;
    //         }
    //         //the for statement has been used instead of for each simply because for each was refusing to work
    //         for (int i = 0; i < balls.Length; i++) //resets the balls transform so you can try again
    //         {
    //             balls[i].ResetTransform(); //sends a command to the ballDetector to invoke the ResetTransform method
    //             BallsUsed = 0; //sets the amount of balls used to 0
    //         }
    //     }
    //
    //     if (BoxesKnocked == boxes.Length && BallsUsed <= BallsUsed)
    //     {
    //         obstacle.SetActive(false);
    //     }
    //     
    //     for (int i = 0; i < boxes.Length; i++) //again a for method that checks the array of boxes
    //     {
    //         if (boxes[i].boxHasMoved == true) //checks if the boxHasMoved variable has become true
    //         {
    //             BoxesKnocked +=1; //increases the amount of boxesKnocked by 1 every time boxHasMoved has become true
    //             boxes[i].boxHasMoved = false; //disables the boxHasMoved variable to avoid accidentally turning it back on we have a variable
    //         }
    //     }
    // }

    private void Update()
    {
        if ( BoxesKnocked >= boxes.Length ) { WinGame(); }

        // Check for points
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].boxHasMoved == true) // Checks if a box has moved
            {
                BoxesKnocked += 1; // Adds a point
                boxes[i].boxHasMoved = false; // avoids double counting
            }
        }
        
        if( BallsUsed >= balls.Length ){ ResetGame(); }
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
        obstacle.SetActive(false);
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
