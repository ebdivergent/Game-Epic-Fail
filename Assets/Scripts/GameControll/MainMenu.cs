using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GameEpicFail.Menus
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameObject _findOpponentPanel;
        [SerializeField] GameObject _waitingStatusPanel;
        [SerializeField] Text _waitingStatusText;

        private bool _isConnecting = false;

        private const string GameVersion = "0.1";
        private const int MaxPlayerPerRoom = 2;

        void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

        public void FindOpponent()
        {
            _isConnecting = true;

            _findOpponentPanel.SetActive(false);
            _waitingStatusPanel.SetActive(true);

            _waitingStatusText.text = "Searching...";

            if(PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Master");

            if(_isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            _waitingStatusPanel.SetActive(false);
            _findOpponentPanel.SetActive(true);

            Debug.Log($"Disconnected due to: {cause}");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No oponnet");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Client Successfully joined a room");

            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if(playerCount != MaxPlayerPerRoom)
            {
                _waitingStatusText.text = "Waiting For Opponent";
                Debug.Log("Client is waiting for an opponent");
            }
            else
            {
                _waitingStatusText.text = "Opponent found";

                Debug.Log("Match is ready");
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                _waitingStatusText.text = "Opponent found";
                Debug.Log("Match is ready");

                PhotonNetwork.LoadLevel("Game");
            }
        }
    }

}
