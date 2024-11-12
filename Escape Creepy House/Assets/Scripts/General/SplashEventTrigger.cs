using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SplashEventTrigger : MonoBehaviour
{
    [Header("TYPES")]
    private bool active = true;

    [Header("BUTTON")]
    [SerializeField] private Button continueButton;

    [Header("AUDIO")]
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;

    private void Start()
    {
        DOTween.Init();
        DOTween.defaultTimeScaleIndependent = active;

        AttachButtonHoverEventsOther(continueButton);
    }

    public void EnterHoverSoundEffectOther(Transform buttonTransform)
    {
        buttonTransform.DOScale(3.5f, 0.2f).SetUpdate(active);
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectOther(Transform buttonTransform)
    {
        buttonTransform.DOScale(3.2f, 0.2f).SetUpdate(active);
    }

    public void AttachButtonHoverEventsOther(Button otherButtons)
    {
        EventTrigger otherTrigger = otherButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry otherEntryEnter = new EventTrigger.Entry();
        otherEntryEnter.eventID = EventTriggerType.PointerEnter;
        otherEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectOther(otherButtons.transform); });
        otherTrigger.triggers.Add(otherEntryEnter);

        EventTrigger.Entry otherEntryExit = new EventTrigger.Entry();
        otherEntryExit.eventID = EventTriggerType.PointerExit;
        otherEntryExit.callback.AddListener((data) => ExitHoverSoundEffectOther(otherButtons.transform));
        otherTrigger.triggers.Add(otherEntryExit);
    }
}
