using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] public string sprintInputAxis;
    [SerializeField] public string jetpackInputAxis;

    public Slider sprintStaminaSlider;
    public Slider jetpackStaminaSlider;

    /*public List<Slider> sliders = new List<Slider>();*/

    Player playerComponent;

    public float staminaRemoveValue = 0.1f;
    public float staminaAddValue = 0.1f;

    public bool isRemoveStamina = false;
    public bool isRemoveStaminaJetpack = false;

    

    private void Start()
    {
        /*SliderInitialization();*/
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

/*    private void SliderInitialization()
    {
        sliders.AddRange(FindObjectsOfType<Slider>());
    }*/

    private IEnumerator SmoothChangeSliderValue(Slider slider, float targetValue)
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
    }

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
        if (Input.GetAxisRaw(sprintInputAxis) != 0 && sprintStaminaSlider.value > 0.0f)
        {
            Debug.Log("sprint");
            playerComponent.SprintOn();
            if (!isRemoveStamina)
            {
                StartCoroutine(RemoveStamina(sprintStaminaSlider));
            }
        }
        else
        {
            playerComponent.SprintOff();
            isRemoveStamina = false;
        }

        if (Input.GetAxisRaw(jetpackInputAxis) != 0 && jetpackStaminaSlider.value > 0.0f)
        {
            Debug.Log("jetpack" + jetpackStaminaSlider.value);
            playerComponent.PlayerJetpack();
            if (!isRemoveStaminaJetpack)
            {
                StartCoroutine(RemoveStaminaJetpack(jetpackStaminaSlider));
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
        while (Input.GetAxisRaw(sprintInputAxis) != 0)
        {
            float currentStamina = staminaSlider.value;
            currentStamina -= staminaRemoveValue * Time.deltaTime;
            staminaSlider.value = Mathf.Clamp01(currentStamina);
            yield return null;
        }
        isRemoveStamina = false;
    }

    private IEnumerator RemoveStaminaJetpack(Slider staminaSlider)
    {
        isRemoveStaminaJetpack = true;
        while (Input.GetAxisRaw(jetpackInputAxis) != 0)
        {
            float currentStamina = staminaSlider.value;
            currentStamina -= staminaRemoveValue * Time.deltaTime;
            staminaSlider.value = Mathf.Clamp01(currentStamina);
            yield return null;
        }
        isRemoveStaminaJetpack = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw(sprintInputAxis) == 0)
        {
            float currentStamina = sprintStaminaSlider.value;
            currentStamina += staminaAddValue * Time.fixedDeltaTime;
            sprintStaminaSlider.value = Mathf.Clamp01(currentStamina);
        }

        if (Input.GetAxisRaw(jetpackInputAxis) == 0)
        {
            float currentStamina = jetpackStaminaSlider.value;
            currentStamina += staminaAddValue * Time.fixedDeltaTime;
            jetpackStaminaSlider.value = Mathf.Clamp01(currentStamina);
        }
    }
}
