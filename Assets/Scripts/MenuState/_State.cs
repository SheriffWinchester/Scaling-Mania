using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the parent class to all states
public class _State : MonoBehaviour
{
    public StateController.MenuState state { get; protected set; }
    public StateController.GlobalState globalState { get; protected set; }
    protected StateController stateController;
    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    public virtual void InitMenuState(StateController stateController)
    {
        this.stateController = stateController;

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    public virtual void InitGlobalState(StateController stateController)
    {
        this.stateController = stateController;
        //gameObject.SetActive(false);
    }

    public void Show(float duration = 0.3f)
    {
        gameObject.SetActive(true);
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvas(0f, 1f, duration, true));
    }

    public void Hide(float duration = 0.5f)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvas(1f, 0f, duration, false));
    }

    private IEnumerator FadeCanvas(float from, float to, float duration, bool enableAfter)
    {
        float elapsed = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = to;

        if (to == 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }


    //Jump back to the menu before it when we press a back button or escape key
    //You have to manually hook up each back-button to this method
    public void JumpBack()
    {
        stateController.JumpBack();
    }

    public void AdjustUI()
    {
        stateController.AdjustUI();
    }
}
