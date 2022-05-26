using BepInEx;
using Bepinject;
using System.Reflection;
using HarmonyLib;
using Utilla;
using System.ComponentModel;

namespace Grippy
{
    [Description("HauntedModMenu")]
    [BepInPlugin("org.gaminggreenninja.gtag.Alphawalls", "Alphawalls", "1.0.0")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        private static Harmony harmony;
        static bool inAllowedRoom = false;

        public void OnEnable() // Capitalization on these methods matters
        {
            harmony = new Harmony("com.gaminggreenninja.gtag.Alphawalls");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnDisable()
        {
            harmony.UnpatchSelf();
        }

        [ModdedGamemodeJoin]
        public void RoomJoined(string gamemode)
        {
             static void Main(string[] args)
            Console.WriteLine("LOADED.");
            inAllowedRoom = true;
        }

        [ModdedGamemodeLeave]
        public void RoomLeft(string gamemode)
        {
            static void Main(string[] args)
            Console.WriteLine("UNLOADED.");
            inAllowedRoom = false;
        }

        [HarmonyPatch(typeof(GorillaLocomotion.Player), "GetSlidePercentage")]
        class slidepatch
        {
            static void Postfix(ref float __result)
            {
                if (inAllowedRoom)
                {
                    __result = 0.03f;
                }
            }
        }
    }
}