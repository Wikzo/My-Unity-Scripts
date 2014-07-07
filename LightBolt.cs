using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LightBolt : MonoBehaviour
{
    // lightning stuff - http://atimarin.wordpress.com/2013/09/25/lightning-strike-on-unity-3d-using-line-renderer/
    // -----------------------------
    private LineRenderer line;
    private Transform myTransform;
    private Vector3 finalPos;

    public Transform Target;
    public float NumberOfBends = 4;

    public Vector2 RandomMiddleMin;
    public Vector2 RandomMiddleMax;

    public Vector2 RandomEndMin;
    public Vector2 RandomEndMax;
    // -----------------------------

    // camera shake stuff -  https://gist.github.com/ftvs/5822103
    // -----------------------------

    public bool UseCameraShake = true;

    public Transform camTransform;

    public float ShakeDuration = 0.2f;

    public float CameraShakeAmount = 0.2f;
    public float CameraShakeDecreaseFactor = 1.0f;

    private float shakeTimer;

    Vector3 originalPos;

    // -----------------------------


    // Use this for initialization
    void Start()
    {
        // line renderer setup
        line = GetComponent<LineRenderer>();
        line.SetVertexCount((int)NumberOfBends);

        myTransform = gameObject.transform;

        finalPos = Target.transform.localPosition;

        // camera setup
        if (camTransform == null)
            camTransform = Camera.main.transform;

        originalPos = camTransform.localPosition;

    }

    IEnumerator SpawnSingleLightBolt(float duration)
    {
        line.enabled = true;

        line.SetPosition(0, myTransform.position);

        for (var i = 1; i < (int)NumberOfBends; i++)
        {
            var pos = Vector3.Lerp(myTransform.localPosition, Target.transform.localPosition, i / NumberOfBends);

            pos.x += Random.Range(RandomMiddleMin.x, RandomMiddleMax.x);
            pos.y += Random.Range(RandomMiddleMin.y, RandomMiddleMax.y);

            line.SetPosition(i, pos);

        }

        var end = finalPos;

        end.x += Random.Range(RandomEndMin.x, RandomEndMax.x);
        end.y += Random.Range(RandomEndMin.y, RandomEndMax.y);

        line.SetPosition((int)NumberOfBends - 1, end);
        shakeTimer = ShakeDuration;


        yield return new WaitForSeconds(duration);
        line.enabled = false;
    }

    void SpawnContinuousLightBolt()
    {
        line.enabled = true;

        line.SetPosition(0, myTransform.position);

        for (var i = 1; i < (int)NumberOfBends; i++)
        {
            var pos = Vector3.Lerp(myTransform.localPosition, Target.transform.localPosition, i / NumberOfBends);

            pos.x += Random.Range(RandomMiddleMin.x, RandomMiddleMax.x);
            pos.y += Random.Range(RandomMiddleMin.y, RandomMiddleMax.y);

            line.SetPosition(i, pos);

        }

        var end = finalPos;

        end.x += Random.Range(RandomEndMin.x, RandomEndMax.x);
        end.y += Random.Range(RandomEndMin.y, RandomEndMax.y);

        line.SetPosition((int)NumberOfBends - 1, end);
        shakeTimer = ShakeDuration;
    }
    
    void ShakeCamera()
    {
        if (shakeTimer > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * CameraShakeAmount;

            shakeTimer -= Time.deltaTime * CameraShakeDecreaseFactor;
        }
        else
        {
            shakeTimer = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //line.SetVertexCount((int)NumberOfBends);

        if (Input.GetMouseButtonDown(1)) // right mouse = toggle
            StartCoroutine(SpawnSingleLightBolt(Random.Range(0.1f, 0.4f)));
        else if (Input.GetMouseButton(0)) // left mouse = hold down
            SpawnContinuousLightBolt();
        else if (Input.GetMouseButtonUp(0))
            line.enabled = false;

        if (UseCameraShake)
            ShakeCamera();
    }
}
