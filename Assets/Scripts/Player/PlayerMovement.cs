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

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private string currentAnimState;

    private float jumpTimer;
    private bool isGrounded = false;
    private bool isJumping = false;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Crouch Collider")]
    [SerializeField] private BoxCollider2D playerCollider;

    [Header("ParticleSystem")]
    [SerializeField] private ParticleSystem particleFeather;

    private Vector2 normalColliderSize;
    private Vector2 normalColliderOffset;
    private bool isCrouching = false;

    private void Awake()
    {
        normalColliderSize = playerCollider.size;
        normalColliderOffset = playerCollider.offset;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, data.groundDistance, data.groundLayer);

        // Jumping
        if (isGrounded && Input.GetKeyDown(data.jump))
        {
            isJumping = true;
            jumpTimer = 0f;
            rb.linearVelocity = Vector2.up * data.jumpForce;
            particleFeather.Play();
            audioSource.PlayOneShot(data.jumpClip);
        }

        if (isJumping && Input.GetKey(data.jump))
        {
            if (jumpTimer < data.jumpTime)
            {
                rb.linearVelocity = Vector2.up * data.jumpForce;
                jumpTimer += Time.deltaTime;
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
            playerCollider.size = data.crouchColliderSize;
            playerCollider.offset = data.crouchColliderOffset;
        }

        if (isCrouching && Input.GetKeyUp(data.crouch))
        {
            isCrouching = false;
            playerCollider.size = normalColliderSize;
            playerCollider.offset = normalColliderOffset;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (!isGrounded)
        {
            PlayAnimation("JumpCycle");
        }
        else if (isCrouching)
        {
            PlayAnimation("CrouchCycle");
        }
        else
        {
            PlayAnimation("WalkCycle");
        }
    }

    private void PlayAnimation(string stateName)
    {
        if (currentAnimState == stateName) return; // evita reiniciar la animación cada frame
        animator.Play(stateName);
        currentAnimState = stateName;
    }
}