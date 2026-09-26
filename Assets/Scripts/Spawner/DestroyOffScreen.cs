using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    [SerializeField] private float destroyX = -15f;
    private void Update()
    {
       if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
