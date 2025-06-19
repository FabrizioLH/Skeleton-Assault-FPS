using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
    public CapsuleCollider colisionador;
    public float duration;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plataforma")
        { 
            colisionador.enabled = false;
        }

    }

 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plataforma")
        {
            colisionador.enabled = true;
        }
    }
}
