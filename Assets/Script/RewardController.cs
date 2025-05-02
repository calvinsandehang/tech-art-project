using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.UI;

public class RewardController : MonoBehaviour
{
    public Transform rewardTarget; 
    public Transform rewardLid;
    public GameObject vfxGlow;
    public Graphic darkBgImage;
    public Graphic spotlightImage;
    public float shakeDuration = 0.5f;
    public float shakeStrength = 10f;
    public int shakeVibrato = 10;
    public float moveDuration = 1f;
    public float scaleDuration = 0.5f;
    public float rewardScaleTarget;

    public GameObject rewardObject;
    public GameObject rewardText;
    public Vector3 lidOpenRotation = new Vector3(-90, 0, 0);  
    public Vector3 lidOpenPositionOffset = new Vector3(0, 50, 0);

    private Vector3 originalScale;
    private Vector3 originalLidPosition;
    private Transform originalLidTransform;
    private Transform originalRewardTransform;

    private void Start()
    {
        originalScale = transform.localScale;
        originalLidTransform = rewardLid.transform;
        originalRewardTransform = transform;
    }

    public void PlayRewardAnimation()
    {
        Sequence rewardSequence = DOTween.Sequence();

        rewardSequence.Append(transform.DOShakeRotation(shakeDuration, shakeStrength, shakeVibrato));


       rewardSequence.Append(transform.DOJump(rewardTarget.position, 50, 2, moveDuration)
            .SetEase(Ease.OutQuad));
        rewardSequence.Join(transform.DOScale(originalScale * rewardScaleTarget, moveDuration * 0.6f).SetEase(Ease.OutBack));
        rewardSequence.Append(darkBgImage.DOColor(new Color(0, 0, 0, 0.85f), 0.5f));
        rewardSequence.AppendInterval(0.3f); 

        rewardSequence.AppendInterval(0.2f);
        rewardSequence.Join(rewardLid.DOLocalRotate(lidOpenRotation, 0.6f).SetEase(Ease.OutBack)); 
        rewardSequence.Join(rewardLid.DOLocalMove(originalLidPosition + lidOpenPositionOffset, 0.6f).SetEase(Ease.OutBack));  

        rewardSequence.AppendCallback(() => 
        {
            PlayVFX();
            Reward();
        });
        rewardSequence.Play();
    }

    void PlayVFX()
    {
        vfxGlow.SetActive(true); 
        spotlightImage.DOFade(1f, 0.8f).SetEase(Ease.InOutSine);

        vfxGlow.transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutSine) 
            .SetLoops(-1, LoopType.Restart); 
    }

    public void Reward()
    {
        
       

        rewardObject.transform.localScale = Vector3.zero;
        rewardText.transform.localScale = Vector3.zero;
        
        rewardObject.SetActive(true);
        rewardText.SetActive(true);

        Sequence spawnSequence = DOTween.Sequence();

        spawnSequence.Append(rewardObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        spawnSequence.Join(rewardObject.transform.DOMoveY(rewardTarget.position.y + 750f, 1f).SetEase(Ease.OutQuad));

        spawnSequence.Join(rewardText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        spawnSequence.Join(rewardText.transform.DOMoveY(rewardTarget.position.y + 1000f, 1f).SetEase(Ease.OutQuad));
    


        spawnSequence.Play();
    }

}
