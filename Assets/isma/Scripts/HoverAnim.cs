using UnityEngine;
using UnityEngine.EventSystems;

public class HoverItemPreviewSmooth : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Idle")]
    public float levitationAmplitude = 0.2f;
    public float levitationSpeed = 2f;
    public float rotationSpeed = 40f;

    [Header("Zoom manual")]
    public Transform zoomTargetTransform; // Arrastra aqu� el transform de la posici�n de destino
    public float moveSmoothness = 5f;
    public float scaleSmoothness = 5f;
    public float scaleMultiplier = 1.15f;

    [Header("Textos")]
    public GameObject textoArriba;
    public GameObject textoAbajo;
    public DialogueShop dialogue;

    [Header("Indice de texto")]
    public int lineIndex;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 originalEuler;
    private bool isHovering = false;
    private float floatOffset;

    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalEuler = transform.eulerAngles;
        floatOffset = Random.Range(0f, Mathf.PI * 2);

        if (textoArriba) textoArriba.SetActive(false);
        if (textoAbajo) textoAbajo.SetActive(false);
    }

    void Update()
    {
        // Levita siempre (idle)
        float floatY = Mathf.Sin(Time.time * levitationSpeed + floatOffset) * levitationAmplitude;
        Vector3 levitatedPosition = transform.position + new Vector3(0, floatY, 0) * Time.deltaTime;

        // Rota siempre (idle)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Target position
        Vector3 targetPosition = isHovering && zoomTargetTransform != null ? zoomTargetTransform.position : originalPosition;
        transform.position = Vector3.Lerp(levitatedPosition, targetPosition, Time.deltaTime * moveSmoothness);

        // Escalado
        Vector3 targetScale = isHovering ? originalScale * scaleMultiplier : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSmoothness);

        // Rotaci�n de vuelta si no hay hover (opcional)
        if (isHovering)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, originalEuler, Time.deltaTime * moveSmoothness);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (textoArriba) textoArriba.SetActive(true);
        if (textoAbajo) textoAbajo.SetActive(true);
        if (dialogue) dialogue.StartRead(lineIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (textoArriba) textoArriba.SetActive(false);
        if (textoAbajo) textoAbajo.SetActive(false);
        if (dialogue) dialogue.SkipLine();
    }

    public void ForzarHover(bool estado)
    {
        isHovering = estado;

        if (textoArriba) textoArriba.SetActive(estado);
        if (textoAbajo) textoAbajo.SetActive(estado);
    }

}