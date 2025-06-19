using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 0.5f;
    public int balasPorCargador = 7;
    private int balasRestantes;
    private float tiempoUltimoDisparo;
    private bool estaRecargando = false;

    [Header("Recarga")]
    public float tiempoRecarga = 2f;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip sonidoDisparo;
    public AudioClip sonidoRecarga;

    void Start()
    {
        balasRestantes = balasPorCargador;
    }

    void Update()
    {
        if (estaRecargando)
            return;

        // Recargar con R
        if (Input.GetKeyDown(KeyCode.R) && balasRestantes < balasPorCargador)
        {
            StartCoroutine(Recargar());
            return;
        }

        // Disparo con clic izquierdo o Ctrl
        if (Input.GetButtonDown("Fire1") && Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            if (balasRestantes > 0)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time;
                balasRestantes--;
            }
            else
            {
                StartCoroutine(Recargar());
            }
        }
    }

    void Disparar()
    {
        // Instanciar la bala
        Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);

        // Reproducir sonido
        if (sonidoDisparo != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }

    System.Collections.IEnumerator Recargar()
    {
        estaRecargando = true;

        // Sonido de recarga
        if (sonidoRecarga != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoRecarga);
        }

        yield return new WaitForSeconds(tiempoRecarga);

        balasRestantes = balasPorCargador;
        estaRecargando = false;
    }
}
