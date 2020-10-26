using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColorToTeamColor : MonoBehaviour
{
    public void SetupSpriteMaterials(TeamIDEnum team)
    {
        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        Color color = ColorUtils.TeamIdEnumToColor(team);

        if (spriteRenderers != null)
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sharedMaterial.SetColor("_NewColor", color);
            }
        }
    }
}
