using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CrystalBehaviour : MonoBehaviour
{
     void OnMouseDown()
    {
        Debug.Log("aai");
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }

}