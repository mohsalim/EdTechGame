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
    /// <summary>
    /// Name of flag for tracking if the dialogue box is open or closed.
    /// </summary>
    private const string IS_OPEN_FLAG = "IsOpen";

    /// <summary>
    /// Text for NPC's name.
    /// </summary>
    public Text NameText;

    /// <summary>
    /// Text for current sentence.
    /// </summary>
    public Text DialogueText;

    /// <summary>
    /// Animator for opening/closing dialogue box.
    /// </summary>
    public Animator AnimatorInterface;

    /// <summary>
    /// Queue of setences.
    /// </summary>
    private Queue<string> Sentences;

    /// <summary>
    /// Use this for initialization 
    /// </summary>
    void Start()
    {
        Sentences = new Queue<string>();
    }

    /// <summary>
    /// Starts dialogue queue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        UnityEngine.Debug.Log("Starting dialogue for NPC: " + dialogue.name);

        // Open the the box. TODO Should we do this after we've cleared the sentences and updated the names?
        AnimatorInterface.SetBool(IS_OPEN_FLAG, true);

        // Clear out the current queue.
        Sentences.Clear();

        // Update the NPC's name.
        NameText.text = dialogue.name;

        // Enqueue the sentences from the given dialogue.
        foreach (string sentence in dialogue.sentences)
        {
            Sentences.Enqueue(sentence);
        }

        // Start displaying the new sentences.
        DisplayNextSentence();
    }

    /// <summary>
    /// Display next sentence in queue.
    /// </summary>
    public void DisplayNextSentence()
    {
        // If there are no more sentences.
        if (Sentences.Count == 0)
        {
            // Then end the dialogue and return.
            EndDialogue();
            return;
        }

        // Get the next sentence in the queue.
        string sentence = Sentences.Dequeue();

        // Stop other coroutines (say if the user is clicking the "Continue" button too fast).
        StopAllCoroutines();

        // Start a new coroutine to type out the sentence.
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Iterator to type out each sentence character at a time.
    /// Gives the feel that the NPC is actually speaking to you via text.
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        UnityEngine.Debug.Log("Typing sentence: " + sentence);

        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }

    /// <summary>
    /// End dialogue by closing the the box.
    /// </summary>
    void EndDialogue()
    {
        UnityEngine.Debug.Log("Ending dialogue.");
        AnimatorInterface.SetBool(IS_OPEN_FLAG, false);
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
