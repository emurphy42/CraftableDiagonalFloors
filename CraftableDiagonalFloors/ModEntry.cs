using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace CraftableDiagonalFloors
{
    public class ModEntry : Mod
    {
        private static readonly List<string> baseRecipes = new()
        {
            // TODO
            //"Brick Floor",
            "Crystal Floor",
            //"Rustic Plank Floor",
            "Stone Floor",
            //"Stone Walkway Floor",
            //"Straw Floor",
            "Weathered Floor",
            "Wood Floor"
        };

        private static readonly List<string> recipeVariations = new()
        {
            "NW",
            "NE",
            "SW",
            "SE"
        };

        public override void Entry(IModHelper helper)
        {
            Helper.Events.GameLoop.SaveLoaded += (_sender, _e) => OnSaveLoaded(_sender, _e);
            // TODO also patch buying recipes from Robin
            // TODO also patch farmhand joining
        }

        private void OnSaveLoaded(object? _sender, SaveLoadedEventArgs _e)
        {
            this.Monitor.Log("[Craftable Diagonal Floors] Checking crafting recipes", LogLevel.Trace);
            foreach (var baseRecipe in baseRecipes)
            {
                this.Monitor.Log($"[Craftable Diagonal Floors] baseRecipe = {baseRecipe}", LogLevel.Trace);
                if (Game1.player.knowsRecipe(baseRecipe))
                {
                    LearnRecipeVariations(baseRecipe);
                }
            }
        }

        private void LearnRecipeVariations(string baseRecipe)
        {
            var normalizedBaseRecipe = baseRecipe.Replace(" ", "");
            foreach (var recipeVariation in recipeVariations)
            {
                var moddedRecipe = $"JohnPeters.Craft{normalizedBaseRecipe}_{recipeVariation}";
                this.Monitor.Log($"[Craftable Diagonal Floors] moddedRecipe = {moddedRecipe}", LogLevel.Trace);
                if (!Game1.player.knowsRecipe(moddedRecipe))
                {
                    this.Monitor.Log($"[Craftable Diagonal Floors] Adding recipe {moddedRecipe}", LogLevel.Debug);
                    Game1.player.craftingRecipes.Add(moddedRecipe, 0); // 0 = number of times crafted
                }
            }
        }
    }
}