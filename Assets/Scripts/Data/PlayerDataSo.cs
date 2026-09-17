using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Game/PlayerData")]
public class PlayerDataSo : ScriptableObject
{
    [Header("MoveSettings")]
    public KeyCode jump = KeyCode.W;
    public KeyCode crouch = KeyCode.S;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float jumpTime = 0.3f;
}
