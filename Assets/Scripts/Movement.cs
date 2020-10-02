using Photon.Pun;
using UnityEngine;

namespace GameEpicFail.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviourPun
    {
        [SerializeField] float _speed;

        private CharacterController _characterController;


        void Start() => _characterController = GetComponent<CharacterController>();

        // Update is called once per frame
        void Update()
        {
            if(photonView.IsMine)
            {
                TakeIput();
            }
        }

        private void TakeIput()
        {
            var movement = new Vector3
            {
                x = Input.GetAxis("Horizontal"),
                y = 0,
                z = Input.GetAxis("Vertical"),
            }.normalized;

            _characterController.SimpleMove(movement * _speed);
        }
    }
}

