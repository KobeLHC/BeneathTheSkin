using UnityEngine;
using System.Collections.Generic;

public class LiquidSorting : MonoBehaviour
{
    [SerializeField] public List<GameObject> liquidLayers = new List<GameObject>(); // Liquid stack (bottom to top)
    public int maxLayers = 3; // Max 3 layers per tube
    public float tubeHeight; // Set the height of the tube manually
    public float baseYPosition; // Bottom of the tube
    public bool isFull = false;
    public bool winCheck = true;
    public LevelCode gameManager;
    private static LiquidSorting selectedTube = null; // Stores the first selected tube

    // Sound effect control
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        baseYPosition = transform.position.y - (tubeHeight / 2); // Set the bottom position based on tube height
    }

    void Update()
    {
        // Checks if tube is full
        if (liquidLayers.Count == 3)
        {
            if (liquidLayers[0].transform.name[0] == liquidLayers[1].transform.name[0] && liquidLayers[1].transform.name[0] == liquidLayers[2].transform.name[0])
            {
                isFull = true;
            }
            else
            {
                isFull = false;
            }
        }
        else
        {
            isFull = false;
        }

        if (winCheck && isFull)
        {
            winCheck = false;
            gameManager.WinProgUp();
        }
        else if (!isFull)
        {
            winCheck = true;
        }
    }

    void OnMouseDown()
    {
        if (selectedTube == null && liquidLayers.Count > 0)
        {
            // First click: Select this tube if it has liquid
            selectedTube = this;
            audioManager.PlaySFX(audioManager.clicking);
            Debug.Log("Selected tube with top color: " + GetTopColor());

            // Move the tube up slightly
            transform.position += new Vector3(0, 0.2f, 0); // Move up by 0.2 units
        }
        else if (selectedTube != null && selectedTube != this)
        {
            // Pour liquid from selected tube to the clicked tube
            selectedTube.PourLiquid(this);
            audioManager.PlaySFX(audioManager.clicking);

            // Reset previous tube's position
            selectedTube.transform.position -= new Vector3(0, 0.2f, 0);

            // Deselect the tube
            selectedTube = null;
        }
        else
        {
            // If clicking the same tube again, deselect it
            if (selectedTube == this)
            {
                transform.position -= new Vector3(0, 0.2f, 0); // Move back down
                selectedTube = null;
            }
        }
    }

    void PourLiquid(LiquidSorting targetTube)
    {
        if (liquidLayers.Count > 0 && targetTube.liquidLayers.Count < targetTube.maxLayers)
        {
            GameObject topLiquid = liquidLayers[liquidLayers.Count - 1];
            liquidLayers.RemoveAt(liquidLayers.Count - 1);
            targetTube.liquidLayers.Add(topLiquid);
            topLiquid.transform.parent = targetTube.transform;
            topLiquid.transform.position = targetTube.GetLayerPosition(targetTube.liquidLayers.Count - 1);
            topLiquid.GetComponent<SpriteRenderer>().sortingOrder = targetTube.liquidLayers.Count;
        }
        else
        {
            Debug.Log("Cannot pour! Target tube is full.");
        }
    }

    public Color GetTopColor()
    {
        if (liquidLayers.Count > 0)
        {
            return liquidLayers[liquidLayers.Count - 1].GetComponent<SpriteRenderer>().color;
        }
        return Color.clear;
    }

    // Improved liquid positioning function
    public Vector3 GetLayerPosition(int layerIndex)
    {
        float layerHeight = tubeHeight / maxLayers; // Divide tube height into equal parts
        float yOffset = 0.15f; // Small offset to prevent overlap

        // Calculate Y position dynamically
        float yPos = baseYPosition + (layerHeight * layerIndex) + (yOffset * layerIndex);

        Debug.Log($"Layer {layerIndex} Position: {yPos}");

        return new Vector3(transform.position.x, yPos, transform.position.z - (0.01f * layerIndex));
    }
}

