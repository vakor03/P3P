#region

using UnityEngine;

#endregion

namespace _Scripts.Units
{
    public class TestCubeScript : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision");
        }
    }
}