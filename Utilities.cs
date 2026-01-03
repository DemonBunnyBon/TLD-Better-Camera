
namespace BetterCamera
{
    internal static class BetterCameraUtils
    {
        public static void Save(this Texture2D TexToSave, string file)
        {
            RenderTexture tmp = RenderTexture.GetTemporary(TexToSave.width,TexToSave.height,0,RenderTextureFormat.Default,RenderTextureReadWrite.Linear);
            Graphics.Blit(TexToSave, tmp);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = tmp;
            Texture2D myTexture2D = new Texture2D(TexToSave.width, TexToSave.height);
            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tmp);
            byte[] bytes = myTexture2D.EncodeToPNG();
            if(bytes != null)
            {
                System.IO.File.WriteAllBytes(file, bytes);
            }
            else
            {
                if(Settings.instance.melonlogs)
                {
                    MelonLogger.Msg("Could not encode camera photo: Bytes are empty.");
                }

            }


        }

    }
}