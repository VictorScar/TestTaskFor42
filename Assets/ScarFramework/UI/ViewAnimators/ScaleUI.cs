using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScarFramework.UI.ViewAnimators
{
    [CreateAssetMenu(menuName = "UI/Animators/Scale", fileName = "ScaleUI")]
    public class ScaleUI : UIAnimator
    {
        public float startValue = 1;
        public float endValue;


        protected override Tween AnimateInternal(UIView view)
        {
            Debug.Log("ScaleUI start");
            var animationInternal = DOTween.Sequence();
            animationInternal
                .Append(view.Rect.DOScale(endValue, duration).SetEase(ease));
            return animationInternal;
        }
 

        protected override void OnStartAnimation(UIView view)
        {
            view.Rect.localScale = new Vector3(startValue, startValue, startValue);
        }

        protected override void OnEndAnimation()
        {
            //_view.Rect.localScale = new Vector3(endValue, endValue, endValue);
            Debug.Log("ScaleUI end");
       }
        
        public override UIAnimator GetInstance()
        {
            var instance = CreateInstance<ScaleUI>();
            instance.ease = ease;
            instance.duration = duration;
            instance.startValue = startValue;
            instance.endValue = endValue;
            
            return instance;
        }
    }
}