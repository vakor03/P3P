#region

using _Scripts.Units.Players;
using UnityEngine;

#endregion

namespace _Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineVisuals : MonoBehaviour
    {
        [SerializeField] [Range(0, 50)] private int segments = 50;
        [SerializeField] private PlayerMover playerMover;

        private IJumpRadiusController _jumpRadiusController;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _jumpRadiusController = Player.Instance.JumpRadiusController;
            InitLine();
            RenderLine(_jumpRadiusController.JumpRadius);

            _jumpRadiusController.OnJumpRadiusChanged += PlayerOnJumpRadiusChanged;
            playerMover.OnJumpStarted += PlayerOnJumpStarted;
            playerMover.OnJumpFinished += PlayerOnJumpFinished;
        }

        private void PlayerOnJumpFinished()
        {
            Show();
        }

        private void PlayerOnJumpStarted()
        {
            Hide();
        }

        private void PlayerOnJumpRadiusChanged()
        {
            RenderLine(_jumpRadiusController.JumpRadius);
        }

        private void InitLine()
        {
            _lineRenderer.positionCount = segments + 1;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.widthMultiplier = 0.1f;
        }

        private void RenderLine(float radius)
        {
            CreatePoints(radius);
        }

        private void CreatePoints(float radius)
        {
            float angle = 20f;

            for (int i = 0; i < (segments + 1); i++)
            {
                var x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                var y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                _lineRenderer.SetPosition(i, new Vector3(x, 0, y));

                angle += (360f / segments);
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}