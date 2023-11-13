using PixelCrushers.DialogueSystem;
using ProjectTarnished.Character;
using ProjectTarnished.Controllers;
using UnityEngine;

public class SkillsLua : MonoBehaviour
{
    private void Start()
    {
        RegisterLuaFunctions();
    }

    private static bool hasRegisteredLuaFunctions = false;

    public static void RegisterLuaFunctions()
    {
        if (hasRegisteredLuaFunctions) return;
        hasRegisteredLuaFunctions = true;

        Lua.RegisterFunction("SkillCheck", null, SymbolExtensions.GetMethodInfo(() => SkillCheck(string.Empty, (double)0)));
    }

    private static CharacterSkills GetSkills()
    {
        return DialogueManager.CurrentConversationState.subtitle.listenerInfo.transform.GetComponent<CharacterSkills>();
    }

    private static CharacterStatSheet GetStatSheet()
    {
        return DialogueManager.CurrentConversationState.subtitle.listenerInfo.transform.GetComponent<CharacterStatSheet>();
    }

    private static bool SkillCheck(string skillName, double DC)
    {
        if (GetSkills().SkillCheck(skillName, (int)DC, out int roll))
        {
            ActivityLogController.Instance.AddActivityLog($"{GetStatSheet().StatSheet.CharacterFirstName} rolls {skillName} check: Success {roll}/{DC}");

            return true;
        }
        else
        {
            ActivityLogController.Instance.AddActivityLog($"{GetStatSheet().StatSheet.CharacterFirstName} rolls {skillName} check: Failure {roll}/{DC}");

            return false;
        }
    }
}