using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InterctableManager : MonoBehaviour
{
    public List<Interactables> allInteractables = new List<Interactables>();

    private void Awake()
    {
        allInteractables = FindObjectsOfType<Interactables>().ToList();
    }



}
