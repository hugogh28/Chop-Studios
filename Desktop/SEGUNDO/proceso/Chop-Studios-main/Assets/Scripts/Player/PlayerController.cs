using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    //Creación de variables para el movimiento del jugador
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;
    private float baseSpeed;
    private Vector3 movePlayer;

    [Header("Jump")]
    public float gravity = 9.8f;
    public float fallingVelocity; // Se crea una velocidad de caída aislada para permitir una aceleración
    public float jumpForce;

    [Header("Rotation")]
    public float rotationSensibility = 10f;

    [Header("Camera")]
    public Camera mainCamera;

    private float cameraVerticalAngle;

    Vector3 rotationInput = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        baseSpeed = playerSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isFocused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        Look();

        Movement();

        setGravity();

        playerSkill();

        player.Move(movePlayer * Time.deltaTime);//Aplicamos el movimiento al jugador usando Time.deltaTime para una mayor precisión

        Debug.Log(player.velocity.magnitude); //Debugging para comprobar en tiempo de ejecución la velocidad del player
    }

    void Look()//Función destinada a la rotación de la cámara
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);//Limitación al ángulo vertical de la cámara para evitar que esta pueda dar un 360

        transform.Rotate(Vector3.up * rotationInput.x); //Rota al jugador alrededor del eje Y para poder girar a izquierda o derecha
        mainCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f); //Rotación de la cámara en el eje X y con un mayor realismo de la misma
    }

    void Movement()//Función destinada al movimiento del jugador
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove); //Inicializa a 0 la velocidad en el eje vertical y, así como los inputs en los ejes del suelo
        playerInput = Vector3.ClampMagnitude(playerInput, 1); //Evita que se sumen las velocidades al ir en dirección diagonal

        movePlayer = transform.TransformDirection(playerInput); //Transforma la posición del jugador en función del input

        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && player.isGrounded) ? baseSpeed * 1.65f : baseSpeed; //Se actualiza la velocidad máxima del jugador en función de si se presiona la tecla shift o no

        movePlayer = movePlayer * currentSpeed;//Se actualiza el movimiento del jugador por medio de su velocidad actual
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
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallingVelocity = jumpForce;
            movePlayer.y = fallingVelocity;//No se usa Time.deltaTime porque se busca un pulso no una aceleración
        }
    }
 
}
