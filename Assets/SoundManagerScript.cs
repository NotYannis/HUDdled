using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static SoundManagerScript Instance;


	public AudioClip alert;
	public AudioClip attackCharacter;
	public AudioClip cristal;
	public AudioClip death;
	public AudioClip enemyHit;
	public AudioClip impactMachine;
	public AudioClip levelUp;
	public AudioClip machineExeplosion;
	public AudioClip errorReload;
	public AudioClip reload;
	public AudioClip refill;
	public AudioClip releaseHeat;
	public AudioClip noFuel;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}


	public void MakeSoundAlert()
	{
		MakeSound(alert);
	}
	public void MakeSoundAttackCharacter()
	{
		MakeSound(attackCharacter);
	}

	public void MakeSoundCristal()
	{
		MakeSound(cristal, 0.75f);
	}

	public void MakeSoundDeath()
	{
		MakeSound(death);
	}

	public void MakeSoundEnemyhit()
	{
		MakeSound(enemyHit);
	}
	public void MakeSoundImpactMachine()
	{
		MakeSound(impactMachine);
	}
	public void MakeSoundLevelUp()
	{
		MakeSound(levelUp);
	}
	public void MakeSoundMachineExplosion()
	{
		MakeSound(machineExeplosion);
	}

	public void MakeSoundErrorReload()
	{
		MakeSound(errorReload);
	}

	public void MakeSoundReload()
	{
		MakeSound(reload);
	}

	public void MakeSoundRefill()
	{
		MakeSound(refill);
	}

	public void MakeSoundReleaseHeat()
	{
		MakeSound(releaseHeat);
	}
	public void MakeSoundNoFuel()
	{
		MakeSound(noFuel);
	}

	private void MakeSound(AudioClip originalClip, float volume = 1.0f)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position, volume);
	}
}
