# [The HotPath Show - Episode 11](https://www.youtube.com/watch?v=XqHDsXCfT-Q)
![image](https://github.com/TheHotPathShow/Episode-11/assets/7334984/bd2fb116-e246-4e73-98c1-a3fa7d0b1b07)


## 1. Input Handling basics

Main Input System functions powering most of the project!
```cs
InputUser.listenForUnpairedDeviceActivity = 4;
InputUser.onUnpairedDeviceUsed += (control, _) =>
{
    if (!(control is ButtonControl)) 
        return;
    if (control.device.description.deviceClass == "Mouse") 
        return;

    // Pair the device with the user, making the playerController only act with the given device
    var user = InputUser.PerformPairingWithDevice(control.device);
    var playerController = new KyleInput();
    playerController.Enable();
    user.AssociateActionsWithUser(playerController);
    
    // if keyboard also pair with mouse
    if (control.device.description.deviceClass == "Keyboard")
    {
        var mouse = InputSystem.devices.FirstOrDefault(d => d.description.deviceClass == "Mouse");
        if (mouse != null)
            InputUser.PerformPairingWithDevice(mouse, user);
    }
    
    // Do something with `playerController` or store it for later use
    
    // Listen for one less device
    InputUser.listenForUnpairedDeviceActivity--;
};
```

Written by: Dani K Andersen ([@dani485b](https://twitter.com/dani485b))
