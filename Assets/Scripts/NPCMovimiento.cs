using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMovimiento : MonoBehaviour
{
    public int lifes = 1;
    public float speed = 2.0f;
    public float rayDistance = 1.0f; // Distancia del raycast para detectar el borde de un pozo
    public LayerMask groundLayer; // Capa del suelo para detectar pozos
    public Vector3 vector;
    private Rigidbody rb;
    public int direction = 1; // 1 para la derecha, -1 para la izquierda
    public bool muerto = false;

    public GameObject Malla;
    public GameObject blood;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       muerto = lifes <= 0;
       if(!muerto)
        {
            // Movimiento en el eje X
            rb.linearVelocity = new Vector3(speed * direction, rb.linearVelocity.y, rb.linearVelocity.z);

            // Verifica si hay suelo al frente usando un Raycast
            Vector3 rayOrigin = transform.position + vector + Vector3.right * direction * 0.5f;
            RaycastHit hit;

            bool isGroundAhead = Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance, groundLayer);

            // Cambia de direcci�n si no hay suelo al frente o si choca con un obst�culo
            if (!isGroundAhead)
            {
                ChangeDirection();
            }
       }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cambia de direcci�n al chocar con un obst�culo
        if (!collision.gameObject.CompareTag("Floor"))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        direction *= -1; // Invierte la direcci�n
        transform.Rotate(0, 180, 0); // Gira el NPC para mirar en la nueva direcci�n
    }

    void OnDrawGizmos()
    {
        // Dibuja el raycast en la vista de Scene para depuraci�n
        Gizmos.color = Color.red;
        Vector3 rayOrigin = transform.position + Vector3.right * direction * 0.5f;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayDistance);
    }
    public void GetHit(Vector3 posicion)
    {
        lifes--;
        muerto = lifes <= 0;
        if(muerto)
        {
            var bloodEffect = Instantiate(blood,  posicion, Quaternion.identity);
            
            bloodEffect.transform.SetParent(Malla.transform);
            bloodEffect.transform.localScale = new Vector3(1f, 1f, 1f);
            
            var animation = GetComponentInChildren<Animation>();
            animation["Muerte"].speed = 3f;
            animation.Play(animation.GetClip("Muerte").name);
            
            Destroy(rb);
            Destroy(GetComponent<Collider>());
        }

    }

}
