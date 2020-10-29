using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchColorToTeamColor : MonoBehaviour
{
    public void SetupSpriteMaterials(TeamIDEnum team)
    {
        SpriteRenderer[] spriteRenderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        Color color = ColorUtils.TeamIdEnumToColor(team);
       
        if (spriteRenderers != null)
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                Material newMat = new Material(spriteRenderer.material);
                newMat.SetColor("_NewColor", color);
                spriteRenderer.material = newMat;
            }
        }
    }

    public void SetupImageMaterials(TeamIDEnum team)
    {
        Image[] images = this.GetComponentsInChildren<Image>();
        Color color = ColorUtils.TeamIdEnumToColor(team);

        if (images != null)
        {
            foreach (var image in images)
            {
                Material newMat = new Material(image.material);
                newMat.SetColor("_NewColor", color);
                image.material = newMat;
            }
        }
    }
}
