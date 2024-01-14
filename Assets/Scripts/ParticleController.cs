using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    private ParticleSystem ps;
    private bool isInitialized = false;

    public void SetPositionAndPlay(Vector3 StartPos, Vector3 Scale)
    {
        ps = GetComponent<ParticleSystem>();
        transform.position = StartPos;
        transform.localScale = new Vector3(Scale.x/1.5f, Scale.y/1.5f, Scale.z/1.5f);
        isInitialized = true;

        ps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            if (!ps.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
