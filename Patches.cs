using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection;
using System.Collections;
using Il2CppTLD.Gear;
using Il2Cpp;
using Il2CppTLD.IntBackedUnit;
using Il2CppVLB;
using Il2CppNodeCanvas.Tasks.Actions;
using Il2CppSteamworks;
using Il2CppTLD.UI;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine.UIElements;
using Il2CppParadoxNotion.Serialization.FullSerializer;


namespace BetterCamera
{

    internal static class Patches
    {
        [HarmonyPatch(typeof(PhotoManager), "SetPhotoTexture")]

        public static class PicturePatch
        {
            public static void Postfix(ref PhotoManager __instance)
            {
                BetterCameraMelon.CanSavePicture = true;
                if(Settings.instance.popups)
                {
                    HUDMessage.AddMessage("Press '" + Settings.instance.keyCode + "' to save photo.", true, true);
                }
            }

        }

        [HarmonyPatch(typeof(GameManager), "Awake")]
        public static class FOVPatch
        {
            public static void Postfix(ref GameManager __instance)
            {
                PhotoManager pm = GameManager.GetPhotoManager();
                pm.m_FieldOfViewScalar = Settings.instance.photofov;
    

            }

        }
       

        [HarmonyPatch(typeof(GunItem), "Awake")]
        public static class CameraPatch
        {
            public static void Postfix(ref GunItem __instance)
            {
                if(__instance.m_GunType == GunType.Camera)
                {
                    if (Settings.instance.instantphoto)
                    {
                        __instance.m_FiringRateSeconds = 0;
                    }
                    else
                    {
                        __instance.m_FiringRateSeconds = 6.333f;
                    }
                    __instance.m_SupportsUnload = Settings.instance.unloading;
                    __instance.m_ClipSize = Settings.instance.clipsize;
                    __instance.m_RoundsToReloadPerClip = Settings.instance.clipsize;
                }


            }

        }


    }
}

