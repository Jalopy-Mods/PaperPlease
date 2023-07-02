using System.Collections;
using JaLoader;
using UnityEngine;

namespace Jalopy_PaperPlease
{
    public class JalopyPaperPlease : Mod
    {
        public override string ModID => "DDRPaperPlease";
        public override string ModName => "DDR Paper Please";
        public override string ModAuthor => "MeblIkea";
        public override string ModDescription => "This mod adds Paper Please frontier music to the game.";
        public override string ModVersion => "1.0.0";

        public override bool UseAssets => true;
        public override WhenToInit WhenToInit => WhenToInit.InGame; // OR WhenToInit.InMenu (In menu is both)

        public override void Start()
        {
            var sound = LoadAsset("ddrpaperplease", "paper_sound_emitter", "");
            StartCoroutine(WaitForBorder(sound));
        }

        private static IEnumerator WaitForBorder(GameObject sound)
        {
            yield return GameObject.Find("Hub_02");
            yield return GameObject.Find("CSFR_Hub_Sturovo(Clone)");
            yield return GameObject.Find("HUN_Letenye");
            yield return GameObject.Find("CSFR_Hub_Sturovo");
            yield return GameObject.Find("BUL_HUB_01 1");
            yield return GameObject.Find("YUGO_Hub_01");
            foreach (var gameobject in FindObjectsOfType<GameObject>())
            {
                if (gameobject.GetComponents<BorderLogicC>().Length <= 0) continue;
                var nO = Instantiate(sound, gameobject.transform);
                var position = nO.transform.position;
                nO.transform.position = new Vector3(position.x, position.y - 2, position.z);
            }
        }

    }
}