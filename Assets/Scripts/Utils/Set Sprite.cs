using UnityEngine;
using UnityEngine.UI;

static class Utility
{
    public static void SetSprite(Image imageComponent, Sprite newSprite)
    {
            // Set the new sprite
            imageComponent.sprite = newSprite;

            // Add AspectRatioFitter component if not already added
            AspectRatioFitter aspectRatioFitter = imageComponent.GetComponent<AspectRatioFitter>();
            if (aspectRatioFitter == null)
            {
                aspectRatioFitter = imageComponent.gameObject.AddComponent<AspectRatioFitter>();
            }

            // Set the aspect mode to Fit In Parent
            aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;

            // Update the aspect ratio based on the new sprite
            if (newSprite != null)
            {
                aspectRatioFitter.aspectRatio = newSprite.bounds.size.x / newSprite.bounds.size.y;
            }
    }
    public static void SetSprite(GameObject gameObject, Sprite newSprite)
    {
        if (gameObject == null || newSprite == null)
        {
            Debug.LogError("GameObject or Sprite is null. Cannot set sprite.");
            return;
        }

        // Check if the GameObject has an Image component
        Image imageComponent = gameObject.GetComponent<Image>();
        if (imageComponent != null)
        {
            // Set the new sprite
            imageComponent.sprite = newSprite;

            // Add AspectRatioFitter component if not already added
            AspectRatioFitter aspectRatioFitter = imageComponent.GetComponent<AspectRatioFitter>();
            if (aspectRatioFitter == null)
            {
                aspectRatioFitter = imageComponent.gameObject.AddComponent<AspectRatioFitter>();
            }

            // Set the aspect mode to Fit In Parent
            aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;

            // Update the aspect ratio based on the new sprite
            if (newSprite != null)
            {
                aspectRatioFitter.aspectRatio = newSprite.bounds.size.x / newSprite.bounds.size.y;
            }
        }
        else
        {
            // Check if the GameObject has a SpriteRenderer component
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Set the new sprite
                spriteRenderer.sprite = newSprite;

                // Optionally, adjust the size or other properties here as needed
            }
            else
            {
                Debug.LogError("GameObject does not have an Image or SpriteRenderer component.");
            }
        }
    }

}
