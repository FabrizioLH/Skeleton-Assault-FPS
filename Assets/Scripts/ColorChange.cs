using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    
    public SpriteRenderer _renderer;

    public float colordesde =0;
    public float colorhasta = 1;

    public float escala = 0.1f;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float val = _renderer.color.r + escala;
        Debug.Log(_renderer.color.r);
        Debug.Log(val);
        if (val > colorhasta || val < colordesde)
        { 
            escala*=-1;
        }
        _renderer.color = new Color( _renderer.color.r + escala, _renderer.color.g, _renderer.color.b)  ;
    }
}
