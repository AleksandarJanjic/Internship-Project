using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem dustParticles;

    public void DustCloud()
    {
        dustParticles.Play();
    }
}
