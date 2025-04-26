using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player game object
    public GameObject player;

    // Offset between the camera and the player
    private Vector3 offset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Calculate the inital offset between the camera and the player
        offset = transform.position - player.transform.position;
    }

    //  // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {
        // Maintain the same offset between the camera and player throughout the game.
        transform.position = player.transform.position + offset;
    }
}
