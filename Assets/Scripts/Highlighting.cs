using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour
{
 void Awake()
 {
   GetComponent<Renderer>().material.color = Color.red;
 }
}