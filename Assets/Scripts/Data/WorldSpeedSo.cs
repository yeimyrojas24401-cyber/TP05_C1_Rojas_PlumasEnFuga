using UnityEngine;
[CreateAssetMenu(fileName = "WorldSpeedSo", menuName = "Data/Game/WorldSpeedData")]
public class WorldSpeedSo : ScriptableObject
{
    private float baseSpeed;
    private float currentSpeed;

    public float CurrentSpeed => currentSpeed;
    public float SpeedFactor => baseSpeed > 0 ? currentSpeed / baseSpeed : 0;
    public void ResetSpeed(float startSpeed)
    {
        baseSpeed = startSpeed;
        currentSpeed = startSpeed;
    }

    public void SetCurrentSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
