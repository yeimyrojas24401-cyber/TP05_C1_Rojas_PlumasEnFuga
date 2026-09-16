using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private KeyCode Jump = KeyCode.Space;
    [SerializeField] private KeyCode Crouch = KeyCode.S;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpTime = 0.3f; //tiempo maximo que dura en el aire
    private float jumpTimer; //mide cuanto tiempo sostengo la tecla
    private bool isGrounded = false;
    private bool isJumping = false;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite crouchSprite;

    [Header("Crouch Collider")]
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Vector2 crouchColliderSize;
    [SerializeField] private Vector2 crouchColliderOffset;

    private Vector2 normalColliderSize;
    private Vector2 normalColliderOffset;
    private bool isCrouching = false;
    private void Awake()
    {
        spriteRenderer.sprite = normalSprite;
        normalColliderSize = playerCollider.size;
        normalColliderOffset = playerCollider.offset;
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
                                       //dibuja un circulo en feetPos.position con la distancia de ground Distance y checa si hay algo de 
// Jumping
        if (isGrounded && Input.GetKeyDown(Jump))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.linearVelocity = Vector2.up * jumpForce; //agrega una velocidad lineal que sera impulsada por mi fuerza de salto inicial

        }

        if (isJumping && Input.GetKey(Jump))
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

        if (Input.GetKeyUp(Jump))
        {
            isJumping = false;
        }
        if (isGrounded && Input.GetKeyDown(Crouch))
        {
            isCrouching = true;
            spriteRenderer.sprite = crouchSprite;
            playerCollider.size = crouchColliderSize;
            playerCollider.offset = crouchColliderOffset;
        }

        if (isCrouching && Input.GetKeyUp(Crouch))
        {
            isCrouching = false;
            spriteRenderer.sprite = normalSprite;
            playerCollider.size = normalColliderSize;
            playerCollider.offset = normalColliderOffset;
        }
    }
}
