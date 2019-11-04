using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
   [DefOf]
   public static class CloningRecipesDefOf
   {
		static CloningRecipesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(CloningRecipesDefOf));

		public static HediffDef LuluOrganCloning_ClonedOrgan;
      public static RecipeDef LuluOrganCloning_PrepareOrgan;
      public static RecipeDef LuluOrganCloning_HarvestOrgan;
   }
}
