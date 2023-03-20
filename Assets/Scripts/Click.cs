using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour
{
    public static Action OnClickEnter;

    [SerializeField] private GameObject _particlePanch;
    [SerializeField] private AudioSource _audioPunch;

    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            Vector3 mousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            mousePosition.x = 0;
            mousePosition.z = -6.5f;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                if (!EventSystem.current.IsPointerOverGameObject()) // Луч не проходит, через UI
                    if (hit.collider.tag == "Enemy")
                    {
                        OnClickEnter?.Invoke();
                        _audioPunch.Play();
                        GameObject particle = Instantiate(_particlePanch, mousePosition, Quaternion.identity);
                        Destroy(particle, 0.5f);
                    }                        
        }
    }
}
