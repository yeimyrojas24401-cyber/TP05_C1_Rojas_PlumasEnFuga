using UnityEngine;
[CreateAssetMenu(fileName = "WorldSpeedSo", menuName = "Data/Game/WorldSpeedData")]
public class WorldSpeedSo : ScriptableObject
{
    private float currentSpeed;

    public float CurrentSpeed => currentSpeed;
    public void ResetSpeed(float startSpeed)
    {
        currentSpeed = startSpeed;
    }

    public void SetCurrentSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
