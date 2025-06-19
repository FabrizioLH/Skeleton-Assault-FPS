using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBound : MonoBehaviour
{
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {            
              // Obtener el punto de contacto usando la posición del objeto que colisionó
            Vector3 collisionPoint = new Vector3(other.transform.position.x, other.transform.position.y, 29f);      
      
            other.GetComponent<NPCMovimiento>().GetHit(collisionPoint);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.linearVelocity=Vector3.zero;            
        }
    }
}
