using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Game/PlayerData")]
public class PlayerDataSo : ScriptableObject
{
    [Header("MoveSettings")]
    public KeyCode jump = KeyCode.W;
    public KeyCode crouch = KeyCode.S;

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
}
