using UnityEngine;

public class MovementBoolScript : MonoBehaviour
{
    // This script is used for changing the method of locomotion in both the main menu and pause menu 
    public void MovementBool()
    {
        if (PlayerMovement.isUsingStickMovement == true)
        {
            PlayerMovement.isUsingStickMovement = false;
        }
        else if (PlayerMovement.isUsingStickMovement == false)
        {
            PlayerMovement.isUsingStickMovement = true;
        }
    }
}
