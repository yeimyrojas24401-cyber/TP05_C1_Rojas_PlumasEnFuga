using UnityEngine;

public class ScoreIncrementer : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private float scorePerSecond = 10f;

    private float accumulator;

    private void Update()
    {
        accumulator += scorePerSecond * Time.deltaTime;
        int wholePoints = Mathf.FloorToInt(accumulator);

        if (wholePoints > 0)
        {
            scoreData.AddScore(wholePoints);
            accumulator -= wholePoints;
        }
    }
}
