using TMPro;
using UnityEngine;

namespace _Scripts.Units.Players
{
    public class PlayerRadiusText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private PlayerMover playerMover;

        private IJumpRadiusController _jumpRadiusController;
        private void Start()
        {
            _jumpRadiusController = Player.Instance.JumpRadiusController;
            
            text.text = _jumpRadiusController.JumpNumber.ToString();
            _jumpRadiusController.OnJumpRadiusChanged += PlayerOnJumpRadiusChanged;
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
            text.text = _jumpRadiusController.JumpNumber.ToString();
        }
    }
}