#region

using TMPro;
using UnityEngine;

#endregion

namespace _Scripts.Units.Players
{
    public class PlayerRadiusText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private IJumpRadiusController _jumpRadiusController;

        private IPlayerMover _playerMover;

        private void Start()
        {
            _jumpRadiusController = Player.Instance.JumpRadiusController;
            _playerMover = Player.Instance.PlayerMover;
            
            text.text = _jumpRadiusController.JumpNumber.ToString();
            _jumpRadiusController.OnJumpRadiusChanged += PlayerOnJumpRadiusChanged;
            _playerMover.OnJumpStarted += PlayerMoverOnJumpStarted;
            _playerMover.OnJumpFinished += PlayerMoverOnJumpFinished;
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
            text.text = _jumpRadiusController.JumpNumber.ToString();
        }
    }
}