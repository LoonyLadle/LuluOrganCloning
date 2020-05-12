using HarmonyLib;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	[StaticConstructorOnStartup]
	static class MyStaticConstructor
	{
		static MyStaticConstructor()
		{
			Harmony harmony = new Harmony("rimworld.loonyladle.organcloning");
			harmony.PatchAll();
		}
	}
}
