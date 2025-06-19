using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 20f;
    public int danio = 1;
    public float tiempoVida = 5f;

    void Start()
    {
        Destroy(gameObject, tiempoVida); // Por si no golpea nada
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemigo = other.GetComponent<EnemyHealth>();
            if (enemigo != null)
            {
                enemigo.RecibirDanio(danio);
            }
        }

        Destroy(gameObject); // Destruye la bala al impactar
    }
}
