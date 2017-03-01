using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {

    public ParticleSystem warpel;
    public ParticleSystem warpTrail;
    public ParticleSystem boomBloom;

    private ParticleSystem.EmissionModule warpelEm;
    private ParticleSystem.EmissionModule warpTrailEm;

    public float deathCountdown = -1f;

    private Warper warper;

    private void Awake()  
    {
        this.warper = transform.root.GetComponent<Warper>();
        this.warpelEm = warpel.emission;
        this.warpTrailEm = warpTrail.emission;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(deathCountdown < 0f)
        {
            warpelEm.enabled = false;
            warpTrailEm.enabled = false;
            this.boomBloom.Emit(this.boomBloom.main.maxParticles);
            deathCountdown = this.boomBloom.main.startLifetime.constant;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(deathCountdown >= 0f)
        {
            deathCountdown -= Time.deltaTime;
            if(deathCountdown <= 0f) 
            {
                deathCountdown = -1f;
                warpelEm.enabled = true;
                warpTrailEm.enabled = true;
                warper.Fade();
            }
        }		
	}
}
