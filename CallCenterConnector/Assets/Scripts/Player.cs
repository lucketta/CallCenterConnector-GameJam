using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCablePortChangedEventArgs> OnSelectedCablePortChanged;
    public class OnSelectedCablePortChangedEventArgs : EventArgs {
        public CablePort selectedCablePort;
    }

    [SerializeField] private float interactMaxDistance = 2f;
    [SerializeField] private float playerHeight = 1.35f;
    [SerializeField] private LayerMask cablePortLayerMask;

    private StarterAssetsInputs _input;
    private FirstPersonController firstPersonController;
    private Vector3 lastInteractDir;
    private CablePort selectedCablePort;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player Instance!");
        }

        Instance = this;
    }


    private void Start() {
        _input = GetComponent<StarterAssetsInputs>();

        firstPersonController = GetComponent<FirstPersonController>();
        firstPersonController.OnInteractAction += _input_OnInteractAction;
    }

    private void Update() {
        
        Vector3 forward = Camera.main.transform.forward * 10;
        Debug.DrawRay(transform.position + new Vector3(0f, playerHeight, 0f), forward, Color.red);

        HandleInteractions();        
    }

    private void HandleInteractions() {
        Vector3 inputDirection = Camera.main.transform.forward * 10;

        if (inputDirection != Vector3.zero) {
            lastInteractDir = inputDirection;
        }

        if (Physics.Raycast(transform.position + new Vector3(0f, playerHeight, 0f), lastInteractDir, out RaycastHit raycastHit, interactMaxDistance, cablePortLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out CablePort cablePort)) {
                // Has Cable Port
                if (cablePort != selectedCablePort) {
                    SetSelectedCablePort(cablePort);
                }
            }
            else {
                SetSelectedCablePort(null);
            }
        }
        else {
            SetSelectedCablePort(null);
        }
    }

    private void _input_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCablePort != null) {
            selectedCablePort.Interact(this);
        }
    }

    private void SetSelectedCablePort(CablePort selectedCablePort) {
        this.selectedCablePort = selectedCablePort;

        OnSelectedCablePortChanged?.Invoke(this, new OnSelectedCablePortChangedEventArgs {
            selectedCablePort = selectedCablePort
        });
    }
}
