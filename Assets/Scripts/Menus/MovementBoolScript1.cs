using UnityEngine;

public class MovementBoolScript1 : MonoBehaviour
{
    // This script is used for changing the method of locomotion in both the main menu and pause menu 
    public void MovementBool()
    {
        if (PlayerMovement.isUsingStickMovement)
        {
            PlayerMovement.isUsingStickMovement = false;
        }
        else if (PlayerMovement.isUsingStickMovement == false)
        {
            PlayerMovement.isUsingStickMovement = true;
        }
    }
}
