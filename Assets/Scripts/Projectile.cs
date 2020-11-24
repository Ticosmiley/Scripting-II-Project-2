using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ITargetable _target;
    int _damage;
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetTarget(ITargetable target, int damage)
    {
        Debug.Log("setting target");
        _target = target;
        _damage = damage;
        StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        Debug.Log("coroutine");
        Vector3 pos = _target.GetPosition();
        int elapsedFrames = 0;
        while (transform.position != pos)
        {
            float t = (float)elapsedFrames / 360f;
            transform.position = Vector3.Lerp(transform.position, pos, t);
            elapsedFrames++;
            yield return null;
        }

        IDamageable damageable = _target as IDamageable;
        damageable.TakeDamage(_damage);

        _audioSource.Play();
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(_audioSource.clip.length);

        Destroy(gameObject);
    }
}
