using MelonLoader;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection; 
using System.Collections;
using Il2CppTLD.Gear;
using Il2Cpp;
using Il2CppTLD.IntBackedUnit;
using Il2CppVLB;
using Il2CppSystem;
using Il2CppSWS;
using Harmony;
using System.Text.Json;
using System.Text.Json.Serialization;
using Il2CppTLD.Gameplay;



namespace BetterCamera
{
	public class BetterCameraMelon : MelonMod
	{
        public static bool canSavePicture = false;
        public static float cameraFOVBeforeAim = 60; //using 60 as failsafe.
        public static PlayerManager? pm;
        public static vp_FPSCamera? cam;
        
        public override void OnInitializeMelon()
		{
            Settings.instance.AddToModSettings("Better Camera");

         
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            pm = GameManager.GetPlayerManagerComponent();
            cam = GameManager.m_vpFPSCamera;
            canSavePicture = false;
        }



        public override void OnUpdate()
        {
            if(!GameManager.IsMainMenuActive() && pm != null && pm.PlayerIsZooming() == false && Settings.instance.dynazoom == true)
            {
                cameraFOVBeforeAim = cam.m_UnzoomedFieldOfView;
            }


            if (!GameManager.IsMainMenuActive() && InputManager.instance != null && InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.instance.keyCode) && canSavePicture == true)
            {
                Texture2D tex = GameManager.GetPhotoManager().PhotoTexture;
                System.DateTime dt = System.DateTime.Now;
                string photoname = dt.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
                if (tex != null)
                {
                    if(!Directory.Exists("Mods/SavedPhotos"))
                    {
                        Directory.CreateDirectory("Mods/SavedPhotos");
                    }
                    tex.Save("Mods/SavedPhotos/" + photoname);
                    if(Settings.instance.melonlogs)
                    {
                        MelonLogger.Msg("Photo saved as: " + photoname + " in Mods folder.");
                    }
                    if(Settings.instance.popups)
                    {
                        InterfaceManager.GetPanel<Panel_Subtitles>().ShowSubtitlesForced("Photo saved as: " + photoname + " in 'SavedPhotos' folder inside Mods folder.", 4f);
                    }

                    canSavePicture = false;
                }
                else
                {
                    if (Settings.instance.popups)
                    {
                        InterfaceManager.GetPanel<Panel_Subtitles>().ShowSubtitlesForced("Failed to save photo.", 4f);
                    }

                    if (Settings.instance.melonlogs)
                    {
                        MelonLogger.Error("Could not encode camera photo: No Image Found.");
                    }


                    canSavePicture = false;
                }

            }
            if(Settings.instance.dynazoom == true)
            {
                if (!GameManager.IsMainMenuActive() && InputManager.instance != null && (InputManager.GetScroll(InputManager.m_CurrentContext) > 0 || InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.instance.zoomin)) && pm.PlayerIsZooming() == true && pm != null && GameManager.m_vpFPSCamera.CurrentWeapon.m_GunItem.m_GunType == GunType.Camera)
                {
                    cam.m_UnzoomedFieldOfView = System.Math.Clamp(cam.m_UnzoomedFieldOfView - 1.5f, 12, 87);
                    if(Settings.instance.scrollsound == true)
                    {
                        GameAudioManager.PlayGUIScroll();
                    }
                    
                }
                if (!GameManager.IsMainMenuActive() && InputManager.instance != null && (InputManager.GetScroll(InputManager.m_CurrentContext) < 0 || InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.instance.zoomout)) && pm.PlayerIsZooming() == true && pm != null && GameManager.m_vpFPSCamera.CurrentWeapon.m_GunItem.m_GunType == GunType.Camera)
                {
                    cam.m_UnzoomedFieldOfView = System.Math.Clamp(cam.m_UnzoomedFieldOfView + 1.5f, 12, 87);
                    if (Settings.instance.scrollsound == true)
                    {
                        GameAudioManager.PlayGUIScroll();
                    }
                }
            }

        }

    }
}