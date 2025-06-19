using System;
using UnityEngine;

public class WormBossMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del gusano
    public float screenMidpointX = 0f; // Coordenada X de la mitad de la pantalla
    public float boundaryRightX = 20f; // Coordenada X del borde derecho
    private bool movingToMidpoint = true; // Direcci�n del movimiento
    private float fixedZ = 30f; // Posici�n fija en el eje Z

    public Animator[] armors; // Array de animadores para las armaduras de animaci�n
    private int currentArmorIndex = 0; // �ndice de la armadura actual
    private float armorChangeInterval = 2f; // Intervalo para cambiar armadura
    private float armorChangeTimer = 0f;

    public Animator animacionBoca;
    public AudioSource mouthAudio;
    private DateTime mouthDtm = DateTime.Now;
    public int secondsMouth = 5;

    public bool muerto = false;
    public int lifes = 1;

    void Start()
    {
        // Inicializa la posici�n del gusano
        transform.position = new Vector3(boundaryRightX, transform.position.y, fixedZ);

        // Activa la primera armadura y desactiva las dem�s
        UpdateArmor();
    }

    void Update()
    {
        muerto = lifes <= 0;
        // Movimiento autom�tico del gusano
        if (!muerto)
        {
            MoveWorm();

            if ((DateTime.Now - mouthDtm).TotalSeconds > secondsMouth)
            {
                mouthDtm = DateTime.Now;
                mouthAudio.Play();
                animacionBoca.SetBool("Abrir", true);
            }
            animacionBoca.SetBool("Abrir", mouthAudio.isPlaying);
            // Cambio autom�tico de armaduras
            HandleArmorChange();
        }
    }

    void MoveWorm()
    {
        float step = speed * Time.deltaTime;

        if (movingToMidpoint)
        {
            // Avanzar hacia la mitad de la pantalla
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(screenMidpointX, transform.position.y, fixedZ), step);

            if (transform.position.x == screenMidpointX)
            {
                movingToMidpoint = false; // Cambiar direcci�n
            }
            //  armors[0].Play(0);
        }
        else
        {
            // Regresar al borde derecho
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(boundaryRightX, transform.position.y, fixedZ), step);

            if (transform.position.x == boundaryRightX)
            {
                movingToMidpoint = true; // Cambiar direcci�n
            }
        }
    }

    void HandleArmorChange()
    {
        // Actualiza el temporizador
        armorChangeTimer += Time.deltaTime;

        if (armorChangeTimer >= armorChangeInterval)
        {
            // Cambia a la siguiente armadura
            currentArmorIndex = (currentArmorIndex + 1) % armors.Length;
            UpdateArmor();

            // Reinicia el temporizador
            armorChangeTimer = 0f;
        }
    }

    void UpdateArmor()
    {
        // Activa solo la armadura correspondiente al �ndice actual
        for (int i = 0; i < armors.Length; i++)
        {
            //   armors[i].gameObject.SetActive(i == currentArmorIndex);
        }
    }

    public void GetHit()
    {
        lifes--;
        muerto = lifes <= 0;
        if (muerto)
        {

        }
    }
}

