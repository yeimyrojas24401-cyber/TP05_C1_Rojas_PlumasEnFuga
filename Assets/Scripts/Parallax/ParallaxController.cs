using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private List<ParallaxLayer> parallaxLayer = new List<ParallaxLayer>();
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    private void Start()
    {
        foreach (var layer in parallaxLayer) //reordena
        {
            layer.tiles.Sort(ComparingX);
        }
    }

    private void Update()
    {
        float factor = obstacleSpawner.SpeedFactor;
        foreach (var layer in parallaxLayer)
        {
            for (int i = 0; i < layer.tiles.Count; i++)
            {
                layer.tiles[i].position += Vector3.left * (layer.speed * factor * Time.deltaTime);
            }
            //sistema reciclado
            if (layer.tiles[0].localPosition.x < layer.recycleThresholdX)
            {
                Transform current = layer.tiles[0];
                Transform target = layer.tiles[^1];

                layer.tiles.Remove(current); //se quita de la lista y los demas detras se recorren
                layer.tiles.Add(current);

                float posX = target.localPosition.x; //posicion a la que se transporta
                current.localPosition = new Vector3(
                    posX + layer.tileSpacing,
                    current.localPosition.y,
                    current.localPosition.z
                );
            }
        }
    }
    private int ComparingX(Transform a, Transform b)
    {
        float xA = a.localPosition.x;
        float xB = b.localPosition.x;

        if (xA < xB)
        {
            return -1;
        }
        else if (xA > xB)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
