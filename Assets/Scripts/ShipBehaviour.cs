using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CameraScript _cameraScript;
    private ShipSounds _soundScript;
    public bool IsDead = false;
    [SerializeField] private GameObject _destroyParticle;
    [SerializeField] private GameObject _partsParticle;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _hp;
    [SerializeField] private float _propulsionForce;
    private float _relativeVelocity;

    IEnumerator WaitToDestroyTheShip()
    {
        IsDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
        SceneController.Instance.ReloadCurrentScene();
    }

    void Start()
    {
        _cameraScript = FindObjectOfType<CameraScript>();
        _soundScript = FindObjectOfType<ShipSounds>();
        _rb = GetComponent<Rigidbody2D>(); // Get the rigidbody from the components.
        if (_rb == null) // If can't get the rigidbody, sends a error.
            Debug.LogError("Cannot get the game object rigidbody2D");
    }

    void Update()
    {
        if (!IsDead)
            ShipPropulsion();
    }

    private void ShipPropulsion() // Controlls all the ship propulsors and movement.
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            _soundScript.PlayThrustersSound(2);
            GetComponent<Animator>().SetBool("LeftPropulsorOn", true);
            GetComponent<Animator>().SetBool("RightPropulsorOn", true);
            if (_rb.velocity.magnitude <= 10) // If the speed reach the limit, dont add the force.
            {
                _rb.AddForce(transform.up * _propulsionForce);
            }
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _soundScript.PlayThrustersSound(1);
            _rb.AddTorque(-_torqueForce);
            GetComponent<Animator>().SetBool("LeftPropulsorOn", true);
            GetComponent<Animator>().SetBool("RightPropulsorOn", false);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _soundScript.PlayThrustersSound(1);
            _rb.AddTorque(_torqueForce);
            GetComponent<Animator>().SetBool("RightPropulsorOn", true);
            GetComponent<Animator>().SetBool("LeftPropulsorOn", false);
        }

        else
        {
            _soundScript.StopThrustersSound();
            GetComponent<Animator>().SetBool("RightPropulsorOn", false);
            GetComponent<Animator>().SetBool("LeftPropulsorOn", false);
        }
    }

    private void CheckImpact(float velocidade, GameObject landObject)
    {
        if (velocidade > 3 && !IsDead)
        {
            DestroyShip();
        }

        else if (velocidade < 3)
        {
            if (velocidade < 2 && landObject.tag == "Plataform")
            {
                Debug.Log("No Damage on landing");
                return;
            }
            TakeDamage(velocidade);
        }
    }

    public int GetCurrentScore()
    {
        return (int)_hp / 3;
    }

    private void TakeDamage(float damage)
    {
        _hp -= damage * 2;
        if (_hp <= 0 && !IsDead)
        {
            DestroyShip();
        }
    }

    private void DestroyShip()
    {
        _soundScript.PlayDestroyShipSound();
        Instantiate(_destroyParticle, transform.position, Quaternion.identity);
        Instantiate(_partsParticle, transform.position, Quaternion.identity);
        StartCoroutine(WaitToDestroyTheShip());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsDead)
        {
        _relativeVelocity = collision.relativeVelocity.magnitude;
        _cameraScript.CameraShake();
        _soundScript.PlayShipHitSound();
        CheckImpact(_relativeVelocity, collision.gameObject);
        }
    }
}
