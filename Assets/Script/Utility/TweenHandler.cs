using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LivePlay.TechArtTest
{
    /// <summary>
    /// Centralized helper class for reusable tween animations using DOTween.
    /// </summary>
    public static class TweenHandler
    {
        /// <summary>
        /// Fades a CanvasGroup's alpha and updates its interactivity.
        /// </summary>
        public static Tween FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration, Ease ease = Ease.InOutSine)
        {
            return canvasGroup.DOFade(targetAlpha, duration)
                .SetEase(ease)
                .OnStart(() =>
                {
                    if (targetAlpha > 0f)
                    {
                        canvasGroup.interactable = false;
                        canvasGroup.blocksRaycasts = false;
                    }
                })
                .OnComplete(() =>
                {
                    bool enable = targetAlpha > 0f;
                    canvasGroup.interactable = enable;
                    canvasGroup.blocksRaycasts = enable;
                });
        }

        /// <summary>
        /// Shakes a Transform's position, then moves it to the given destination.
        /// </summary>
        public static Tween ShakeAndMove(Transform target, Vector3 destination, float shakeDuration, Vector3 shakeStrength, int vibrato, float moveDuration)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(target.DOShakePosition(shakeDuration, strength: shakeStrength, vibrato: vibrato));
            seq.Append(target.DOMove(destination, moveDuration).SetEase(Ease.InOutQuad));
            return seq;
        }

        /// <summary>
        /// Scales a Transform to a given scale with easing.
        /// </summary>
        public static Tween ScaleTo(Transform target, Vector3 targetScale, float duration, Ease ease = Ease.OutBack)
        {
            return target.DOScale(targetScale, duration).SetEase(ease);
        }

        /// <summary>
        /// Rotates a Transform to a local rotation.
        /// </summary>
        public static Tween RotateTo(Transform target, Vector3 localRotation, float duration, Ease ease = Ease.OutBounce)
        {
            return target.DOLocalRotate(localRotation, duration).SetEase(ease);
        }

        /// <summary>
        /// Moves a Transform to a world position.
        /// </summary>
        public static Tween MoveTo(Transform target, Vector3 position, float duration, Ease ease = Ease.OutQuad)
        {
            return target.DOMove(position, duration).SetEase(ease);
        }

        /// <summary>
        /// Moves a Transform to a local position.
        /// </summary>
        public static Tween MoveLocalTo(Transform target, Vector3 localPosition, float duration, Ease ease = Ease.OutBack)
        {
            return target.DOLocalMove(localPosition, duration).SetEase(ease);
        }

        /// <summary>
        /// Fades a CanvasGroup in (to alpha 1).
        /// </summary>
        public static Tween FadeIn(CanvasGroup group, float duration)
        {
            return group.DOFade(1f, duration).SetEase(Ease.InOutSine);
        }

        /// <summary>
        /// Fades a CanvasGroup out (to alpha 0).
        /// </summary>
        public static Tween FadeOut(CanvasGroup group, float duration)
        {
            return group.DOFade(0f, duration).SetEase(Ease.InOutSine);
        }
    }
}
