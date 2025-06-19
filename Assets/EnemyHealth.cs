using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida = 3;

    public void RecibirDanio(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("Enemy recibi� da�o. Vida restante: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Destroy(gameObject);
    }
}
