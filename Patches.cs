
namespace BetterCamera
{

    internal static class Patches
    {
        [HarmonyPatch(typeof(PhotoManager), nameof(PhotoManager.SetPhotoTexture))]

        public static class PicturePatch
        {
            public static void Postfix(ref PhotoManager __instance)
            {
                GameManager.m_vpFPSCamera.m_UnzoomedFieldOfView = BetterCameraMelon.cameraFOVBeforeAim;
                BetterCameraMelon.canSavePicture = true;
                if(Settings.instance.popups)
                {
                    HUDMessage.AddMessage("Press '" + Settings.instance.keyCode + "' to save photo.", true, true);
                }
            }

        }

        [HarmonyPatch(typeof(GunItem), nameof(GunItem.Awake))]
        public static class CameraStatsPatch
        {
            public static void Postfix(ref GunItem __instance)
            {
                if(__instance.m_GunType == GunType.Camera)
                {
                    __instance.m_SupportsUnload = Settings.instance.unloading;
                    __instance.m_ClipSize = Settings.instance.clipsize;
                    __instance.m_RoundsToReloadPerClip = Settings.instance.clipsize;
                    if(Settings.instance.tooltip)
                    {
                        __instance.m_FireButtonLabel = "Take Photo";
                    }
                    __instance.m_MultiplierAiming = Settings.instance.aimspeed;
                }


            }

        }

        [HarmonyPatch(typeof(GunItem), nameof(GunItem.ZoomEnd))]
        public static class CameraStopAimPatch
        {
            public static void Postfix(ref GunItem __instance)
            {
                if (__instance.m_GunType == GunType.Camera)
                {
                    GameManager.m_vpFPSCamera.m_UnzoomedFieldOfView = BetterCameraMelon.cameraFOVBeforeAim;
                }
            }
        }



    }
}

