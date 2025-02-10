using UnityEngine;
using UnityEngine.InputSystem;

public class BoxGameManager : MonoBehaviour
{
    public static bool ResetMinigameBox = false;
    public BoxDetector[] boxes;
    public BallDetector[] balls;
    public float BoxesKnocked = 0;
    public float BallsUsed = 0;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) //temporary test method to see if anythings borked
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].ResetTransform();
            }

            for (int i = 0; i < balls.Length; i++)
            {
                balls[i].ResetTransform();
            }
        }

        if (BoxesKnocked < 5 && BallsUsed == 3) //the reset method checks if all 5 boxes have been knocked down and if all your balls have been used
        {
            for (int i = 0; i < boxes.Length; i++) //resets the boxes transform so you can try again
            {
                boxes[i].ResetTransform(); //sends a command to the boxDetector to invoke the ResetTransform method
                BoxesKnocked = 0; //sets the amount of boxes knocked to 0
                //boxes[i]._initialized = false;
            }
            //the for statement has been used instead of for each simply because for each was refusing to work
            for (int i = 0; i < balls.Length; i++) //resets the balls transform so you can try again
            {
                balls[i].ResetTransform(); //sends a command to the ballDetector to invoke the ResetTransform method
                BallsUsed = 0; //sets the amount of balls used to 0
            }
        }
        
        for (int i = 0; i < boxes.Length; i++) //again a for method that checks the array of boxes
        {
            if (boxes[i].boxHasMoved == true) //checks if the boxHasMoved variable has become true
            {
                BoxesKnocked +=1; //increases the amount of boxesKnocked by 1 every time boxHasMoved has become true
                boxes[i].boxHasMoved = false; //disables the boxHasMoved variable to avoid accidentally turning it back on we have a variable
            }
        }
    }

    void OnTriggerEnter(Collider other) //checks if something has entered the managers trigger area if it has it assigns it the name other
    {
        if (other.gameObject.tag == "Balls") //checks the object with the assigned name other if it has the tag Balls
        {
            BallsUsed += 1; //increases the amount of ballsUsed by 1
        }
    }

}
