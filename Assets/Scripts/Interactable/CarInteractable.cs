using UnityEngine;

public class CarInteractable : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {

    }

    void Honking()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void Interact(Transform interacterTransform)
    {
        Honking();
    }

    public string getInteractText()
    {
        return "Interact with car";
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }
}
