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



namespace BetterCamera
{
	public class BetterCameraMelon : MelonMod
	{
        public static bool CanSavePicture = false;


        public override void OnInitializeMelon()
		{
            Settings.instance.AddToModSettings("Better Camera");
        }

        public override void OnUpdate()
        {
            if (!GameManager.IsMainMenuActive() && InputManager.instance != null && InputManager.GetKeyDown(InputManager.m_CurrentContext, Settings.instance.keyCode) && CanSavePicture == true)
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

                    CanSavePicture = false;
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


                    CanSavePicture = false;
                }
            }
        }

    }
}