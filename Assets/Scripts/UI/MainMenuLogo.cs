using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuLogo : MonoBehaviour
{
    public float waveSpeed = 2f; // Speed of the wave
    public float waveAmplitude = 5f; // Amplitude of the wave
    public float waveFrequency = 1f; // Frequency of the wave

    private TMP_Text textMeshPro;
    private Vector3[] vertices;
    private TMP_MeshInfo[] cachedMeshInfo;

    public Image logoImage; // Reference to the Image component
    public Color color1 = Color.red;
    public Color color2 = Color.yellow;
    public float colorSpeed = 2f;

    public float scaleSpeed = 1f; // Speed of scaling
    public float scaleAmount = 0.1f; // Amount of scaling
    public float rotationSpeed = 50f; // Speed of rotation
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        // Get the TextMeshPro component
        textMeshPro = GetComponent<TMP_Text>();
        textMeshPro.ForceMeshUpdate(); // Ensure the mesh is updated
        cachedMeshInfo = textMeshPro.textInfo.CopyMeshInfoVertexData(); // Cache the original vertex data

        // Store the original scale and position
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        //Animate the text with wave motion
        WaveMotion();

        // Animate the text colors
        AnimateTextColors();

        // Animate scaling (pulsating effect)
        float scaleFactor = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = originalScale * scaleFactor;

        // Rotate the logo
        //RotateLogo();
    }

    void WaveMotion()
    {
        // Get the text info
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // Loop through each character
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue; // Skip invisible characters

            // Get the vertices of the character
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            vertices = cachedMeshInfo[materialIndex].vertices;

            // Calculate the wave offset for this character
            float offset = Mathf.Sin(Time.time * waveSpeed + i * waveFrequency) * waveAmplitude;

            // Apply the wave offset to the character's vertices
            Vector3 offsetVector = new Vector3(0, offset, 0);
            textInfo.meshInfo[materialIndex].vertices[vertexIndex + 0] = vertices[vertexIndex + 0] + offsetVector;
            textInfo.meshInfo[materialIndex].vertices[vertexIndex + 1] = vertices[vertexIndex + 1] + offsetVector;
            textInfo.meshInfo[materialIndex].vertices[vertexIndex + 2] = vertices[vertexIndex + 2] + offsetVector;
            textInfo.meshInfo[materialIndex].vertices[vertexIndex + 3] = vertices[vertexIndex + 3] + offsetVector;
        }

        // Update the mesh with the modified vertices
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

    void AnimateTextColors()
    {
        // Get the text info
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // Loop through each character
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue; // Skip invisible characters

            // Get the vertex colors of the character
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Calculate the color based on time and character index
            Color color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time * colorSpeed + i * 0.1f, 1));

            // Apply the color to the character's vertices
            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }

        // Update the mesh with the modified vertex colors
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

void RotateLogo()
{
    // Calculate the oscillating rotation angle using a sine wave
    float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * 10f; // Oscillates between -30 and 30 degrees

    // Apply the rotation to the logo
    transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
}
}