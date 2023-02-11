// vim: ts=4 sw=4 noet cc=120

using FrooxEngine;
using BaseX;
using HarmonyLib;
using NeosModLoader;

public class MoreTags : NeosMod
{
    public override string Name => "MoreTags!";
    public override string Version => "1.0.0";
    public override string Author => "Ukilop";

    [AutoRegisterConfigKey]
    public static ModConfigurationKey<bool> TagCompAttacher = new ModConfigurationKey<bool>("TagCompAttacher", "Add the 'Developer' Tag to ComponentAttacher", () => true);
    [AutoRegisterConfigKey]
    public static ModConfigurationKey<bool> TagColorDialog = new ModConfigurationKey<bool>("TagColorDialog", "Add the 'Developer' Tag to NeosColorDialog", () => true);
    [AutoRegisterConfigKey]
    public static ModConfigurationKey<bool> TagPhotos = new ModConfigurationKey<bool>("TagPhotos", "Add the 'Photo' Tag to finger and camera photos", () => true);

    public static ModConfiguration Config;

    public override void OnEngineInit()
    {
        Harmony harmony = new Harmony("Ukilop.MoreTags");
        Config = GetConfiguration();
        Config.Save(true);
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(ComponentAttacher))]
    private static class MoreTagsPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnAttach")]
        private static bool OnAttachPrefix(ComponentAttacher __instance)
        {
            if (Config.GetValue(TagCompAttacher))
            {
                __instance.Slot.Tag = "Developer";
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(NeosColorDialog))]
    private static class NeosColorDialogPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("SetupColor")]
        private static bool SetupColorPrefix(NeosColorDialog __instance)
        {
            if (Config.GetValue(TagColorDialog))
            {
                __instance.Slot.Tag = "Developer";
            }
            return true;
        }

    }
    [HarmonyPatch(typeof(PhotoMetadata))]
    private static class PhotoMetadataPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("NotifyOfScreenshot")]
        private static bool NotifyOfScreenshotPrefix(PhotoMetadata __instance)
        {
            if (Config.GetValue(TagPhotos))
            {
                __instance.Slot.Tag = "Photo";
            }
            return true;
        }
    }
}
