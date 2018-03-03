using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages and starts given dialogues.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    /// <summary>
    /// Use this for initialization 
    /// </summary>
    void Start()
    {
        sentences = new Queue<string>();
    }

    /// <summary>
    /// Starts dialogue queue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        UnityEngine.Debug.Log("Starting dialogue for NPC: " + dialogue.name);

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    int RunCode(string code)
    {
        // TODO Rename this am.Name

        AppDomain appDomain = AppDomain.CurrentDomain;
        AssemblyName assemblyName = new AssemblyName();
        assemblyName.Name = "TestAsm";
        AssemblyBuilder assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("testmod", "TestAsm.exe");
        TypeBuilder typeBuilder = moduleBuilder.DefineType("mytype", TypeAttributes.Public);
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("hi", MethodAttributes.Public | MethodAttributes.Static, null, null);
        assemblyBuilder.SetEntryPoint(methodBuilder);

        ILGenerator ilGenerator = methodBuilder.GetILGenerator();
        ilGenerator.EmitWriteLine("Hello World");
        ilGenerator.Emit(OpCodes.Ret);
        typeBuilder.CreateType();
        assemblyBuilder.Save("TestAsm.exe");

        // Prepare the process to run.
        ProcessStartInfo start = new ProcessStartInfo();
        // Enter the executable to run, including the complete path.
        start.FileName = "TestAsm.exe";
        start.WindowStyle = ProcessWindowStyle.Hidden;
        start.CreateNoWindow = true;

        // Run the external process & wait for it to finish
        int exitCode;
        using (Process process = Process.Start(start))
        {
            process.WaitForExit();

            // Retrieve the app's exit code
            exitCode = process.ExitCode;
        }

        return exitCode;
    }
}
