using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StackingSample.Events;
using StackingSample.ScriptableScripts.Stacking;

namespace StackingSample.UI.Windows.InGameWindows.StackingWindow
{
    public class StackPanel : MonoBehaviour
    {
        [field: SerializeField] private StackingObjectDataSO stackingObjectDataSO;
        [field: SerializeField] private Toggle sineWaveFollowToggle;
        [field: SerializeField] private Toggle sineRandomizeSpeedToggle;
        [field: SerializeField] private Slider sineAmplitudeSlider;
        [field: SerializeField] private TextMeshProUGUI sineAmplitudeText;


        private void Awake()
        {
            stackingObjectDataSO.IsFollowTypeSineWave = false;
            sineWaveFollowToggle.isOn = false;
            sineRandomizeSpeedToggle.isOn = false;
            SetSineAmplitudeFirstValues();

            sineWaveFollowToggle.onValueChanged.AddListener(x=> { SetStackingFollowType();});
            sineRandomizeSpeedToggle.onValueChanged.AddListener(x=> { SetSineFollowRandomizeSpeed();});
            sineAmplitudeSlider.onValueChanged.AddListener(x => 
            {
                sineAmplitudeSlider.value = (float)Math.Round(sineAmplitudeSlider.value, 2);
                SetSineAmplitudeText();
                SetSineAmplitudeData(sineAmplitudeSlider.value);
            });
        }

        private void SetStackingFollowType()
        {
            stackingObjectDataSO.IsFollowTypeSineWave = sineWaveFollowToggle.isOn;
            sineRandomizeSpeedToggle.gameObject.SetActive(sineWaveFollowToggle.isOn);
            sineAmplitudeSlider.gameObject.SetActive(sineWaveFollowToggle.isOn);
            StackEvents.OnStackSineFollowWayTriggered?.Invoke(sineWaveFollowToggle.isOn);
        }

        private void SetSineFollowRandomizeSpeed()
        {
            StackEvents.OnStackSineRandomizeSpeedTriggered?.Invoke(sineRandomizeSpeedToggle.isOn);
        }

        private void SetSineAmplitudeFirstValues()
        {
            var SineAmplitude = typeof(StackingObjectDataSO)
                .GetField(nameof(StackingObjectDataSO.SineAmplitude))
                .GetCustomAttribute<RangeAttribute>();

            sineAmplitudeSlider.minValue = SineAmplitude.min;
            sineAmplitudeSlider.maxValue = SineAmplitude.max;
            sineAmplitudeSlider.value = stackingObjectDataSO.SineAmplitude;
            sineAmplitudeText.text = sineAmplitudeSlider.value.ToString();
            StackEvents.OnStackSineAmplitudeValueChanged(stackingObjectDataSO.SineAmplitude);
        }

        private void SetSineAmplitudeText()
        {
            sineAmplitudeText.text = sineAmplitudeSlider.value.ToString();
        }

        private void SetSineAmplitudeData(float value)
        {
            stackingObjectDataSO.SineAmplitude = value;
            StackEvents.OnStackSineAmplitudeValueChanged(stackingObjectDataSO.SineAmplitude);
        }
    }
}
