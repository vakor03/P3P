using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class PlayerRadiusText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private PlayerMover playerMover;
        
        private void Start()
        {
            text.text = Player.Instance.JumpNumber.ToString();
            Player.Instance.OnJumpRadiusChanged += PlayerOnJumpRadiusChanged;
            playerMover.OnJumpStarted += PlayerMoverOnJumpStarted;
            playerMover.OnJumpFinished += PlayerMoverOnJumpFinished;
        }

        private void PlayerMoverOnJumpFinished()
        {
            text.enabled = true;
        }

        private void PlayerMoverOnJumpStarted()
        {
            text.enabled = false;
        }

        private void PlayerOnJumpRadiusChanged()
        {
            text.text = Player.Instance.JumpNumber.ToString();
        }
    }
}