using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Ball : MonoBehaviour
{
    private Renderer _renderer;
    private Coroutine _hideCoroutine;

    private float _deactiveMaxTime = 6;
    private bool _isContacted = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnDisable()
    {
        _isContacted = false;
        _renderer.material.color = Color.white;
        _hideCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isContacted)
        {
            if (collision.gameObject.TryGetComponent(out Plane plane))
            {
                _isContacted = true;

                ChangeColor();

                float timer = Random.Range(0, _deactiveMaxTime);
                _hideCoroutine = StartCoroutine(HideAfterDelay(timer));
            }
        }
    }

    private IEnumerator HideAfterDelay(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void ChangeColor()
    {
        _renderer.material.color = Color.red;
    }
}
