using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreDataSo", menuName = "Game/Score Data")]
public class ScoreData : ScriptableObject
{
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;

    public int CurrentScore => currentScore; // => esto significa que cualquiera
                                             // de afuera puede leerlo pero no cambiarlo
    public int HighScore => highScore;

    public UnityAction<int> OnScoreChanged; //cuando reciba el int podra ejecutar el evento
    public UnityAction<int> OnNewHighScore;

    public void AddScore(int amount)
    {
        currentScore += amount; //suma los puntos 
        OnScoreChanged?.Invoke(currentScore); //cuando currentScore cambia derivado de la suma de amount 
                                              // eso desenvoca que Invoke de la senal para que se ejecute OnScoreChanged
                                              // y les pase el int que estan esperando. El int que les pasara es el 
                                              //currentScore 
        if (currentScore > highScore) //si superamos el record
        {
            highScore = currentScore; //actualizamos
            OnNewHighScore?.Invoke(highScore); //avisamos que hay un nuevo record
        }


    }
}