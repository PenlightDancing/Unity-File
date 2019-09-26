using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffter : MonoBehaviour
{
    public ParticleSystem EffterSystem;

    [SerializeField]
    private List<ParticleSystem> particleSystems;

    // Start is called before the first frame update
    private void Awake()
    {
        particleSystems = new List<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public List<ParticleSystem> GetParticleSystems()
    {
        return particleSystems;
    }

    public void PlayParticle(Vector3 PrePos, Quaternion rotation)
    {
        int count = particleSystems.Count;
        for (int i = 0; i < count; i++)
        {
            if (particleSystems[i].isPlaying == false)
            {
                particleSystems[i].transform.position = PrePos;
                particleSystems[i].transform.rotation = rotation;
                particleSystems[i].Play();
                return;
            }
            else if (i == count - 1)
            {
                AddColorParticle();
            }
        }
    }

    public void AddParticle()
    {
        ParticleSystem particle = Instantiate(EffterSystem);
        particle.transform.parent = transform;
        particleSystems.Add(particle);
    }

    private void AddColorParticle()
    {
        ParticleSystem particle = Instantiate(EffterSystem);
        particle = particleSystems[0];
        particle.transform.parent = transform;
        particleSystems.Add(particle);
    }

    public void ParticleColorChange(PenLight[] penLightDatas, int ButtonCount, int DataValue)
    {
        for (int i = 0; i < particleSystems.Count; i++)
        {
            ParticleSystem.ColorOverLifetimeModule colorOver = particleSystems[i].GetComponent<ParticleSystem>().colorOverLifetime;

            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(penLightDatas[DataValue].RGB.R/255f,
                                                                                     penLightDatas[DataValue].RGB.G/255f,
                                                                                     penLightDatas[DataValue].RGB.B/255f),0f)
                                ,new GradientColorKey(Color.white,1.0f)},
                            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0f), new GradientAlphaKey(1.0f, 1f) });
            colorOver.color = gradient;
        }
    }
}