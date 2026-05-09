using UnityEditor.Callbacks;
using UnityEngine;

public class player_main : MonoBehaviour
{

    // Load Components

    public Rigidbody player_rb;
    public Transform player_body_position;
    public Transform player_camera;

    // Player Stats
    public float cam_sensitivity;
    public float player_speed = 2.5f;
    public int player_health = 100;

    // player hud setup

    //bool is_game_paused = false;
   

    void FixedUpdate()
    {
        // move the camera into place
        Vector3 player_height = new Vector3 (0, 1, 0);
        player_camera.transform.position = player_body_position.transform.position + player_height;

        // move the rigidbody based on camera orientation
        Vector3 cam_orientation = player_camera.transform.forward;

        if (Input.GetKey("w"))
        {
            player_rb.AddForce(cam_orientation * player_speed);
        }
        if (Input.GetKey("s"))
        {
            player_rb.AddForce(-cam_orientation * player_speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("game paused"); // all i need for now
        }
    }
}
