using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreDataSo", menuName = "Data/Game/Score Data")]
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
            OnNewHighScore?.Invoke(highScore); //avisamos que hay un nuevo record Por ahora NO LO TENGO HABILITADO
        }
    }
    public void ResetScore()
    {
        currentScore = 0; //cuando lo llame va a resetear el currentScore
        OnScoreChanged?.Invoke(currentScore); //Da el aviso que ya ha cambiado y que ahora el valor 
                                              //int es 0
    }
    public void LoadHighScore() //traer el record guardado en el disco y lo carga en memoria 
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); //use get int para guardar el int que encuentre bajo
                                                        //el nombre de HighScore, el 0 es mi valor por defecto
    }
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore); //guardamos el valor actual con la clave HighScore
        PlayerPrefs.Save(); //esto fuerza a Unity a que escriba los datos a disco 
    }
}