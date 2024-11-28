using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class AddEventTrigger : MonoBehaviour
{
    private bool active = true;

    [Header("BUTTONS")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button audioCategoryButton;
    [SerializeField] private Button videoCategoryButton;
    [SerializeField] private Button graphicsCategoryButton;
    [SerializeField] private Button controlsCategoryButon;
    [SerializeField] private Button backToMenuButtonSettings;
    [SerializeField] private Button backToPreviousButton;
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button backToMenuButtonCredits;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseSettingsButton;
    [SerializeField] private Button pauseHomeButton;
    [SerializeField] private Button pauseExitButton;
    [SerializeField] private Button notesButton;
    [SerializeField] private Button backToGameButtonFromNotes;

    [Header("AUDIO")]
    [SerializeField] private AudioSource hoverAudioSource;
    [SerializeField] private AudioClip hoverAudioClip;

    private void Start()
    {
        DOTween.Init();
        DOTween.defaultTimeScaleIndependent = active;

        AttachButtonHoverEventsMenu(playButton);
        AttachButtonHoverEventsMenu(settingsButton);
        AttachButtonHoverEventsMenu(creditsButton);
        AttachButtonHoverEventsMenu(exitButton);
        AttachButtonHoverEventsMenu(notesButton);

        AttachButtonHoverEventsSettings(audioCategoryButton);
        AttachButtonHoverEventsSettings(videoCategoryButton);
        AttachButtonHoverEventsSettings(graphicsCategoryButton);
        AttachButtonHoverEventsSettings(controlsCategoryButon);

        AttachButtonHoverEventsOther(backToGameButton);
        AttachButtonHoverEventsOther(backToGameButtonFromNotes);
        AttachButtonHoverEventsOther(backToMenuButtonSettings);
        AttachButtonHoverEventsOther(backToPreviousButton);
        AttachButtonHoverEventsOther(backToMenuButtonCredits);

        AttachButtonHoverEventsPause(resumeButton);
        AttachButtonHoverEventsPause(pauseSettingsButton);
        AttachButtonHoverEventsPause(pauseHomeButton);
        AttachButtonHoverEventsPause(pauseExitButton);
    }

    public void EnterHoverSoundEffectPause(Transform buttonTransform)
    {
        buttonTransform.DOScale(1f, 0.2f).SetUpdate(active);
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectPause(Transform buttonTransform)
    {
        buttonTransform.DOScale(0.8f, 0.2f).SetUpdate(active);
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

    public void EnterHoverSoundEffectMenu(Transform buttonTransform)
    {
        buttonTransform.DOScale(1.2f, 0.2f).SetUpdate(active);
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectMenu(Transform buttonTransform)
    {
        buttonTransform.DOScale(1f, 0.2f).SetUpdate(active);
    }

    public void EnterHoverSoundEffectSettings(Transform buttonTransform)
    {
        buttonTransform.DOScale(4.5f, 0.2f).SetUpdate(active);
        AudioManager.instance.PlaySound(hoverAudioSource, hoverAudioClip);
    }

    public void ExitHoverSoundEffectSettings(Transform buttonTransform)
    {
        buttonTransform.DOScale(4f, 0.2f).SetUpdate(active);
    }

    public void AttachButtonHoverEventsMenu(Button menuButtons)
    {
        EventTrigger menuTrigger = menuButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry menuEntryEnter = new EventTrigger.Entry();
        menuEntryEnter.eventID = EventTriggerType.PointerEnter;
        menuEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectMenu(menuButtons.transform); });
        menuTrigger.triggers.Add(menuEntryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { ExitHoverSoundEffectMenu(menuButtons.transform); });
        menuTrigger.triggers.Add(entryExit);
    }

    public void AttachButtonHoverEventsSettings(Button settingsButtons)
    {
        EventTrigger settingsTrigger = settingsButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry settingsEntryEnter = new EventTrigger.Entry();
        settingsEntryEnter.eventID = EventTriggerType.PointerEnter;
        settingsEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectSettings(settingsButtons.transform); });
        settingsTrigger.triggers.Add(settingsEntryEnter);

        EventTrigger.Entry settingsEntryExit = new EventTrigger.Entry();
        settingsEntryExit.eventID = EventTriggerType.PointerExit;
        settingsEntryExit.callback.AddListener((data) => { ExitHoverSoundEffectSettings(settingsButtons.transform); });
        settingsTrigger.triggers.Add(settingsEntryExit);
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

    public void AttachButtonHoverEventsPause(Button pauseButtons)
    {
        EventTrigger pauseTrigger = pauseButtons.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pauseEntryEnter = new EventTrigger.Entry();
        pauseEntryEnter.eventID = EventTriggerType.PointerEnter;
        pauseEntryEnter.callback.AddListener((data) => { EnterHoverSoundEffectPause(pauseButtons.transform); });
        pauseTrigger.triggers.Add(pauseEntryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { ExitHoverSoundEffectPause(pauseButtons.transform); });
        pauseTrigger.triggers.Add(entryExit);
    }
}
