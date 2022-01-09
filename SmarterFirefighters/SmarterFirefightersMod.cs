﻿using CitiesHarmony.API;
using HarmonyLib;
using ICities;
using System.Reflection;

namespace SmarterFirefighters
{
    public class Mod : IUserMod
    {
        public string Name => "Smarter Firefighters: Improved AI";
        public string Description => "Improves firefighter AI by prioritizing nearby fires to combat fire spread.";
        public void OnEnabled()
        {
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }
    }

    public static class Patcher
    {
        private const string HarmonyId = "taalbrecht.SmarterFirefighters";

        private static bool patched = false;

        public static void PatchAll()
        {
            if (patched) return;

            UnityEngine.Debug.Log("SmarterFirefighters Activated");

            patched = true;

            var harmony = new Harmony("taalbrecht.SmarterFirefighters");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void UnpatchAll()
        {
            if (!patched) return;

            var harmony = new Harmony(HarmonyId);
            harmony.UnpatchAll(HarmonyId);

            patched = false;

            UnityEngine.Debug.Log("SmarterFirefighters Deactivated");
        }
    }
}
