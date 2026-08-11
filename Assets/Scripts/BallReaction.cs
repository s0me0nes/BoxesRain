using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BallReaction : MonoBehaviour
{
    private Renderer _renderer;
    private float _deactiveMaxTime = 6;
    private bool _isContacted = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnDisable()
    {
        _isContacted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Plane plane))
        {
            if (!_isContacted)
            {
                _isContacted = true;

                ColorChanger();

                float timer = Random.Range(0, _deactiveMaxTime);
                Invoke(nameof(Hiding), timer);
            }
        }
    }

    private void ColorChanger()
    {
        _renderer.material.color = Color.red;
    }

    private void Hiding()
    {
       gameObject.SetActive(false);
    }
}
