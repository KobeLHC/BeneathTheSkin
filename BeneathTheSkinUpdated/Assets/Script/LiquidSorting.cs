using UnityEngine;
using System.Collections.Generic;

public class LiquidSorting : MonoBehaviour
{
    public LevelCode other;
    public List<GameObject> liquidLayers = new List<GameObject>(); // Liquid stack (bottom to top)
    public int maxLayers = 3; // Max 3 layers per tube
    public float tubeHeight = 1.0f; // Set the height of the tube manually
    public float baseYPosition; // Bottom of the tube
    public bool isFull = false;
    public bool winCheck = true;

    private static LiquidSorting selectedTube = null; // Stores the first selected tube

    void Start()
    {
        baseYPosition = transform.position.y - (tubeHeight / 2); // Set the bottom position based on tube height
    }

    void Update()
    {
        // Checks if tube is full
        if (liquidLayers.Count == 3)
        {
            // Checks if tube only consists of one colour
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
            other.WinProgUp();
        }
        else if (!isFull)
        {
            winCheck = true;
        }
    }

    void OnMouseDown()
    {
        if (selectedTube == null)
        {
            // First click: Select this tube if it has liquid
            if (liquidLayers.Count > 0)
            {
                selectedTube = this;
                Debug.Log("Selected tube with top color: " + GetTopColor());
            }
        }
        else
        {
            // Second click: Transfer top liquid to this tube
            if (selectedTube != this)
            {
                selectedTube.PourLiquid(this);
            }
            // Reset selection
            selectedTube = null;
        }
    }

    void PourLiquid(LiquidSorting targetTube)
    {
        if (liquidLayers.Count > 0 && targetTube.liquidLayers.Count < targetTube.maxLayers)
        {
            // Move top liquid layer
            GameObject topLiquid = liquidLayers[liquidLayers.Count - 1];
            liquidLayers.RemoveAt(liquidLayers.Count - 1);
            targetTube.liquidLayers.Add(topLiquid);

            // Move liquid visually
            topLiquid.transform.parent = targetTube.transform;
            topLiquid.transform.position = targetTube.GetLayerPosition(targetTube.liquidLayers.Count - 1);

            // Adjust sorting order
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

    public Vector3 GetLayerPosition(int layerIndex)
    {
        // Manually calculate Y position for each layer in the tube
        float layerHeight = tubeHeight / maxLayers; // Divide tube height into equal parts
        float yPos = baseYPosition + (layerHeight * layerIndex);
        return new Vector3(transform.position.x, yPos, transform.position.z - (0.01f * layerIndex));
    }
}

