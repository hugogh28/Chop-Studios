using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideController : MonoBehaviour
{
    [Header ("Movement")]
    //Creaci�n de variables para el movimiento del jugador
    public float horizontalMove; 
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private float baseSpeed;
    private Vector3 movePlayer;

    [Header ("Jump")]
    public float gravity = 9.8f;
    public float fallingVelocity; // Se crea una velocidad de ca�da aislada para permitir una aceleraci�n
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

        playerInput = new Vector3(horizontalMove, 0, verticalMove); //Inicializa a 0 la velocidad en el eje vertical y, as� como los inputs en los ejes del suelo
        playerInput = Vector3.ClampMagnitude(playerInput, 1); //Evita que se sumen las velocidades al ir en direcci�n diagonal

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward; //Movimiento del jugador relativo a la c�mara

        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && player.isGrounded) ? baseSpeed * 1.65f : baseSpeed; //Se actualiza la velocidad m�xima del jugador en funci�n de si se presiona la tecla shift o no

        movePlayer = movePlayer * currentSpeed;//Se actualiza el movimiento del jugador por medio de su velocidad actual

        if (movePlayer != Vector3.zero) 
        {
            //Con esto logramos un movimiento m�s natural del jugador y que no simplemente parezca rotarse instant�neamente
            Quaternion targetRotation = Quaternion.LookRotation(movePlayer);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        setGravity();//Aplicaci�n de la gravedad

        playerSkill();

        player.Move(movePlayer * Time.deltaTime);//Aplicamos el movimiento al jugador usando Time.deltaTime para una mayor precisi�n

        Debug.Log(player.velocity.magnitude); //Debugging para comprobar en tiempo de ejecuci�n la velocidad del player
    }

    void camDirection() //Funci�n destinada a determinar la direcci�n a la que mira nuestra c�mara
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void setGravity()//Funci�n destinada a aplicar una velocidad de ca�da al player, es decir, la existencia de gravedad
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

    void playerSkill()//Funci�n destinada a las habilidades del player
    {
        if(player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallingVelocity = jumpForce;
            movePlayer.y = fallingVelocity;//No se usa Time.deltaTime porque se busca un pulso no una aceleraci�n
        }
    }

}
