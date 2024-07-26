using System;
using System.Collections;
using JaLoader;
using UnityEngine;

namespace Jalopy_PaperPlease
{
    public class JalopyPaperPlease : Mod
    {
        public override string ModID => "DDRPaperPlease";
        public override string ModName => "DDR Papers Please";
        public override string ModAuthor => "MeblIkea";
        public override string ModDescription => "This mod adds Papers Please frontier music to the game.";
        public override string ModVersion => "1.0.1";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/PaperPlease/";

        public override bool UseAssets => true;
        public override WhenToInit WhenToInit => WhenToInit.InGame; // OR WhenToInit.InMenu (In menu is both)

        private GameObject sound;

        public override void EventsDeclaration()
        {
            base.EventsDeclaration();

            EventsManager.Instance.OnRouteGenerated += OnRouteGenerated;
        }

        private void OnRouteGenerated(string startLocation, string endLocation, int distance)
        {
            Invoke("AddSound", 5);
        }

        public override void OnEnable()
        {
            sound = LoadAsset<GameObject>("ddrpaperplease", "paper_sound_emitter", "", ".prefab");
            sound.SetActive(false);
        }

        public override void Start()
        {
            base.Start();

            Invoke("AddSound", 5);
        }

        private void AddSound()
        {
            foreach(var border in FindObjectsOfType<BorderLogicC>())
            {
                if (border.transform.Find("PapersPleaseSong")) // it already has the song
                    continue;

                var obj = Instantiate(sound, border.transform);
                obj.name = "PapersPleaseSong";
                obj.SetActive(true);
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 2, obj.transform.position.z);
            }
        }
    }
}