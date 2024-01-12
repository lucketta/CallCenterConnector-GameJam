using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour {

    public LayerMask colliderMask;

    [SerializeField] private float minCollisionDistance = 1f;
    [SerializeField] private float startWidth = .1f;
    [SerializeField] private float endWidth = .1f;
    [SerializeField] private float offsetHeight = .5f;
    [SerializeField] private int maxCornerVertices = 90;

    private void Update() {
        if (Player.Instance.RopeLine != null && Player.Instance.HasCable) {
            UpdateRopePositions();
            LastSegmentGoToPlayerPos();

            DetectCollisionEnter();
            if (Player.Instance.RopePositions.Count > 2) DetectCollisionExits();
        }
    }

    public void GenerateNewCable(Vector3 cablePortLocation) {
        Player.Instance.RopeLine.startWidth = startWidth;
        Player.Instance.RopeLine.endWidth = endWidth;
        Player.Instance.RopeLine.useWorldSpace = true;
        Player.Instance.RopeLine.numCornerVertices = maxCornerVertices;

        AddPositionsToRope(cablePortLocation);
    }

    private void AddPositionsToRope(Vector3 pos) {
        Player.Instance.RopePositions.Add(pos);
        Player.Instance.RopePositions.Add(Player.Instance.transform.position + new Vector3(0, offsetHeight, 0)); //Always the last pos must be the player
    }

    private void UpdateRopePositions() {
        Player.Instance.RopeLine.positionCount = Player.Instance.RopePositions.Count;
        Player.Instance.RopeLine.SetPositions(Player.Instance.RopePositions.ToArray());
    }

    public void UpdateCablePort(Vector3 newCableLocation) {
        Player.Instance.RopeLine.SetPosition(Player.Instance.RopeLine.positionCount - 1, newCableLocation);
    }

    private void DetectCollisionEnter() {
        RaycastHit hit;
        if (Physics.Linecast(Player.Instance.transform.position + new Vector3(0, offsetHeight, 0), Player.Instance.RopeLine.GetPosition(Player.Instance.RopePositions.Count - 2), out hit, colliderMask)) {
            // Check for duplicated collision (two collisions at the same place).

            if (System.Math.Abs(Vector3.Distance(Player.Instance.RopeLine.GetPosition(Player.Instance.RopePositions.Count - 2), hit.point)) > minCollisionDistance) {
                Player.Instance.RopePositions.RemoveAt(Player.Instance.RopePositions.Count - 1);
                AddPositionsToRope(hit.point);
            }
        }
    }

    private void DetectCollisionExits() {
        RaycastHit hit;
        if (!Physics.Linecast(Player.Instance.transform.position + new Vector3(0, offsetHeight, 0), Player.Instance.RopeLine.GetPosition(Player.Instance.RopePositions.Count - 3), out hit, colliderMask)) {
            Player.Instance.RopePositions.RemoveAt(Player.Instance.RopePositions.Count - 2);
        }
    }

    private void LastSegmentGoToPlayerPos() {
        Player.Instance.RopeLine.SetPosition(Player.Instance.RopeLine.positionCount - 1, Player.Instance.transform.position + new Vector3(0, offsetHeight, 0));
    }
}