using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace LivePlay.TechArtTest
{
    /// <summary>
    /// Handles the full reward animation sequence: gift box movement, lid opening, background fade, spotlights, and reward VFX.
    /// </summary>
    public class GiftBoxSequenceController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform rewardTarget;
        [SerializeField] private Transform rewardLid;
        [SerializeField] private CanvasGroup darkBgCanvas;
        [SerializeField] private SpotlightVfxManager spotlightVfxManager;

        [Header("Main Object")]
        [SerializeField] private Transform giftBox;
        [SerializeField] private float rewardScaleTarget = 2f;
        [SerializeField] private float shakeDuration = 0.5f;
        [SerializeField] private Vector3 shakeStrength = new Vector3(10f, 10f, 0);
        [SerializeField] private int shakeVibrato = 20;
        [SerializeField] private float moveToTargetDuration = 0.6f;
        [SerializeField] private float scaleDuration = 0.6f;

        [Header("Background Fade")]
        [SerializeField] private float fadeToAlpha = 0.8f;
        [SerializeField] private float fadeDuration = 0.4f;

        [Header("Reward Elements")]
        [SerializeField] private GameObject rewardObject;
        [SerializeField] private GameObject rewardObjectDescription;
        [SerializeField] private GameObject rewardText;
        [SerializeField] private Vector3 rewardFlyOutOffset = new Vector3(0, 750f, 0);
        [SerializeField] private Vector3 rewardDescFlyOutOffset = new Vector3(0, 750f, 0);
        [SerializeField] private Vector3 textFlyOutOffset = new Vector3(0, 1000f, 0);
        [SerializeField] private float rewardObjectScale = 0.6f;
        [SerializeField] private float rewardVfxScale = 8.5f;
        [SerializeField] private float rewardDescScale = 1.15f;
        [SerializeField] private Vector3 rewardTextScale = new Vector3(1.3f, 1f, 1f);
        [SerializeField] private float rewardAnimationDuration = 0.5f;
        [SerializeField] private float rewardFlyDuration = 1f;

        [Header("Lid Animation")]
        [SerializeField] private Vector3 lidOpenRotation = new Vector3(-90, 0, 0);
        [SerializeField] private Vector3 lidOpenPositionOffset = new Vector3(0, 50, 0);
        [SerializeField] private float lidOpenDuration = 0.6f;
        [SerializeField] private float lidOpenDelay = 0.3f;

        [Header("VFX")]
        [SerializeField] private GameObject vfxGlow;
        [SerializeField] private GameObject particleSystemRewardText;

        private Vector3 originalLidPosition;

        private void Start()
        {
            giftBox.localScale = Vector3.one;
            originalLidPosition = rewardLid.localPosition;

            darkBgCanvas.alpha = 0;
            rewardObject.SetActive(false);
            rewardText.SetActive(false);
        }

        /// <summary>
        /// Starts the full animation sequence of the reward box.
        /// </summary>
        public void PlayRewardAnimation()
        {
            Sequence seq = DOTween.Sequence();

            // Shake → move → scale
            seq.Append(TweenHandler.ShakeAndMove(giftBox, rewardTarget.position, shakeDuration, shakeStrength, shakeVibrato, moveToTargetDuration));
            seq.Join(TweenHandler.ScaleTo(giftBox, Vector3.one * rewardScaleTarget, scaleDuration));
            seq.Append(TweenHandler.FadeCanvasGroup(darkBgCanvas, fadeToAlpha, fadeDuration));

            // Lid open after delay
            seq.AppendInterval(lidOpenDelay);
            seq.Join(TweenHandler.RotateTo(rewardLid, lidOpenRotation, lidOpenDuration));
            seq.Join(TweenHandler.MoveLocalTo(rewardLid, originalLidPosition + lidOpenPositionOffset, lidOpenDuration));

            // Continue with reward effects
            seq.AppendCallback(() =>
            {
                PlayVFX();
                ShowReward();
            });
        }

        /// <summary>
        /// Triggers spotlight visual effects.
        /// </summary>
        private void PlayVFX()
        {
            spotlightVfxManager.PlaySpotlightVfx();
        }

        /// <summary>
        /// Animates the appearance and fly-out motion of reward UI elements and plays particles after completion.
        /// </summary>
        private void ShowReward()
        {
            rewardObject.transform.localScale = Vector3.zero;
            rewardText.transform.localScale = Vector3.zero;

            rewardObject.SetActive(true);
            rewardText.SetActive(true);
            rewardObjectDescription.SetActive(true);

            Sequence spawnSeq = DOTween.Sequence();

            spawnSeq.Append(TweenHandler.ScaleTo(rewardObject.transform, Vector3.one * rewardObjectScale, rewardAnimationDuration));
            spawnSeq.Join(TweenHandler.ScaleTo(rewardObjectDescription.transform, Vector3.one * rewardDescScale, rewardAnimationDuration));
            spawnSeq.Join(TweenHandler.MoveTo(rewardObject.transform, rewardTarget.position + rewardFlyOutOffset, rewardFlyDuration));
            spawnSeq.Join(TweenHandler.MoveTo(rewardObjectDescription.transform, rewardTarget.position + rewardDescFlyOutOffset, rewardFlyDuration));
            spawnSeq.Join(TweenHandler.ScaleTo(rewardText.transform, rewardTextScale, rewardAnimationDuration));
            spawnSeq.Join(TweenHandler.MoveTo(rewardText.transform, rewardTarget.position + textFlyOutOffset, rewardFlyDuration));

            spawnSeq.AppendCallback(() =>
            {
                if (particleSystemRewardText != null)
                {
                    particleSystemRewardText.SetActive(true);
                }
            });
        }
    }
}
