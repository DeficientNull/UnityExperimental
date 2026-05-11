using Unity.VisualScripting;
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
    public float player_speed;
    public int player_health = 100;
    public float player_jump_amt;
    public float player_drag_amt;

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
        PlayerJump();
    }

    // Update is called once per frame
    void Update()
    {
        player_rb.linearDamping = player_drag_amt;

        // Temp Pause Menu Crap
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

        cam_orientation.y = 0;
        cam_side_orientation.y = 0;
        cam_orientation.Normalize();
        cam_side_orientation.Normalize();

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

        // clamp camera rotation
        cam_rotation_x = Mathf.Clamp(cam_rotation_x, -90f, 90f);

        // FIXME - weird issue where moving around the mouse feels very stuttery
        player_camera.transform.rotation = Quaternion.Euler(cam_rotation_x, cam_rotation_y, 0);
    }

    void PlayerJump()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            player_rb.AddForce(Vector3.up * player_jump_amt, ForceMode.Impulse); 
        }        
    }
}
