using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Game/PlayerData")]
public class PlayerDataSo : ScriptableObject
{
    [Header("MoveSettings")]
    public KeyCode[] jumpKeys = { KeyCode.W, KeyCode.Space, KeyCode.UpArrow };
    public KeyCode[] crouchKeys = { KeyCode.S, KeyCode.DownArrow };

    [Header("Jumping")]
    public float jumpForce = 10f; //fuerza de salto
    public float jumpTime = 0.3f; //tiempo maximo que dura en el aire

    [Header("Ground")]
    public LayerMask groundLayer;
    public float groundDistance = 0.25f;

    [Header("Audio")]
    public AudioClip jumpClip;

    [Header("Visuals")]
    public Sprite normalSprite;
    public Sprite crouchSprite;

    [Header("Crouch Collider")]
    public Vector2 crouchColliderSize;
    public Vector2 crouchColliderOffset;

    [Header("Health")]
    public int startingLives = 1;
    public int maxLives = 3;
    public float invulnerabilityAfterHit = 1.5f;
}
