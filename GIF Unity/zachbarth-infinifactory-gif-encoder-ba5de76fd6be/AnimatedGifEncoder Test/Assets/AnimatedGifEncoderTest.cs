using GifEncoder;
using System;
using System.IO;
using UnityEngine;

public class AnimatedGifEncoderTest : MonoBehaviour
{
    // from: https://bitbucket.org/zachbarth/infinifactory-gif-encoder

    public Camera renderCamera;
    public Transform testCube;

    [Header("Recording Options")]
    [Space(10)]
    public float RecordingDuration = 5f;
    public int Width = 320;
    public int Height = 240;
    public int Depth = 24;

    private bool startRecording = false;

    private float timer = 0f;

    private RenderTexture renderTexture;
    private AnimatedGifEncoder gifEncoder;
    private float cameraAngle;

    public void Start()
    {
        // Configure the camera to render manually into the render texture:
        this.renderTexture = new RenderTexture(Width, Height, Depth);
        this.renderCamera.enabled = false;
        this.renderCamera.targetTexture = this.renderTexture;

        // Create a GIF encoder:
        String created = DateTime.Now.ToString("H_mm_ss");
        //this.gifEncoder = new AnimatedGifEncoder(Path.Combine(Application.dataPath, "GIF_ " + created + ".gif"));
        this.gifEncoder = new AnimatedGifEncoder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GIF_ " + created + ".gif"));

        this.gifEncoder.SetDelay(1000 / 30);
    }

    public void Update()
    {
        RotateCube();

        if (!startRecording)
            return;

        timer += Time.deltaTime;

        // Force the render camera, which we disabled earlier, to render a frame:
        this.renderCamera.Render();

        // Copy the render texture data into a temporary texture:
        RenderTexture.active = this.renderTexture;
        Texture2D frameTexture = new Texture2D(this.renderTexture.width, this.renderTexture.height, TextureFormat.RGB24, false);
        frameTexture.ReadPixels(new Rect(0, 0, this.renderTexture.width, this.renderTexture.height), 0, 0);

        // Add the current frame to the GIF:
        this.gifEncoder.AddFrame(frameTexture);

        // Destroy the temporary texture:
        UnityEngine.Object.Destroy(frameTexture);

        if (timer > RecordingDuration)
        {
            StopRecording();
        }
    }

    private void StopRecording()
    {
        this.gifEncoder.Finish();
        startRecording = false;
        timer = 0;
        this.Quit();
    }

    

    private void RotateCube()
    {
        // After we've rotated the cube exactly once we should finalize the GIF so that it loops seamlessly:
        this.cameraAngle += 5;
        this.testCube.eulerAngles = new Vector3(0, this.cameraAngle, 0);
    }


    void OnGUI()
    {
        if (startRecording)
            GUI.Label(new Rect(10, 10, 410, 20), "RECORDING: " + timer);
        else if (!startRecording)
            if (GUI.Button(new Rect(10, 10, 110, 20), "Begin recording"))
                startRecording = true;

    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
