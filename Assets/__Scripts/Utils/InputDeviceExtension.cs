using UnityEngine.InputSystem;

public static class InputDeviceExtension
{
    public static DeviceType ToDeviceTypeEnum(this InputDevice device)
    {
        if (device.name.Contains("Controller"))
        {
            return DeviceType.CONTROLLER;
        } 
        else if (device.name.Equals("Keyboard"))
        {
            return DeviceType.KEYBOARD;
        }
        else
        {
            return DeviceType.UNDEFINED;
        }
    }
}
