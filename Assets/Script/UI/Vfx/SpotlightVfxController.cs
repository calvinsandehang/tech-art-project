using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace LivePlay.TechArtTest
{
    /// <summary>
    /// Controls the fill animation of a spotlight UI image with optional fade-in using DOTween.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class SpotlightVfxController : MonoBehaviour
    {
        [Header("Animation Settings")]
        [Tooltip("Duration for the fill animation.")]
        [SerializeField] private float fillDuration = 1.5f;

        [Tooltip("Ease type used for the fill animation.")]
        [SerializeField] private Ease fillEase = Ease.InOutSine;

        private CanvasGroup cgSpotlight;
        private Image imgSpotlight;
        private Tween fillTween;

        private void Awake()
        {
            cgSpotlight = GetComponent<CanvasGroup>();
            imgSpotlight = GetComponent<Image>();

            imgSpotlight.fillAmount = 0f;

            // Ensure spotlight starts invisible
            TweenHandler.FadeCanvasGroup(cgSpotlight, 0f, 0.25f);
        }

        /// <summary>
        /// Fades in and animates the fill amount of the spotlight image from 0 to 1.
        /// </summary>
        public void PlayFillAnimation()
        {
            TweenHandler.FadeCanvasGroup(cgSpotlight, 1f, 0.25f);

            // Kill previous tween if still active
            fillTween?.Kill();

            imgSpotlight.fillAmount = 0f;
            fillTween = imgSpotlight
                .DOFillAmount(1f, fillDuration)
                .SetEase(fillEase);
        }
    }
}
