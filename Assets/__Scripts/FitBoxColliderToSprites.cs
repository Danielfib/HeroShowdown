using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitBoxColliderToSprites : MonoBehaviour
{
    void Update()
    {
        FitColliderToSprites();
    }

    private void FitColliderToSprites()
    {
        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        Vector2 sum = Vector2.zero;
        int i = 0;
        Bounds bound = new Bounds(transform.position, Vector3.zero);
        foreach (var sr in spriteRenderers)
        {
            if (sr.sprite == null) continue;

            bound.Encapsulate(sr.bounds.max);
            bound.Encapsulate(sr.bounds.min);
            Vector2 v = new Vector2(sr.sprite.bounds.center.x, sr.sprite.bounds.center.y);
            sum += v;
            i++;
        }
        Vector2 mediumCenter = sum / i;

        BoxCollider2D col = transform.root.GetComponent<BoxCollider2D>();
        float eRad = col.edgeRadius;
        col.size = bound.size * ((1 - eRad) - col.edgeRadius * col.edgeRadius);
        col.offset = mediumCenter;
    }
}
