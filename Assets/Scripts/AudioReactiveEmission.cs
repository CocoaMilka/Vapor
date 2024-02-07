using UnityEngine;

public class AudioReactiveEmission : MonoBehaviour
{
    public AudioSource audioSource; // Assign this from the Inspector
    Material targetMaterial; // The material whose emission will react to the audio
    public float sensitivity = 100.0f; // Sensitivity of the emission reaction to the audio volume
    public Color emissionColor = Color.white; // Emission color

    private float[] audioData = new float[1024]; // Increased size for more detailed spectrum data

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned to AudioReactiveEmission script on " + gameObject.name);
            this.enabled = false; // Disable this script if no AudioSource is assigned
            return;
        }

        // Grab Material
        targetMaterial = GetComponent<Renderer>().material;

        if (targetMaterial != null)
        {
            targetMaterial.EnableKeyword("_EMISSION"); // Enable emission for the material
        }
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            audioSource.GetSpectrumData(audioData, 0, FFTWindow.BlackmanHarris); // Get spectrum data from the audio source
            float intensity = CalculateBassIntensity(audioData) * sensitivity; // Calculate the intensity based on the bass frequencies

            if (targetMaterial != null)
            {
                Color finalColor = emissionColor * Mathf.LinearToGammaSpace(intensity); // Adjust the final color based on the intensity
                targetMaterial.SetColor("_EmissionColor", finalColor); // Set the emission color of the material
            }
        }
    }

    // Calculate the intensity of the bass frequencies using a non-linear scale
    float CalculateBassIntensity(float[] data)
    {
        float sum = 0f;
        int bassRange = 20; // Represents the range of indices to consider as 'bass' frequencies, adjust as needed

        for (int i = 0; i < bassRange; i++)
        {
            sum += data[i];
        }
        float average = sum / bassRange;

        // Apply a non-linear transformation using a power function
        float nonLinearIntensity = Mathf.Pow(average, 2); // Squaring the average to enhance the effect of smaller changes

        return nonLinearIntensity;
    }
}
