

public static class InputManager
{
    public static ActionMap actionMap;

    
    static InputManager()
    {
        actionMap = new ActionMap();
        actionMap.Enable();
    }
    //
    // public static void SwitchToMenuInput()
    // {
    //     Time.timeScale = 0;
    // }
    //
    // public static void SwitchToPlayerInput()
    // {
    //     Time.timeScale = 1;
    // }
}
