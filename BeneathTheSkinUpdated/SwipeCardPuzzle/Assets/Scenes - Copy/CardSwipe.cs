using UnityEngine;

public class CardSwipe : MonoBehaviour
{
    private Vector2 startPos;
    private float swipeStartTime;
    private bool isSwiping = false;
    
    public Transform swipeStart;
    public Transform swipeEnd;
    
    public float minSpeed = 3f; // Adjust these based on testing
    public float maxSpeed = 6f;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Start Swipe
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                isSwiping = true;
                startPos = transform.position;
                swipeStartTime = Time.time;
            }
        }

        if (Input.GetMouseButton(0) && isSwiping) // Dragging the card
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float newX = Mathf.Clamp(mousePos.x, swipeStart.position.x, swipeEnd.position.x);
            transform.position = new Vector2(newX, transform.position.y);
        }

        if (Input.GetMouseButtonUp(0) && isSwiping) // Release Swipe
        {
            isSwiping = false;
            float swipeDuration = Time.time - swipeStartTime;
            float swipeDistance = Vector2.Distance(startPos, transform.position);
            float swipeSpeed = swipeDistance / swipeDuration;

            if (swipeSpeed < minSpeed)
            {
                Debug.Log("Too Slow! Try Again.");
                ResetCard();
            }
            else if (swipeSpeed > maxSpeed)
            {
                Debug.Log("Too Fast! Try Again.");
                ResetCard();
            }
            else
            {
                Debug.Log("Swipe Successful!");
            }
        }
    }

    private void ResetCard()
    {
        transform.position = swipeStart.position;
    }
}
