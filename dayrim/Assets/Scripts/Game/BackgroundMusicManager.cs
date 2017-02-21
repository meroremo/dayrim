using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour 
{
    private MusicPlayingController musicPlaying;
    private AudioSource[] music;

    private AudioSource junkyardMusic;
    private AudioSource menuMusic;
    private AudioSource insideMusic;

	void Start () 
    {
        music = GetComponents<AudioSource>();

        junkyardMusic = music[0];
        // um weitere Musik erweitern

        musicPlaying = GetComponent<MusicPlayingController>();

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == SceneNameManager.junkyardFirst || currentSceneName == SceneNameManager.junkyardSecond || currentSceneName == SceneNameManager.dialog)
        {
            if (!musicPlaying.junkyardMusicPlaying)
            {
                musicPlaying.junkyardMusicPlaying = true;
                junkyardMusic.loop = true;
                junkyardMusic.Play();
                //AudioSource.
                // junkyardMusic.loop = true;
                // junkyardMusic.Play();
                // Alle Musik aus, Junkyard an
                Debug.Log("JUNKYARD MUSIK");
            }
            if (musicPlaying.menuMusicPlaying)
            {
                musicPlaying.menuMusicPlaying = false;
                // menumusik ausschalten
            }
        }
        else if (currentSceneName == SceneNameManager.menu)
        {
            if (!musicPlaying.menuMusicPlaying)
            {
                musicPlaying.menuMusicPlaying = true;
                //menu musik loopen
                //menumusik abspielen
            }
            if (musicPlaying.junkyardMusicPlaying)
            {
                musicPlaying.junkyardMusicPlaying = false;
                junkyardMusic.Stop();
            }
        }

       // FÜR WEITERE MUSIKSTÜCKE ERWEITERN
            // .
            // .
            // .
	}
}
