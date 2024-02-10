using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    public Camera camera;
    public float baseFOV = 90f;
    public float maxFOV = 110f;
    public float maxSpeed = 20f;
    public float shakeMagnitude = 0.01f;
    public float shakeSmoothFactor = 0.5f; // Added for smoothing the shake transition

    private Vector3 originalCamPos;
    private Vector3 targetShakeOffset; // Target offset for smoother shake

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        originalCamPos = camera.transform.localPosition;
        SpeedController.Instance.OnSpeedChanged += AdjustFOVAndShake;
    }

    void OnDestroy()
    {
        SpeedController.Instance.OnSpeedChanged -= AdjustFOVAndShake;
    }

    void AdjustFOVAndShake(float newSpeed)
    {
        float newFOV = Mathf.Lerp(baseFOV, maxFOV, newSpeed / maxSpeed);
        camera.fieldOfView = Mathf.Clamp(newFOV, baseFOV, maxFOV);

        float speedRatio = newSpeed / maxSpeed;
        if (speedRatio > 0.5f)
        {
            float adjustedSpeedRatio = (speedRatio - 0.5f) * 2f;
            targetShakeOffset = Random.insideUnitSphere * Mathf.Lerp(0f, shakeMagnitude, adjustedSpeedRatio);
        }
        else
        {
            targetShakeOffset = Vector3.zero;
        }
    }

    void Update()
    {
        if (targetShakeOffset != Vector3.zero)
        {
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, originalCamPos + targetShakeOffset, shakeSmoothFactor * Time.deltaTime);
        }
        else
        {
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, originalCamPos, shakeSmoothFactor * Time.deltaTime);
        }
    }
}
