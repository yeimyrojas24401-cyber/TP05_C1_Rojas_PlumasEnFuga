using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public string name;                     // solo para identificar la capa
    public float speed = 1f; //multiplicador de velocidad del mundo 1 = acera
    public List<Transform> tiles = new List<Transform>();
    public float recycleThresholdX = -29f;  // X local donde el tile sale de pantalla
    public float tileSpacing = 38.4f;       // distancia entre tiles (ancho del sprite)
}