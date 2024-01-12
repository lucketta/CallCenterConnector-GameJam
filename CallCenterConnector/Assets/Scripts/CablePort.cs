using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CablePort : MonoBehaviour
{
    internal bool wasSelected;
    public bool portOccupied;
    [SerializeField] public Material selectedMaterial;
    [SerializeField] public MeshRenderer myRenderer;
    private GameObject Rope;

    public void Interact(Player player) {
        if (!Player.Instance.HasCable && !portOccupied) {
            // Player isn't holding cable so generate new cable
            portOccupied = true;

            Player.Instance.InitializeRopeLine();
            Player.Instance.HasCable = true;
            Player.Instance.RopeRenderer.GenerateNewCable(transform.position);

            Rope = Player.Instance.RopeLine.gameObject;
        }
        else if (Player.Instance.HasCable && !portOccupied) {
            // Player is holding a cable, update new CablePort and remove cable from Player
            portOccupied = true;
            Player.Instance.HasCable = false;
            Player.Instance.RopeRenderer.UpdateCablePort(transform.position);

            Rope = Player.Instance.RopeLine.gameObject;
        }
    }

    public void InteractAlternate(Player player) {
        if (!Player.Instance.HasCable && portOccupied) {
            // Player isn't holding a cable, destroy cable and set cablePorts to false
            CablePort[] cablePortsArray = FindObjectsOfType<CablePort>();

            foreach (CablePort cablePort in cablePortsArray) {
                if (cablePort.Rope == Rope) {
                    cablePort.portOccupied = false;
                }
            }

            Destroy(Rope);
        }
    }
}
