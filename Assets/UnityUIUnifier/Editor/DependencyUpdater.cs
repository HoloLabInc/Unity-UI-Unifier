using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityUIUnifier
{
    struct Dependency
    {
        public string AsmdefName;
        public string Symbol;

        internal Dependency(string asmdefName, string symbol)
        {
            AsmdefName = asmdefName;
            Symbol = symbol;
        }
    }

    [InitializeOnLoad]
    public class DependencyChecker
    {
        static readonly string uiBindingsAsmdefName = "UnityUIUnifier";

        static readonly List<Dependency> dependencies = new List<Dependency>() {
            new Dependency("Unity.TextMeshPro", "UUU_TEXTMESHPRO_PRESENT")
        };

        static DependencyChecker()
        {
            var references = new List<string>();

            foreach (var dependency in dependencies)
            {
                var asmdefName = dependency.AsmdefName;
                var symbol = dependency.Symbol;

                if (AsmdefExists(asmdefName))
                {
                    AddSymbol(symbol);
                    references.Add(asmdefName);
                }
                else
                {
                    RemoveSymbol(symbol);
                }
            }

            UpdateReferences(uiBindingsAsmdefName, references);
        }

        static string FindAsmdef(string asmdefName)
        {
            var filterString = $"{asmdefName} t:asmdef";
            var asset = AssetDatabase
                .FindAssets(filterString)
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault(str => string.Equals(Path.GetFileNameWithoutExtension(str), asmdefName, StringComparison.CurrentCultureIgnoreCase));
            return asset;
        }

        static bool AsmdefExists(string asmdefName)
        {
            var asset = FindAsmdef(asmdefName);
            return asset != null;
        }

        #region ScriptingDefineSymbols
        static void RemoveSymbol(string symbol)
        {
            var targetGroup = GetCurrentTargetGroup();
            var symbols = GetCurrentSymbols(targetGroup);

            if (symbols.Contains(symbol))
            {
                symbols.Remove(symbol);
                SaveSymbols(targetGroup, symbols);
            }
        }

        static void AddSymbol(string symbol)
        {
            var targetGroup = GetCurrentTargetGroup();
            var symbols = GetCurrentSymbols(targetGroup);

            if (!symbols.Contains(symbol))
            {
                symbols.Add(symbol);
                SaveSymbols(targetGroup, symbols);
            }
        }

        static BuildTargetGroup GetCurrentTargetGroup()
        {
            var activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            return BuildPipeline.GetBuildTargetGroup(activeBuildTarget);
        }

        static List<string> GetCurrentSymbols(BuildTargetGroup targetGroup)
        {
            return PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup).Split(';').ToList();
        }

        static void SaveSymbols(BuildTargetGroup targetGroup, List<string> symbols)
        {
            var symbol = string.Join(";", symbols);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbol);
        }
        #endregion

        #region AsmdefReferences
        static void UpdateReferences(string asmdefName, List<string> references)
        {
            var asmdefPath = FindAsmdef(asmdefName);
            try
            {
                var asmdef = File.ReadAllText(asmdefPath);
                asmdef = ReplaceReferences(asmdef, references);
                File.WriteAllText(asmdefPath, asmdef);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }


        static string ReplaceReferences(string asmdef, List<string> references)
        {
            var reg = new Regex(@"""references""\s*:\s*\[(\s*(""[^""]*"")\s*,?)*\s*\]\s*,[ \t]*", RegexOptions.Singleline);

            var match = reg.Match(asmdef);
            if (match.Success)
            {
                var newReferences = ConvertToReferencesJson(references);
                asmdef = asmdef.Replace(match.Value, newReferences);
            }
            return asmdef;
        }

        static string ConvertToReferencesJson(List<string> references)
        {
            var sb = new StringBuilder();
            sb.Append(@"""references"": ");
            if (references.Count == 0)
            {
                sb.Append(@"[],");
            }
            else
            {
                var referencesText = string.Join(",\n", references.Select(x => $"        \"{x}\""));
                sb.AppendLine("[");
                sb.AppendLine(referencesText);
                sb.Append("    ],");
            }

            return sb.ToString();
        }
        #endregion
    }
}