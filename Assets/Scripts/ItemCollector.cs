using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    // < >
    private int cherries = 0;

    [SerializeField] private Text textoPuntos;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
    if(collision.gameObject.CompareTag("Cherry"))
    {
        Destroy(collision.gameObject);
        cherries++;
        textoPuntos.text = "Puntos: " + cherries;
    }
   }
}
