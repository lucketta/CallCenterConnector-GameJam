using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CablePort : MonoBehaviour
{
    internal bool wasSelected;
    [SerializeField] public Material selectedMaterial;
    [SerializeField] public MeshRenderer myRenderer;

    public void Interact(Player player) {
        Debug.Log("Cable Port Interacted!");
    }
}
