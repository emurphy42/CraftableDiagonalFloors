using StardewValley;
using StardewModdingAPI;

namespace CraftableDiagonalFloors
{
    internal class ObjectPatches
    {
        // initialized by ModEntry.cs
        public static ModEntry ModInstance;

        private static readonly List<string> baseRecipes = new()
        {
            // TODO
            "Brick Floor",
            "Crystal Floor",
            "Rustic Plank Floor",
            "Stone Floor",
            //"Stone Walkway Floor",
            "Straw Floor",
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

        public static void Item_LearnRecipe_Postfix(Farmer player = null)
        {
            ModInstance.Monitor.Log("[Craftable Diagonal Floors] Reacting to Item.LearnRecipe()", LogLevel.Trace);
            CheckKnownRecipes(player ?? Game1.player);
        }

        public static void CheckKnownRecipes(Farmer player)
        {
            ModInstance.Monitor.Log("[Craftable Diagonal Floors] Checking crafting recipes", LogLevel.Trace);
            foreach (var baseRecipe in baseRecipes)
            {
                ModInstance.Monitor.Log($"[Craftable Diagonal Floors] baseRecipe = {baseRecipe}", LogLevel.Trace);
                if (player.knowsRecipe(baseRecipe))
                {
                    LearnRecipeVariations(baseRecipe);
                }
            }
        }

        private static void LearnRecipeVariations(string baseRecipe)
        {
            var normalizedBaseRecipe = baseRecipe.Replace(" ", "");
            foreach (var recipeVariation in recipeVariations)
            {
                var moddedRecipe = $"JohnPeters.Craft{normalizedBaseRecipe}_{recipeVariation}";
                ModInstance.Monitor.Log($"[Craftable Diagonal Floors] moddedRecipe = {moddedRecipe}", LogLevel.Trace);
                if (!Game1.player.knowsRecipe(moddedRecipe))
                {
                    ModInstance.Monitor.Log($"[Craftable Diagonal Floors] Adding recipe {moddedRecipe}", LogLevel.Debug);
                    Game1.player.craftingRecipes.Add(moddedRecipe, 0); // 0 = number of times crafted
                }
            }
        }
    }
}
