using UnityEditor.Callbacks;
using UnityEngine;

public class player_main : MonoBehaviour
{

    // Load Components

    public Rigidbody player_rb;
    public Transform player_body_position;
    public Transform player_camera;

    // Player Stats
    public float cam_sensitivity = 200;
    private float cam_rotation_x;
    private float cam_rotation_y;
    public float player_speed = 2.5f;
    public int player_health = 100;

    // player hud setup
    //bool is_game_paused = false;
    
    void Start()
    {
        // Lock the mouse and turn it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // move the camera into place
        Vector3 player_height = new Vector3 (0, 1, 0);
        player_camera.transform.position = player_body_position.transform.position + player_height;

        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("game paused"); // all i need for now
        }
        //DrawPauseMenu();
        MouseLook();
    }

    void PlayerMove()
    {
        // move the rigidbody based on camera orientation
        Vector3 cam_orientation = player_camera.transform.forward;
        Vector3 cam_side_orientation = player_camera.transform.right;

        if (Input.GetKey("w"))
        {
            player_rb.AddForce(cam_orientation * player_speed);
        }
        if (Input.GetKey("s"))
        {
            player_rb.AddForce(-cam_orientation * player_speed);
        }
        if (Input.GetKey("a"))
        {
            player_rb.AddForce(-cam_side_orientation * player_speed);
        }
        if (Input.GetKey("d"))
        {
            player_rb.AddForce(cam_side_orientation * player_speed);
        }
    }

    void MouseLook()
    {
        float mouse_x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cam_sensitivity;
        float mouse_y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cam_sensitivity;
        
        cam_rotation_y += mouse_x;
        cam_rotation_x -= mouse_y;

        player_camera.transform.rotation = Quaternion.Euler(cam_rotation_x, cam_rotation_y, 0);
    }
}
