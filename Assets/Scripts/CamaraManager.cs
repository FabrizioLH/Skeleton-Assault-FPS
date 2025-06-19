using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    public List<GameObject> Renacer = new List<GameObject>();
    public List<GameObject> Thoth = new List<GameObject>();
    public List<GameObject> Arkhad = new List<GameObject>();
    public List<GameObject> Xyphirion = new List<GameObject>();
    public List<GameObject> Eden = new List<GameObject>();
    public List<GameObject> Urvash = new List<GameObject>();
    public List<GameObject> Varyath = new List<GameObject>();
    public List<GameObject> Zundara = new List<GameObject>();
    public List<GameObject> Nemora = new List<GameObject>();
    public List<GameObject> Noctara = new List<GameObject>();
    public List<GameObject> Solaris = new List<GameObject>();
    public List<GameObject> Skarnbald = new List<GameObject>();
    public List<GameObject> Niflthar = new List<GameObject>();
    public List<GameObject> Vorheim = new List<GameObject>();
    public List<GameObject> Nivaria = new List<GameObject>();
    public List<GameObject> PuenteHades = new List<GameObject>();
    public List<GameObject> VaelZorah = new List<GameObject>();
    public List<GameObject> Nakarath = new List<GameObject>();
    public List<GameObject> XelTharakh = new List<GameObject>();
    public List<GameObject> AzuraThalas = new List<GameObject>();
    public List<GameObject> NyxAelora = new List<GameObject>();
    public List<GameObject> Xirvaelos = new List<GameObject>();
    public List<GameObject> Nokthul = new List<GameObject>();    
    
    public List<Transform> Waypoints = new List<Transform>();
    
    public float travelTime = 2f; // Tiempo en segundos entre cada punto
    private int currentWaypointIndex = 0;
    public int testRuta = -1;
    List<GameObject> puntos = null;
    void Start()
    {
        if(testRuta >-1)
        {
            JuegoManager.currentStage = testRuta;

        }
        
        switch (JuegoManager.currentStage)
        {
            case 0:
                {
                    puntos = Renacer;
                    break;
                }
            case 1:
                {
                    puntos = Thoth;
                    break;
                }
            case 2:
                {
                    puntos = Arkhad;
                    break;
                }
            case 3:
                {
                    puntos = Xyphirion;
                    break;
                }
            case 4:
                {
                    puntos = Eden;
                    break;
                }
            case 5:
                {
                    puntos = Urvash;
                    break;
                }
            case 6:
                {
                    puntos = Varyath;
                    break;
                }
            case 7:
                {
                    puntos = Zundara;
                    break;
                }
            case 8:
                {
                    puntos = Nemora;
                    break;
                }
            case 9:
                {
                    puntos = Noctara;
                    break;
                }
            case 10:
                {
                    puntos = Solaris;
                    break;
                }
            case 11:
                {
                    puntos = Skarnbald;
                    break;
                }
            case 12:
                {
                    puntos = Niflthar;
                    break;
                }
            case 13:
                {
                    puntos = Vorheim;
                    break;
                }
            case 14:
                {
                    puntos = Nivaria;
                    break;
                }
            case 15:
                {
                    puntos = PuenteHades;
                    break;
                }
            case 16:
                {
                    puntos = VaelZorah;
                    break;
                }
            case 17:
                {
                    puntos = Nakarath;
                    break;
                }
            case 18:
                {
                    puntos = XelTharakh;
                    break;
                }
            case 19:
                {
                    puntos = AzuraThalas;
                    break;
                }
            case 20:
                {
                    puntos = NyxAelora;
                    break;
                }
            case 21:
                {
                    puntos = Xirvaelos;
                    break;
                }
            case 22:
                {
                    puntos = Nokthul;
                    break;
                }

        }

        foreach (GameObject t in puntos)
        {
            Waypoints.Add(t.gameObject.transform);
        }
        
        if (Waypoints.Count >= 1)
        {
            StartCoroutine(MoveCamera());
        }
        else
        {
            Debug.LogError("Debes asignar al menos dos puntos de ruta para la c�mara.");
        }
    }

    IEnumerator MoveCamera()
    {
        
        while (currentWaypointIndex < Waypoints.Count-1)
        {
            Transform targetWaypoint = Waypoints[currentWaypointIndex];
            Transform nextWaypoint = Waypoints[(currentWaypointIndex + 1)];
            
            float elapsedTime = 0f;
            Vector3 startPosition = new Vector3(targetWaypoint.position.x, targetWaypoint.position.y,Camera.main.transform.position.z) ;
            Quaternion startRotation = targetWaypoint.rotation;

            while (elapsedTime < travelTime)
            {
                float t = elapsedTime / travelTime;
                transform.position = Vector3.Lerp(startPosition, new Vector3(nextWaypoint.position.x, nextWaypoint.position.y, Camera.main.transform.position.z), t);
                transform.rotation = Quaternion.Lerp(startRotation, nextWaypoint.rotation, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(nextWaypoint.position.x, nextWaypoint.position.y, Camera.main.transform.position.z);
            transform.rotation = nextWaypoint.rotation;

            currentWaypointIndex ++;
        }
        SceneProperties sp = puntos[puntos.Count - 1].GetComponent<SceneProperties>();
        sp.LoadScene();
    }
}
