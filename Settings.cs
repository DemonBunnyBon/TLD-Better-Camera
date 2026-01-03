using ModSettings;

namespace BetterCamera
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Camera")]

        [Name("Dynamic Zoom")]
        [Description("While zooming in with the camera, enables to use the scroll wheel to zoom in and out further. [Default: true]")]
        public bool dynazoom = true;

        [Name("Scrolling Sound")]
        [Description("Whether or not to enable a scrolling sound while using dynamic zoom. [Default: true]")]
        public bool scrollsound = true;

        [Name("Aim Animation Speed")]
        [Description("Allows to speed up the animation of aiming. [Default: 1] [Requires scene reload to take effect]")]
        [Slider(1, 2, 1)]
        public int aimspeed = 1;

        [Name("Change 'Fire' tooltip.")]
        [Description("Renames the 'Fire' tooltip when taking out a camera to say 'Take Photo' instead. [Default: true] [Requires scene reload to take effect]")]
        public bool tooltip = true;

        [Name("Camera Clip Size")]
        [Description("Allows to change the max amount of film inside the camera. [Requires scene reload to take effect] [Default: 6]")]
        [Slider(6, 10, 1)]
        public int clipsize = 6;

        [Name("Allow unloading")]
        [Description("Allows removing the camera's film from inventory. [Requires scene reload to take effect] [Default: true]")]
        public bool unloading = true;

        [Section("Photos")]

        [Name("Enable Save Photo Pop-up")]
        [Description("Whether or not to display pop-ups regarding photo saving. [Default: true]")]
        public bool popups = true;

        [Name("Enable Melon Logging")]
        [Description("Whether or not to display log messages and potential photo saving errors in the Melon log. [Default: true]")]
        public bool melonlogs = false;
        
        [Section("Keybinds")]

        [Name("Save Photo")]
        [Description("Click to set the keybinding. [Default: P]")]
        public UnityEngine.KeyCode keyCode = KeyCode.P;

        [Name("Alternative Zoom In Key")]
        [Description("Alternate key to zoom in instead of scrolling if dynamic zoom is enabled [Default: +=]")]
        public UnityEngine.KeyCode zoomin = KeyCode.Equals;
        [Name("Alternative Zoom Out Key")]
        [Description("Alternate key to zoom out instead of scrolling if dynamic zoom is enabled [Default: -]")]
        public UnityEngine.KeyCode zoomout = KeyCode.Minus;

        [Section("Reset Settings")]
        [Name("Reset To Default")]
        [Description("Resets all settings to Default. [Confirm and Scene Transition/Reload required.]")]
        public bool ResetSettings = false;



        protected override void OnConfirm()
        {
            ApplyReset();
            instance.ResetSettings = false;
            base.OnConfirm();
            base.RefreshGUI();
        }

        public static void ApplyReset()
        {
            if (instance.ResetSettings == true)
            {
                instance.melonlogs = true;
                instance.popups = true;
                instance.clipsize = 6;
                instance.aimspeed = 1;
                instance.unloading = true;
                instance.keyCode = KeyCode.P;
                instance.ResetSettings = false;
                instance.zoomout = KeyCode.Minus;
                instance.zoomin = KeyCode.Plus;
                instance.dynazoom = true;
                instance.scrollsound = true;
                instance.tooltip = true;
            }
        }
    }


}
