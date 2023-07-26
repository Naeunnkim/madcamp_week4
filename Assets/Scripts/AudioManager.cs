using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic; // 배경 음악을 Inspector에서 할당할 오디오 파일
    private float musicTime = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(audioSource!=null && backgroundMusic!=null) {
            audioSource.clip = backgroundMusic;
            audioSource.time = musicTime;
            audioSource.Play();
        }
    }

    private void Update() {
        if(audioSource !=null && audioSource.isPlaying) {
            musicTime = audioSource.time;
        }
    }
}
