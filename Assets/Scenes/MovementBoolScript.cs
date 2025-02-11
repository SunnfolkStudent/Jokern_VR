using UnityEngine;

public class MovementBoolScript : MonoBehaviour
{
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
