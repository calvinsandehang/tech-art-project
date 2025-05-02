using UnityEngine;
using UnityEngine.UI;

namespace LivePlay.TechArtTest
{
    /// <summary>
    /// Rotates a UI element (RectTransform) around its Z axis at a constant speed.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UIZRotator : MonoBehaviour
    {
        [Tooltip("Rotation speed in degrees per second.")]
        [SerializeField] private float rotateSpeed = 100f;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            rectTransform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }
}
