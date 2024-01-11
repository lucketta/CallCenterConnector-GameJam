using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour {
    public LayerMask colliderMask;
    private float minCollisionDistance = .2f;

    private void Update() {
        //Debug.Log("Running");
        if (Player.Instance.RopeLine != null && Player.Instance.HasCable) {
            UpdateRopePositions();
            LastSegmentGoToPlayerPos();

            DetectCollisionEnter();
            if (Player.Instance.RopePositions.Count > 2) DetectCollisionExits();
        }
    }

    public void GenerateNewCable(Vector3 cablePortLocation) {
        Player.Instance.RopeLine.positionCount = 2;
        Player.Instance.RopeLine.startWidth = 0.1f;
        Player.Instance.RopeLine.endWidth = 0.1f;
        Player.Instance.RopeLine.useWorldSpace = true;

        AddPositionsToRope(cablePortLocation);
    }

    private void AddPositionsToRope(Vector3 pos) {
        Player.Instance.RopePositions.Add(pos);
        Player.Instance.RopePositions.Add(Player.Instance.transform.position); //Always the last pos must be the player

       // Debug.Log("postions[0]: " + Player.Instance.RopePositions[0]);
        //Debug.Log("postions[1]: " + Player.Instance.RopePositions[1]);
    }

    private void UpdateRopePositions() {

        Debug.Log("postions[0]: " + Player.Instance.RopePositions[0]);
        Debug.Log("postions[1]: " + Player.Instance.RopePositions[1]);
        Player.Instance.RopeLine.positionCount = Player.Instance.RopePositions.Count;
        Debug.Log("postions count: " + Player.Instance.RopeLine.positionCount);
        Player.Instance.RopeLine.SetPositions(Player.Instance.RopePositions.ToArray());
    }

    public void UpdateCablePort(Vector3 newCableLocation) {
        //Debug.Log("update cable port: " + newCableLocation);
        Player.Instance.RopeLine.SetPosition(1, newCableLocation);
    }

    private void DetectCollisionEnter() {
        RaycastHit hit;
        if (Physics.Linecast(Player.Instance.transform.position, Player.Instance.RopeLine.GetPosition(Player.Instance.RopePositions.Count - 2), out hit, colliderMask)) {
            // Check for duplicated collision (two collisions at the same place).
            Debug.Log("HIT: " + hit);

            if (System.Math.Abs(Vector3.Distance(Player.Instance.RopeLine.GetPosition(Player.Instance.RopePositions.Count - 2), hit.point)) > minCollisionDistance) {
                Player.Instance.RopePositions.RemoveAt(Player.Instance.RopePositions.Count - 1);
                AddPositionsToRope(hit.point);
            }
        }
    }

    private void DetectCollisionExits() {
        RaycastHit hit;
        if (!Physics.Linecast(Player.Instance.transform.position, Player.Instance.RopeLine.GetPosition(Player.Instance.RopePositions.Count - 3), out hit, colliderMask)) {
            Player.Instance.RopePositions.RemoveAt(Player.Instance.RopePositions.Count - 2);
        }
    }

    private void LastSegmentGoToPlayerPos() {
        Debug.Log("playerpos: " + Player.Instance.RopeLine.transform.position);

        Player.Instance.RopeLine.SetPosition(Player.Instance.RopeLine.positionCount - 1, Player.Instance.transform.position);
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour {

    private void Update() {
        //Debug.Log("Running");
        if (Player.Instance.RopeLine != null && Player.Instance.HasCable) {

            Player.Instance.RopeLine.SetPosition(1, Player.Instance.transform.position);
        }
    }

    public void GenerateNewCable(Vector3 cablePortLocation) {
        Player.Instance.RopeLine.positionCount = 2;
        Player.Instance.RopeLine.SetPosition(0, cablePortLocation);
        Player.Instance.RopeLine.SetPosition(1, Player.Instance.transform.position);
        Player.Instance.RopeLine.startWidth = 0.1f;
        Player.Instance.RopeLine.endWidth = 0.1f;
        Player.Instance.RopeLine.useWorldSpace = true;
    }

    public void UpdateCablePort(Vector3 newCableLocation) {
        //Debug.Log("update cable port: " + newCableLocation);
        Player.Instance.RopeLine.SetPosition(1, newCableLocation);
    }
}
*/