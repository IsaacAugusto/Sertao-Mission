using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    enum PlatformType {Start, Finish}

    [SerializeField] private PlatformType _platformType;
    [SerializeField] private GameObject _levelCompleteUI;
    private bool _playerIsGrounded = false;
    private bool _checked = false;

    private void FinishLevel()
    {
        if (_levelCompleteUI != null)
        {
            _levelCompleteUI.SetActive(true);
        }
        GameManager.Instance.UpdateScore();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _playerIsGrounded = true;
        if (_platformType == PlatformType.Finish)
        {
            if (collision.transform.tag == "ShipLanding" && !_checked)
                StartCoroutine(CheckLanding(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerIsGrounded = false;
    }

    IEnumerator CheckLanding(GameObject ship)
    {
        yield return new WaitForSeconds(3);
        if (_playerIsGrounded && ship != null)
        {
            StopAllCoroutines();
            _checked = true;
            FinishLevel();
        }
        StopAllCoroutines();
    }
}
