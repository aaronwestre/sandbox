using UnityEngine;

public class VideoScreen : MonoBehaviour
{
    public int videoWidth = 1920;
    public int videoHeight = 1080;
    public GameObject stipplePrefab;
    public GameObject plane;
    public float gridSpacing;
    public float margin;

    void Start()
    {
        InitializeCameraTexture();
        InitializeStipples();
        //stippleScreen.SetActive(false);
        plane.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        var texture = plane.GetComponent<Renderer>().material.GetTexture("Texture2D_75A0DF1A");
        var previousRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Graphics.Blit(texture, renderTexture);
        readableTexture.ReadPixels(new Rect(0, 0, readableTexture.width, readableTexture.height), 0, 0);
        readableTexture.Apply();
        RenderTexture.active = previousRenderTexture;
        var gridSpacingPixels = (int)Mathf.Floor(videoWidth * (gridSpacing / planeWidth));
        var maximumGridCellValue = gridSpacingPixels * gridSpacingPixels;
        for (var column = 0; column < columnCount; column++)
        {
            for (var row = 0; row < rowCount; row++)
            {
                var x = column * gridSpacingPixels;
                var y = row * gridSpacingPixels;
                var pixels = readableTexture.GetPixels(x, y, gridSpacingPixels, gridSpacingPixels);
                var gridCellValue = 0.0f;
                for (var index = 0; index < pixels.Length; index++)
                {
                    gridCellValue += 1.0f - pixels[index].grayscale;
                }
                gridCellValue /= maximumGridCellValue;
                if (float.IsNaN(gridCellValue))
                {
                    stipples[column][row].transform.localScale = Vector3.zero;
                } else {
                    stipples[column][row].transform.localScale = maximumStippleScale * gridCellValue;
                }
            }
        }
    }

    private void InitializeStipples()
    {
        var planeBounds = plane.GetComponent<Collider>().bounds;
        planeWidth = planeBounds.size.x;
        planeHeight = planeBounds.size.y;
        Debug.Log($"Plane size = width: {planeWidth}, height: {planeHeight}");
        stippleScreen = new GameObject();
        stippleScreen.transform.position = plane.transform.position;
        var planeOrigin = plane.transform.position - new Vector3(planeBounds.extents.x, planeBounds.extents.y, 0.0f);
        var maximumStippleDimension = gridSpacing * (1.0f - margin);
        maximumStippleScale = new Vector3(maximumStippleDimension, maximumStippleDimension, maximumStippleDimension);
        var initialStippleScale = maximumStippleScale * 0.01f;
        var offsetFromSurface = -0.5f;
        var halfGridSpacing = gridSpacing / 2.0f;
        columnCount = (int)Mathf.Floor(planeWidth / gridSpacing);
        rowCount = (int)Mathf.Floor(planeHeight / gridSpacing);
        stipples = new GameObject[columnCount][];
        for (var column = 0; column < columnCount; column++)
        {
            stipples[column] = new GameObject[rowCount];
            for (var row = 0; row < rowCount; row++)
            {
                var x = column * gridSpacing + halfGridSpacing;
                var y = row * gridSpacing + halfGridSpacing;
                var stipplePosition = planeOrigin + new Vector3(x, y, offsetFromSurface);
                stipples[column][row] = Instantiate(stipplePrefab, stipplePosition, Quaternion.identity);
                stipples[column][row].transform.localScale = initialStippleScale;
                stipples[column][row].transform.SetParent(stippleScreen.transform);
            }
        }
    }

    private void InitializeCameraTexture()
    {
        webCamTexture = new WebCamTexture();
        webCamTexture.requestedWidth = videoWidth;
        webCamTexture.requestedHeight = videoHeight;
        webCamTexture.requestedFPS = 30;
        plane.GetComponent<Renderer>().material.SetTexture("Texture2D_75A0DF1A", webCamTexture);
        webCamTexture.Play();
        renderTexture = new RenderTexture(videoWidth, videoHeight, 16);
        readableTexture = new Texture2D(videoWidth, videoHeight);
        //plane.GetComponent<Renderer>().enabled = false;
    }

    private WebCamTexture webCamTexture;
    private RenderTexture renderTexture;
    private Texture2D readableTexture;
    private GameObject stippleScreen;
    private GameObject[][] stipples;
    private Vector3 maximumStippleScale;
    private float planeWidth;
    private float planeHeight;
    private int columnCount;
    private int rowCount;
}