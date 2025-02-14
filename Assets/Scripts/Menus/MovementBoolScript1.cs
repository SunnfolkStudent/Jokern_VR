using UnityEngine;

public class MovementBoolScript1 : MonoBehaviour
{
    public GameObject Move;
    public GameObject Teleport;
    
    public static bool isUsingStickMovement = true;
    
    // This script is used for changing the method of locomotion in both the main menu and pause menu 
    public void MovementBool()
    {
        if (PlayerMovement.isUsingStickMovement == true)
        {
            PlayerMovement.isUsingStickMovement = false;
            Move.SetActive(false);
            Teleport.SetActive(true);
        }
        else if (PlayerMovement.isUsingStickMovement == false)
        {
            PlayerMovement.isUsingStickMovement = true;
            Teleport.SetActive(false);
            Move.SetActive(true);
        }
    }
}
