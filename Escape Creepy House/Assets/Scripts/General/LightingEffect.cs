using System.Collections;
using UnityEngine;

public class LightingEffect : MonoBehaviour
{
    [Header("OTHER")]
    [SerializeField] private ParticleSystem lightingParticle;

    [Header("TYPES")]
    private float startDelay = 30f;
    private float repeatInterval = 30f;

    private void Start()
    {
        StartCoroutine(StartLightingDelay());
    }

    private IEnumerator StartLightingDelay()
    {
        yield return new WaitForSeconds(startDelay);
        lightingParticle.Play();
        yield return new WaitForSeconds(0.5f);
        lightingParticle.Stop();

        while (RoundManager.instance.currentGameState != GameState.pause 
            && RoundManager.instance.currentGameState != GameState.onSettingsGame)
        {
            yield return new WaitForSeconds(repeatInterval);
            lightingParticle.Play();
            yield return new WaitForSeconds(0.5f);
            lightingParticle.Stop();
        }
    }
}
