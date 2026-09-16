using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private KeyCode Jump = KeyCode.Space;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpTime = 0.3f; //tiempo maximo que dura en el aire
    private float jumpTimer; //mide cuanto tiempo sostengo la tecla
    private bool isGrounded = false;
    private bool isJumping = false;
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
                                       //dibuja un circulo en feetPos.position con la distancia de ground Distance y checa si hay algo de 

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.linearVelocity = Vector2.up * jumpForce; //agrega una velocidad lineal que sera impulsada por mi fuerza de salto inicial

        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime) 
            {
                rb.linearVelocity = Vector2.up * jumpForce; 
                jumpTimer += Time.deltaTime; //al sumar el time.deltaTime asegura que jumpTimer en algun momento llegue a ser igual al jumpTimer y eso trae por consecuencia que isJumping = false
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
}
