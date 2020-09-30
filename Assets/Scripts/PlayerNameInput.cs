using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEpicFail.Menus
{
    public class PlayerNameInput: MonoBehaviour
    {
        [SerializeField] InputField _inputNickName;
        [SerializeField] Button _connect;


        private const string PlayerPrefsNameKey = "PlayerName";

        void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if(!PlayerPrefs.HasKey(PlayerPrefsNameKey))
                { return; }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
            
            _inputNickName.text = defaultName;

            SetPlayerName(defaultName);
        }

        private void SetPlayerName(string defaultName)
        {
            _connect.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            string playerName = _inputNickName.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
        }
    }
}
