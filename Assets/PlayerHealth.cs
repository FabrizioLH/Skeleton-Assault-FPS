using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Audio")]
    public AudioClip sonidoDaño;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Jugador recibe daño. HP ahora es: " + currentHealth);
        // 🔊 Reproduce sonido de daño
        if (sonidoDaño != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDaño);
        }

        Debug.Log("Jugador recibe daño. HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
