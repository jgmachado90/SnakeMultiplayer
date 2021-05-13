using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        if (_canvas == null)
            _canvas = GetComponent<Canvas>();
    }

    public void ActivateCanvas()
    {
        _canvas.enabled = true;
    }

    public void ActivateCanvasDelayed()
    {
        StartCoroutine(ActivateCanvasCoroutine());
    }

    public IEnumerator ActivateCanvasCoroutine()
    {
        yield return new WaitForSeconds(4f);
        _canvas.enabled = true;
    }
    public void DeactivateCanvas()
    {
        _canvas.enabled = false;
    }
}
