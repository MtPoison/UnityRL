using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] public string sprintInputAxis;
    [SerializeField] public string jetpackInputAxis;

    public Slider StaminaSlider;

    Player playerComponent;

    public float staminaRemoveJetpackValue = 0.2f;
    public float staminaRemoveSprintValue = 0.1f;
    public float staminaAddValue = 0.1f;

    public bool isRemoveStamina = false;
    public bool isRemoveStaminaJetpack = false;

    

    private void Start()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            // Si le parent direct existe, récupère le parent de ce parent
            Transform grandParentTransform = parentTransform.parent;

            if (grandParentTransform != null)
            {
                // Si le parent de ce parent existe, on get le Component Player
                playerComponent = grandParentTransform.GetComponentInChildren<Player>();
            }
        }
        else
        {
            Debug.Log("Pas de parent");
        }
    }

/*    private IEnumerator SmoothChangeSliderValue(Slider slider, float targetValue)
    {
        float startValue = slider.value;
        float sleepTime = 0.0f;

        while (sleepTime < targetValue)
        {
            sleepTime += Time.deltaTime;
            float t = Mathf.Clamp01(sleepTime / targetValue);
            slider.value = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }

        slider.value = targetValue;
    }*/

    /*public void SetSlidersValue(string tag, float targetValue)
    {
        foreach (Slider slider in sliders)
        {
            if (slider.CompareTag(tag))
            {
                StartCoroutine(SmoothChangeSliderValue(slider, targetValue));
            }
        }
    }*/

    /*    public float GetSlidersValue(string tag)
        {
            foreach (Slider slider in sliders)
            {
                if (slider.CompareTag(tag))
                {
                    return slider.value;
                }
            }
            return 0;
        }*/

    private void Update()
    {
        if (Input.GetAxisRaw(sprintInputAxis) != 0 && StaminaSlider.value > 0.0f)
        {
            playerComponent.SprintOn();
            if (!isRemoveStamina)
            {
                StartCoroutine(RemoveStamina(StaminaSlider));
            }
        }
        else
        {
            playerComponent.SprintOff();
            isRemoveStamina = false;
        }

        if (Input.GetAxisRaw(jetpackInputAxis) != 0 && StaminaSlider.value > 0.0f)
        {
            playerComponent.PlayerJetpack();
            if (!isRemoveStaminaJetpack)
            {
                StartCoroutine(RemoveStaminaJetpack(StaminaSlider));
            }
        }
        else
        {
            isRemoveStaminaJetpack = false;
        }
    }

    private IEnumerator RemoveStamina(Slider staminaSlider)
    {
        isRemoveStamina = true;
        while (Input.GetAxisRaw(sprintInputAxis) != 0 && StaminaSlider.value > 0.0f)
        {
            float currentStamina = staminaSlider.value;
            currentStamina -= staminaRemoveSprintValue * Time.deltaTime;
            staminaSlider.value = Mathf.Clamp01(currentStamina);
            yield return null;
        }
        isRemoveStamina = false;
    }

    private IEnumerator RemoveStaminaJetpack(Slider staminaSlider)
    {
        isRemoveStaminaJetpack = true;
        while (Input.GetAxisRaw(jetpackInputAxis) != 0 && StaminaSlider.value > 0.0f)
        {
            float currentStamina = staminaSlider.value;
            currentStamina -= staminaRemoveJetpackValue * Time.deltaTime;
            staminaSlider.value = Mathf.Clamp01(currentStamina);
            yield return null;
        }
        isRemoveStaminaJetpack = false;
    }

    private IEnumerator AddStamina()
    {
        yield return new WaitForSeconds(2f);
        float currentStamina = StaminaSlider.value;
        while (currentStamina < 1f)
        {
            // check si le player sprint ou use le jetpack, si c'est le cas break la coroutine
            if (Input.GetAxisRaw(sprintInputAxis) != 0 || Input.GetAxisRaw(jetpackInputAxis) != 0)
            {
                yield break;
            }

            // ajoute du stamina au fur et à mesure
            currentStamina += staminaAddValue * Time.fixedDeltaTime;
            StaminaSlider.value = Mathf.Clamp01(currentStamina);
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (!isRemoveStaminaJetpack && !isRemoveStamina && StaminaSlider.value <= 0f)
        {
            StartCoroutine(AddStamina());
        }
        else if (Input.GetAxisRaw(sprintInputAxis) == 0 && Input.GetAxisRaw(jetpackInputAxis) == 0 && !isRemoveStamina && StaminaSlider.value > 0f)
        {
            float currentStamina = StaminaSlider.value;
            currentStamina += staminaAddValue * Time.fixedDeltaTime;
            StaminaSlider.value = Mathf.Clamp01(currentStamina);
        }
    }
}
