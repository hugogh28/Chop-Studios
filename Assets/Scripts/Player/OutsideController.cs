using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideController : MonoBehaviour
{
    [Header ("Movement")]
    //Creación de variables para el movimiento del jugador
    public float horizontalMove; 
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private float baseSpeed;
    private Vector3 movePlayer;

    [Header ("Jump")]
    public float gravity = 9.8f;
    public float fallingVelocity; // Se crea una velocidad de caída aislada para permitir una aceleración
    public float jumpForce;

    [Header("Camera")]
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

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

        playerInput = new Vector3(horizontalMove, 0, verticalMove); //Inicializa a 0 la velocidad en el eje vertical y, así como los inputs en los ejes del suelo
        playerInput = Vector3.ClampMagnitude(playerInput, 1); //Evita que se sumen las velocidades al ir en dirección diagonal

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward; //Movimiento del jugador relativo a la cámara

        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && player.isGrounded) ? baseSpeed * 1.65f : baseSpeed; //Se actualiza la velocidad máxima del jugador en función de si se presiona la tecla shift o no

        movePlayer = movePlayer * currentSpeed;//Se actualiza el movimiento del jugador por medio de su velocidad actual

        if (movePlayer != Vector3.zero) 
        {
            //Con esto logramos un movimiento más natural del jugador y que no simplemente parezca rotarse instantáneamente
            Quaternion targetRotation = Quaternion.LookRotation(movePlayer);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        setGravity();//Aplicación de la gravedad

        playerSkill();

        player.Move(movePlayer * Time.deltaTime);//Aplicamos el movimiento al jugador usando Time.deltaTime para una mayor precisión

        Debug.Log(player.velocity.magnitude); //Debugging para comprobar en tiempo de ejecución la velocidad del player
    }

    void camDirection() //Función destinada a determinar la dirección a la que mira nuestra cámara
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void setGravity()//Función destinada a aplicar una velocidad de caída al player, es decir, la existencia de gravedad
    {
        if (player.isGrounded)
        {
            fallingVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallingVelocity;
        }
        else
        {
            fallingVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallingVelocity;
        }
    }

    void playerSkill()//Función destinada a las habilidades del player
    {
        if(player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallingVelocity = jumpForce;
            movePlayer.y = fallingVelocity;//No se usa Time.deltaTime porque se busca un pulso no una aceleración
        }
    }

}
