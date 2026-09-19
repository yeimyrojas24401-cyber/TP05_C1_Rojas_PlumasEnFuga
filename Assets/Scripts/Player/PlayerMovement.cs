using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerDataSo data;
    [Header("Settings")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform feetPos;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private float jumpTimer; //mide cuanto tiempo sostengo la tecla
    private bool isGrounded = false;
    private bool isJumping = false;

    [Header("Crouch Collider")]
    [SerializeField] private BoxCollider2D playerCollider;

    [Header("ParticleSystem")]
    [SerializeField] private ParticleSystem particleFeather;

    private Vector2 normalColliderSize;
    private Vector2 normalColliderOffset;
    private bool isCrouching = false;
    private void Awake()
    {
        data.spriteRenderer.sprite = data.normalSprite;
        normalColliderSize = playerCollider.size;
        normalColliderOffset = playerCollider.offset;
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, data.groundDistance, data.groundLayer);
                                       //dibuja un circulo en feetPos.position con la distancia de ground Distance y checa si hay algo de 
// Jumping
        if (isGrounded && Input.GetKeyDown(data.jump))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.linearVelocity = Vector2.up * data.jumpForce; //agrega una velocidad lineal que sera impulsada por mi fuerza de salto inicial
            particleFeather.Play();
            audioSource.PlayOneShot(data.jumpClip);
        }

        if (isJumping && Input.GetKey(data.jump))
        {
            if (jumpTimer < data.jumpTime) 
            {
                rb.linearVelocity = Vector2.up * data.jumpForce; 
                jumpTimer += Time.deltaTime; //al sumar el time.deltaTime asegura que jumpTimer en algun momento llegue a ser igual al jumpTimer y eso trae por consecuencia que isJumping = false
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(data.jump))
        {
            isJumping = false;
        }
        if (isGrounded && Input.GetKeyDown(data.crouch))
        {
            isCrouching = true;
            data.spriteRenderer.sprite = data.crouchSprite;
            playerCollider.size = data.crouchColliderSize;
            playerCollider.offset = data.crouchColliderOffset;
        }

        if (isCrouching && Input.GetKeyUp(data.crouch))
        {
            isCrouching = false;
            data.spriteRenderer.sprite = data.normalSprite;
            playerCollider.size = normalColliderSize;
            playerCollider.offset = normalColliderOffset;
        }
    }
}
