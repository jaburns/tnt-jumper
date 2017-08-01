using UnityEngine;

/// <summary>
/// This component simply checks if the ESC key is being pressed and quits the program if it is.
/// </summary>
public class QuitChecker : MonoBehaviour 
{
    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}