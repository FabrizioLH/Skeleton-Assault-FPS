using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private bool isPaused = false;

    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI enemigosText;
    

    public PlayerHealth playerHealth;
    [Header("Main Menu")]
public GameObject mainMenuPanel;
public Button btnJugar;
public Button btnSalir;
public GameObject skeletonSpawner;
public GameObject player;


    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }

        // Mostrar el menú y pausar el juego
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0f;

        // Cursor desbloqueado y visible para menú
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Desactivar gameplay
        if (skeletonSpawner != null)
            skeletonSpawner.SetActive(false);
        if (player != null)
            player.SetActive(false);

        // Asignar botones
        btnJugar.onClick.AddListener(ComenzarPartida);
        btnSalir.onClick.AddListener(SalirDelJuego);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PausarJuego();
            else
                ReanudarJuego();
        }
        // Mostrar vida actual del jugador
        if (playerHealth != null)
        {
            vidaText.text = "HP: " + playerHealth.GetCurrentHealth();
        }

        // Contar enemigos activos en la escena (evita contar prefabs)
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        int vivos = 0;

        foreach (GameObject e in enemigos)
        {
            if (e.activeInHierarchy) vivos++;
        }

        enemigosText.text = "Esqueletos: " + vivos;
    }

    void ComenzarPartida()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (skeletonSpawner != null)
            skeletonSpawner.SetActive(true);
        if (player != null)
            player.SetActive(true);
    }

    void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    void PausarJuego()
    {
        isPaused = true;
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ReanudarJuego()
    {
        isPaused = false;
        Time.timeScale = 1f;
        mainMenuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
