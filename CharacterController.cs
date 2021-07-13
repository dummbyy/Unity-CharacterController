using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    
    public CharacterController controller;
    public float characterSpeed = 10f;
    public Transform camera;
    public GameObject playerCamTarget;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    /*
    Karekök(h * -2 * g); 
    */
    public float g = -9.81f; // gravity
    public float h = 3f; // height

    Vector3 velocity;
    bool isGrounded;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        Movement(); // Karakter hareket WASD kontrolü
        Jump(); // Karakter zıplama SPACE
        CameraFollow(); // Kamera takip script'i
        GroundChecking(); // Zemin kontrolü

    }

    void CameraFollow(){
        camera.position = playerCamTarget.position + camera;
    }

    void Movement(){
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0f, y).normalized;
        controller.Move(move * characterSpeed * Time.deltaTime);
    }

    void GroundChecking(){ 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;   
        }
        velocity.y += g * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void Jump(){ 
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
            velocity.y = Mathf.Sqrt(h * -2 * g);
        }
    }

}