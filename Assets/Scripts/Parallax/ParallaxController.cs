using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public string name; // SOLO LA USO COMO IDENTIFICACION
        public float speed = 1f;
        public List<Transform> tiles = new List<Transform>();

        public float recycleThresholdX = -29f; //posicion X (local) en la que un tile de esta capa se considera fuera de pantalla y entonces debe reciclarse

        public float tileSpacing = 38.4f; //cuando se le debe sumar en X para reciclar el tile lo obtuve retando 48.4 (valor en x del tile fuera de camara) menos 10 valor de X en el tile de la camara
    }

    [SerializeField] private List<ParallaxLayer> layers = new List<ParallaxLayer>();

    private void Start()
    {
        foreach (var layer in layers)
        {
            layer.tiles.Sort((a, b) => a.localPosition.x.CompareTo(b.localPosition.x));
        }
    }

    private void Update()
    {
        foreach (var layer in layers)
        {
            for (int i = 0; i < layer.tiles.Count; i++)
            {
                layer.tiles[i].position += Vector3.left * (layer.speed * Time.deltaTime);
            }

            if (layer.tiles[0].localPosition.x < layer.recycleThresholdX)
            {
                Transform current = layer.tiles[0];
                Transform target = layer.tiles[^1];

                layer.tiles.Remove(current);
                layer.tiles.Add(current);

                float posX = target.localPosition.x;
                current.localPosition = new Vector3(
                    posX + layer.tileSpacing,
                    current.localPosition.y,
                    current.localPosition.z
                );
            }
        }
    }
}
