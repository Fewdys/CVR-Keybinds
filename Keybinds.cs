using ABI_RC.Core;
using ABI_RC.Core.EventSystem;
using ABI_RC.Systems.MovementSystem;
using ABI_RC.Core.Player;
using MelonLoader;
using UnityEngine;
using System.Collections;

namespace Keybinds
{
    public class Keybinds : MelonMod
    {
        public static Rigidbody rbcvr;
        public static Transform trcvr;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg(System.ConsoleColor.Green, "- Keybinds -");
            MelonLogger.Msg(System.ConsoleColor.DarkBlue, "Respawn: LeftCTRL + R");
            MelonLogger.Msg(System.ConsoleColor.DarkBlue, "Remove All Avatars: RightCTRL + Backslash");
            MelonLogger.Msg(System.ConsoleColor.DarkBlue, "Reset Avatar: LeftCTRL + Backslash");
            MelonLogger.Msg(System.ConsoleColor.DarkBlue, "Noclip/Flight/Fly: LeftCTRL + F");
            MelonLogger.Msg(System.ConsoleColor.DarkBlue, "Reload All Avatars: RightShift + R");
            MelonLogger.Msg(System.ConsoleColor.DarkBlue, "ClickTP: Mouse Button 3");
            MelonCoroutines.Start(MouseTPS());
        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Backslash) && Input.GetKey(KeyCode.LeftControl))
            {
                ResetAvatar();
            }
            if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
            {
                Respawn();
            }
            if (Input.GetKeyDown(KeyCode.Backslash) && Input.GetKey(KeyCode.RightControl))
            {
                RemoveAllAvatars();
            }
            if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftControl))
            {
                Fly();
            }
            if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.RightShift))
            {
                ReloadAllAvatars();
            }
        }
        public static void ResetAvatar()
        {
            AssetManagement.Instance.LoadLocalAvatar("17c267db-18c4-4900-bb73-ad323f082640");
            MelonLogger.Msg("Avatar was reset.");
        }
        public static void Respawn()
        {
            RootLogic.Instance.Respawn();
        }

        public static void RemoveAllAvatars()
        {
            CVRPlayerManager.Instance.ClearPlayerAvatars();
            MelonLogger.Msg("All avatars were removed.");
        }

        public void Fly()
        {
            MovementSystem.Instance.ToggleFlight();
        }

        public static void ReloadAllAvatars()
        {
            CVRPlayerManager.Instance.ReloadAllAvatars();
            MelonLogger.Msg("All avatars were reloaded.");
        }

        public static IEnumerator MouseTPS()
        {
            while (true)
            {
                GameObject player = GameObject.Find("_PLAYERLOCAL");

                if (Input.GetKeyDown(KeyCode.Mouse3))
                {
                    var r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    if (Physics.Raycast(r, out RaycastHit raycastHit))
                    {
                        player.transform.position = raycastHit.point;
                    }
                }
                yield return null;
            }

        }
    }
}