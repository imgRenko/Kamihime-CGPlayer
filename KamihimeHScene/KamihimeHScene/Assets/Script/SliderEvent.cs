using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderEvent : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SliderValue(bool seq)
    {
        DownloadManager manager = DownloadManager.Instance;
        if (seq)
        {
            manager.playerManager.currentSequenceIndex = (int)slider.value;
            manager.talSlider.maxValue = manager.sequenceData.sequence[manager.playerManager.currentSequenceIndex].talk.Length - 1;
            manager.playerManager.currentSubSequenceIndex = 0;
            manager.talSlider.value = 0;
        }
        else
        {
            manager.playerManager.currentSubSequenceIndex = (int)(slider.value);
        }
        manager.ClickAndGo();


    }
}
