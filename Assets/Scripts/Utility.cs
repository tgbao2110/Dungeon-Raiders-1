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
}
