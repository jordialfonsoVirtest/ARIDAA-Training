using UnityEngine;

public class NewSlicer : MonoBehaviour
{
    [Header("Cutting Plane Settings")]
    [Tooltip("The object whose material will be affected by the clipping plane.")]
    public Renderer targetObjectRenderer;

    [Tooltip("Which direction of this object will define the normal of the cutting plane.")]
    public Transform planeNormalSource;

    [Tooltip("If checked, the cutting effect will be inverted.")]
    public bool invertCut = false;

    [Header("Transparency Settings")]
    [Tooltip("Toggle transparency for the cut object.")]
    public bool transparencyEnabled = false;

    [Tooltip("Transparency amount when enabled (0.0 = fully transparent, 1.0 = fully opaque).")]
    [Range(0.0f, 1.0f)]
    public float transparencyAmount = 0.75f; // Default to 75% visible when transparent

    private Material _targetMaterial;

    // Store original render queue and tags for restoring opaque state
    private int _originalRenderQueue;
    private string _originalRenderTypeTag;

    // Shader property IDs (good practice for performance)
    private static readonly int _PlanePositionID = Shader.PropertyToID("_PlanePosition");
    private static readonly int _PlaneNormalID = Shader.PropertyToID("_PlaneNormal");

    void Start()
    {

    }

    void Update()
    {
        if (targetObjectRenderer == null)
        {
            Debug.LogError("ClippingPlaneController: Target Object Renderer is not assigned. Please assign the object you want to cut.", this);
            enabled = false;
            return;
        }

        _targetMaterial = targetObjectRenderer.material;
        if (_targetMaterial == null)
        {
            Debug.LogError("ClippingPlaneController: Target object has no material assigned.", this);
            enabled = false;
            return;
        }

        // Check if the material uses our custom shader
        if (!_targetMaterial.shader.name.Contains("Custom/ClippingPlaneTransparent"))
        {
            Debug.LogWarning("ClippingPlaneController: The target object's material does not use the 'Custom/ClippingPlaneTransparent' shader. The clipping and transparency effect will not work.", this);
        }

        if (planeNormalSource == null)
        {
            planeNormalSource = this.transform;
            Debug.LogWarning("ClippingPlaneController: Plane Normal Source not assigned. Using this object's transform for the normal.", this);
        }

        // Store original render queue and render type tag from the material's initial setup
        _originalRenderQueue = _targetMaterial.renderQueue;
        _originalRenderTypeTag = _targetMaterial.GetTag("RenderType", false, "Opaque"); // Default to Opaque if not found

        if (_targetMaterial == null) return;

        // Update clipping plane position
        _targetMaterial.SetVector(_PlanePositionID, transform.position);

        // --- IMPORTANT: Adjust this line based on your cutting object's orientation ---
        // Experiment with planeNormalSource.forward, .up, .right, or their negative versions.
        //Vector3 planeNormal = planeNormalSource.forward; // Current setting

        // Example: If your cutting object's Y-axis is the normal for the cut:
         Vector3 planeNormal = planeNormalSource.up;

        // Example: If your cutting object's X-axis is the normal for the cut:
        // Vector3 planeNormal = planeNormalSource.right;

        // If the cut is on the wrong side, invert it:
        // Vector3 planeNormal = -planeNormalSource.forward; // or -planeNormalSource.up, etc.

        if (invertCut)
        {
            planeNormal = -planeNormal;
        }
        _targetMaterial.SetVector(_PlaneNormalID, planeNormal);

    }
}