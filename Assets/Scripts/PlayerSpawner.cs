
using Photon.Pun;
using UnityEngine;

namespace GameEpicFail
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _playerPref;

        void Start() => PhotonNetwork.Instantiate(_playerPref.name, Vector3.zero, Quaternion.identity);
    }
}

