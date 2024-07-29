using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JaLoaderUnity4;
using UnityEngine;

namespace Jalopy_PaperPlease
{
    public class ModForUnity4 : ModUnity4
    {
        public override string ModID => "DDRPaperPlease";
        public override string ModName => "DDR Papers Please";
        public override string ModAuthor => "MeblIkea";
        public override string ModDescription => "This mod adds Papers Please frontier music to the game.";
        public override string ModVersion => "1.1.0";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/PaperPlease/";

        public override bool UseAssets => true;
        public override WhenToInit WhenToInit => WhenToInit.InGame;

        private GameObject sound;

        public override void EventsDeclaration()
        {
            base.EventsDeclaration();

            EventsManager.Instance.OnRouteGenerated += OnRouteGenerated;
        }

        public override void OnEnable()
        {
            sound = LoadAsset<GameObject>("ddrpaperplease_1.0", "paper_sound_emitter", "", "");
            sound.SetActive(false);
        }

        private void OnRouteGenerated(string startLocation, string endLocation, int distance)
        {
            Debug.Log("Route generated");
            Invoke("AddSound", 5);
        }

        public override void Start()
        {
            base.Start();

            Invoke("AddSound", 5);
        }

        private void AddSound()
        {
            foreach (var border in FindObjectsOfType<BorderLogic>())
            {
                if (border.transform.Find("PapersPleaseSong")) // it already has the song
                    continue;

                var obj = Instantiate(sound as UnityEngine.Object) as GameObject;
                obj.transform.parent = border.transform;
                obj.transform.position = border.transform.position;
                obj.name = "PapersPleaseSong";
                obj.SetActive(true);
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 2, obj.transform.position.z);
            }
        }
    }
}
