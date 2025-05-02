using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LivePlay.TechArtTest
{
    /// <summary>
    /// Manages a sequence of spotlight VFX by triggering their fill animations in staggered intervals.
    /// </summary>
    public class SpotlightVfxManager : MonoBehaviour
    {
        [Header("Spotlight References")]
        [Tooltip("List of spotlight controllers to animate in sequence.")]
        [SerializeField] private List<SpotlightVfxController> spotlights = new();
        [SerializeField] private float delayBetweenSpotlights = 0.5f;
        private Coroutine playSpotlightCoroutine;

        /// <summary>
        /// Starts playing the spotlight VFX in sequence. Stops any previous sequence before starting a new one.
        /// </summary>
        public void PlaySpotlightVfx()
        {
            if (playSpotlightCoroutine != null)
            {
                StopCoroutine(playSpotlightCoroutine);
            }

            playSpotlightCoroutine = StartCoroutine(PlaySpotlightVfxCoroutine());
        }

        /// <summary>
        /// Coroutine that plays the spotlight animations one by one with delay.
        /// </summary>
        private IEnumerator PlaySpotlightVfxCoroutine()
        {
            foreach (var spotlight in spotlights)
            {
                spotlight.PlayFillAnimation();
                yield return new WaitForSeconds(delayBetweenSpotlights); // Delay between each spotlight
            }
        }
    }
}
