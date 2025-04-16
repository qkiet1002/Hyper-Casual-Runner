using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip addMore;
    [SerializeField] private AudioClip removeMore;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to play win sound
    public void Win()
    {
        audioSource.clip = winClip;  // Assign the clip
        audioSource.Play();          // Play the clip
    }
    public void AddMore()
    {
        audioSource.clip = addMore;  // Assign the clip
        audioSource.Play();          // Play the clip
    }
    public void RemoveMore()
    {
        audioSource.clip = removeMore;  // Assign the clip
        audioSource.Play();          // Play the clip
    }
}
