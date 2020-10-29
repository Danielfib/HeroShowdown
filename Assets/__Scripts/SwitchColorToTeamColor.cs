using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchColorToTeamColor : MonoBehaviour
{
    private Material mat;

    private void Awake()
    {
        mat = new Material(Shader.Find("Shader Graphs/ChangeColorToColor"));
        mat.SetColor("_OldColor", ColorUtils.ReplaceColor);
    }

    private void Start()
    {
        Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();
        foreach(var r in renderers)
        {
            r.material = mat;
        }
    }

    public void ChangeMaterialColor(TeamIDEnum team)
    {
        Color color = ColorUtils.TeamIdEnumToColor(team);
        this.mat.SetColor("_NewColor", color);
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
