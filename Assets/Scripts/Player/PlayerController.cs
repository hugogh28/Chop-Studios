using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Creación de variables para el movimiento del jugador
    public float horizontalMove; 
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private float baseSpeed;
    private Vector3 movePlayer;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public float keyCode;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        baseSpeed = playerSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? baseSpeed * 1.65f : baseSpeed;

        if(movePlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movePlayer);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        player.Move(movePlayer * currentSpeed * Time.deltaTime);

        Debug.Log(player.velocity.magnitude);
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
}
