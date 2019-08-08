using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _minCameraDistance;
    [SerializeField] private float _maxCameraDistance;
    private ShipBehaviour _shipScript;
    private Animator anim;
    private float _targetVelocity;
    private Vector3 _offset;

    private void Start()
    {
        _shipScript = FindObjectOfType<ShipBehaviour>();
        anim = GetComponentInChildren<Animator>();
        _offset = Vector3.back * 10;
        _target = FindObjectOfType<ShipBehaviour>().gameObject;
        if (_target == null)
            Debug.LogError("The camera can't find the Ship");
    }

    private void Update()
    {
        if (_shipScript.IsDead)
            _target = null;
    }

    private void FixedUpdate()
    {
        if (_target != null)
            _targetVelocity = _target.GetComponent<Rigidbody2D>().velocity.magnitude;
        CameraFollow();
        CameraSpeedDistance();
    }

    private void CameraFollow()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.transform.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void CameraSpeedDistance()
    {
        float distance;

        distance = Mathf.Lerp(_minCameraDistance, _maxCameraDistance, 0.05f * _targetVelocity);

        GetComponentInChildren<Camera>().orthographicSize = distance;
    }

    public void CameraShake()
    {
        anim.SetTrigger("Shake");
    }
}

