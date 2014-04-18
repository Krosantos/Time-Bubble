using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	
	private static AudioManager _instance;

	public static AudioManager instance{
		get{
			if (_instance == null){
				_instance = GameObject.FindObjectOfType<AudioManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	private List<AudioSource> audioSources = new List<AudioSource>();

	public AudioClip menusound, music;

	void Awake(){
		if (_instance == null){
			_instance = this;
			DontDestroyOnLoad(this);
		}else{
			if (this != _instance) 
				Destroy(this.gameObject);
		}
	}

	private void PlayClip(AudioClip clip, bool loop){
		foreach (AudioSource source in audioSources){
			if (!source.isPlaying){
				source.loop = loop;
				source.clip = clip;
				source.Play();
				return;
			}
		}

		audioSources.Add(gameObject.AddComponent<AudioSource>());
		audioSources[audioSources.Count-1].loop = loop;
		audioSources[audioSources.Count-1].clip = clip;
		audioSources[audioSources.Count-1].Play();
	}

	public void Play(AudioClip clip, bool loop = false){
		PlayClip(clip, loop);
	}

	public void Stop(AudioClip clip){
		foreach (AudioSource source in audioSources){
			if (source.clip == clip){
				source.Stop();
			}
		}
	}


}
