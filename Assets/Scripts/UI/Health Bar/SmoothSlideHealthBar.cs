using System.Collections;
using UnityEngine;

class SmoothSlideHealthBar : SlideHealthBar
{
    [SerializeField, Min(.1f)] private float _smoothSlideDelta = 0.5f;

    private Coroutine _coroutine;

    protected override void RefreshData()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothChangeSliderValue(_health.Amount));
    }

    private IEnumerator SmoothChangeSliderValue(float target)
    {
        target /= _health.MaxAmount;

        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards
                (_slider.value, target, _smoothSlideDelta * Time.deltaTime);

            yield return null;
        }
    }
}