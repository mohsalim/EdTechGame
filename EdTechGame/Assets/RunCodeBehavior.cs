using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Assets.Tutorials;

public class RunCodeBehavior : MonoBehaviour
{
    /// <summary>
    /// Component to grab code from.
    /// 
    /// Note: we want to grab Text component from the InputField component instead of directly caching the Text component.
    /// This is to prevent cut-off text due to cropped box of Text component.
    /// https://forum.unity.com/threads/input-field-text-does-not-return-the-whole-thing.355042/
    /// </summary>
    public InputField CodeInputField = null;

    /// <summary>
    /// Component to put output to.
    /// </summary>
    public Text outputText = null;

    /// <summary>
    /// Prefix for output.
    /// </summary>
    private const string OUTPUT_PREFIX = "Output:\n";

    /// <summary>
    /// Attach button event during initialization.
    /// </summary>
	void Start()
    {
        Button button = gameObject.GetComponent(typeof(Button)) as Button;
        if (button != null && button.onClick != null)
        {
            button.onClick.AddListener(RunCode);
        }
    }

    /// <summary>
    /// Run code and update UI if it passed or not.
    /// </summary>
    private void RunCode()
    {
        Debug.Log("Run Code button clicked. Input text: " + CodeInputField.text);

        // Build and run code.
        RunPythonCode(CodeInputField.text);
    }

    /// <summary>
    /// Run python code using IronPython.
    /// TODO: try/catch ??
    /// </summary>
    /// <param name="code"></param>
    private void RunPythonCode(string code)
    {
        // Create engine and scope.
        ScriptEngine engine = Python.CreateEngine();
        ScriptScope scope = engine.CreateScope();

        // Add source code to engine.
        ScriptSource sourceCode = engine.CreateScriptSourceFromString(code);

        // Create streams for output and errors.
        MemoryStream streamOutput = new MemoryStream();
        MemoryStream streamError = new MemoryStream();

        // Set default encoding to both streams.
        engine.Runtime.IO.SetOutput(streamOutput, Encoding.Default);
        engine.Runtime.IO.SetErrorOutput(streamError, Encoding.Default);

        // Handle any exception when executing code.
        string exception = "";
        try
        {
            // Execute code within scope.
            string executionResult = sourceCode.Execute<string>(scope);
        }
        catch (Exception ex)
        {
            // Most errors will come out as exception instead of through error stream.
            exception = ex.Message;
            Debug.Log("Exception:  " + exception);
        }

        // Get output and errors from stream.
        string output = Encoding.Default.GetString(streamOutput.ToArray());
        Debug.Log("Captured output: " + output);
        string errors = Encoding.Default.GetString(streamError.ToArray());
        Debug.Log("Captured error: " + errors);

        // Show player the results.
        string totalOutput = output + errors + exception;
        outputText.text = OUTPUT_PREFIX + totalOutput;

        // TODO cache in variable as optimization?
        TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (tutorialManager.currentProblem != null)
        {
            string hint;
            if (tutorialManager.currentProblem.ValidateAnswer(code, totalOutput, out hint))
            {
                dialogueManager.StartSuccessMessage(tutorialManager.currentDialogue);
            }
            else
            {
                dialogueManager.StartHint(hint);
            }
        }
    }
}
