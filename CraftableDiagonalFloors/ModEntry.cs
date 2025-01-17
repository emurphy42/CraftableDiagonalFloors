using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace CraftableDiagonalFloors
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            ObjectPatches.ModInstance = this;

            // Check crafting recipes when starting game or connecting to multiplayer world
            Helper.Events.GameLoop.SaveLoaded += (_sender, _e) => OnSaveLoaded(_sender, _e);

            // Check crafting recipes after learning a recipe
            var harmony = new Harmony(this.ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Item), nameof(StardewValley.Item.LearnRecipe)),
                postfix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.Item_LearnRecipe_Postfix))
            );
        }

        private void OnSaveLoaded(object? _sender, SaveLoadedEventArgs _e)
        {
            this.Monitor.Log("[Craftable Diagonal Floors] Reacting to OnSaveLoaded()", LogLevel.Trace);
            ObjectPatches.CheckKnownRecipes(Game1.player);
        }
    }
}