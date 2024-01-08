using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCablePortVisual : MonoBehaviour
{
    [SerializeField] private CablePort cablePort;
    [SerializeField] private GameObject gameObjVisual;

    private void Start() {
        Player.Instance.OnSelectedCablePortChanged += Player_OnSelectedCablePortChanged;
    }

    private void Player_OnSelectedCablePortChanged(object sender, Player.OnSelectedCablePortChangedEventArgs e) {
        if (e.selectedCablePort == cablePort) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObjVisual.SetActive(true);
    }

    private void Hide() {
        gameObjVisual.SetActive(false);
    }
}
