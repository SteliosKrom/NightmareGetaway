using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LightingEffect : MonoBehaviour
{
    [Header("OTHER")]
    [SerializeField] private ParticleSystem lightingParticle;

    [Header("TYPES")]
    private float startDelay;
    private float stopDelay = 1f;

    private void Start()
    {
        startDelay = Random.Range(30, 60);
        StartCoroutine(StartLightingDelay());
    }

    private IEnumerator StartLightingDelay()
    {
        lightingParticle.Stop();
        yield return new WaitForSeconds(startDelay);
        lightingParticle.Play();
        yield return new WaitForSeconds(stopDelay);
        lightingParticle.Stop();

        while (true)
        {
            if (RoundManager.instance.currentGameState != GameState.pause
                && RoundManager.instance.currentGameState != GameState.onSettingsGame)
            {
                yield return new WaitForSeconds(startDelay);
                lightingParticle.Play();
                yield return new WaitForSeconds(stopDelay);
                lightingParticle.Stop();
            }
            else
            {
                lightingParticle.Stop();
            }
        }
    }
}
