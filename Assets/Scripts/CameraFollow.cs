using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objetivo a seguir
    public Vector3 offset;   // Desplazamiento de la cámara con respecto al objetivo
    public float smoothSpeed = 0.125f; // Velocidad de suavizado

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}

