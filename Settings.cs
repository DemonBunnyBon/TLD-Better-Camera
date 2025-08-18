using ModSettings;

namespace BetterCamera
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Camera")]
        [Name("No Shooting Delay")]
        [Description("Allows to immediately take a picture after another. Warning: this will lead to animation desync! [Requires scene reload to take effect] [Default: false]")]
        public bool instantphoto = false;

        [Name("Camera Clip Size")]
        [Description("Allows to change the max amount of film inside the camera. [Requires scene reload to take effect] [Default: 6]")]
        [Slider(6, 10, 1)]
        public int clipsize = 6;

        [Name("Allow unloading")]
        [Description("Allows removing the camera's film from inventory. [Requires scene reload to take effect] [Default: true]")]
        public bool unloading = true;

        [Section("Photos")]

        [Name("Photo FOV Multiplier")]
        [Description("Allows to set the width of photos shot by the camera. Width should be proportionate to height for best result. High values might cause lag or instability when using camera. [Requires scene reload to take effect] [Default: 1]")]
        [Slider(0.5f, 2f, 1)]
        public float photofov = 1f;

        [Section("Keybinds")]

        [Name("Save Photo")]
        [Description("Click to set the keybinding. [Default: P]")]
        public UnityEngine.KeyCode keyCode = KeyCode.P;

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
                instance.clipsize = 6;
                instance.instantphoto = false;
                instance.unloading = true;
                instance.photofov = 1;
                instance.keyCode = KeyCode.P;
                instance.instantphoto = false;
                instance.ResetSettings = false;
            }
        }
    }


}
