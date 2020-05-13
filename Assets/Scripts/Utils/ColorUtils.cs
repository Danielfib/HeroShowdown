using UnityEngine;

public static class ColorUtils
{
    public static Color Green = Color.green;
    public static Color Red = Color.red;
    public static Color Brown = new Color(192f / 255f, 136f / 255f, 63f / 255f);
    public static Color Purple = new Color(141f / 255f, 23f / 255f, 192f / 255f);
    public static Color Blue = Color.cyan;

    public static Color TeamIdEnumToColor(TeamIDEnum color)
    {
        Color colorToReturn = Color.white;
        switch (color)
        {
            case TeamIDEnum.BLUE:
                colorToReturn = Blue;
                break;
            case TeamIDEnum.BROWN:
                colorToReturn = Brown;
                break;
            case TeamIDEnum.GREEN:
                colorToReturn = Green;
                break;
            case TeamIDEnum.PURPLE:
                colorToReturn = Purple;
                break;
            case TeamIDEnum.RED:
                colorToReturn = Red;
                break;
        }
        return colorToReturn;
    }
}
