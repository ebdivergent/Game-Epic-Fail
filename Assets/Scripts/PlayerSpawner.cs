using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace GameEpicFail
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _playerPref;
        [SerializeField] CinemachineFreeLook _playerCamera;
        void Start()
        {
            var player = PhotonNetwork.Instantiate(_playerPref.name, Vector3.zero, Quaternion.identity);
            _playerCamera.Follow = player.transform;
            _playerCamera.LookAt = player.transform;
        }
    }
}

