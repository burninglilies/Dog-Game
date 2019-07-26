using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public string renderer = GetComponent<Renderer>();


    void Start()
    {
        
    }

    void OnMouseEnter()
    {
        renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.black;
    }

    void Update()
    {
        
    } 
}
