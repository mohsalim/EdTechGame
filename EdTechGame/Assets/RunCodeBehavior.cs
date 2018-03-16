//using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CSharp;

public class RunCodeBehavior : MonoBehaviour
{
    /// <summary>
    /// Component to grab code from.
    /// </summary>
    public Text inputText = null;

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
        Debug.Log("Run Code button clicked. Input text: " + inputText.text);

        // Build and run code.
        BuildAndRunCode(inputText.text);
    }

    /// <summary>
    /// Compile and run code on the fly. Delete executable after execution is complete.
    /// Based on my code:
    /// https://www.linkedin.com/pulse/compile-run-c-code-string-variable-runtime-ahmed-karim/
    /// </summary>
    /// <param name="code"></param>
    private int BuildAndRunCode(string code)
    {
        // Compiler the code.
        string outputExe = "TestAsm.exe";
        CompilerParameters parameters = new CompilerParameters()
        {
            // Generate EXE file (not DLL).
            GenerateExecutable = true,
            OutputAssembly = outputExe
        };
        CSharpCodeProvider codeProvider = new CSharpCodeProvider();
        CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, code);

        // Check if there was any compilation errors.
        if (results.Errors.Count > 0)
        {
            // Concat all errors.
            string errors = "";
            foreach (CompilerError compilerError in results.Errors)
            {
                errors += "Line number " + compilerError.Line 
                    + ", Error Number: " + compilerError.ErrorNumber 
                    + ", '" + compilerError.ErrorText + ";" 
                    + Environment.NewLine + Environment.NewLine;
            }

            Debug.Log("Code compiler errors: " + errors);

            // Update output text.
            outputText.text = OUTPUT_PREFIX + errors;
            return -1;
        }

        // Get absolute file path from relative file path.
        string fullFilePath = Path.GetFullPath(results.PathToAssembly);
        Debug.Log("Code compiled to: " + fullFilePath);
        
        // Prepare the process to run.
        System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo()
        {
            // Enter the executable to run including the complete path.
            FileName = fullFilePath,
            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
            CreateNoWindow = false,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        // Run the external process & wait for it to finish.
        Debug.Log("About to start process...");
        int exitCode;
        string output;
        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
        {
            // Retrieve the output from the output stream.
            output = process.StandardOutput.ReadToEnd();

            // Wait for the process to exit.
            process.WaitForExit();

            // Get exit code.
            exitCode = process.ExitCode;
        }

        Debug.Log("Process finished with exit code: " + exitCode);
        Debug.Log("Process finished with output: " + output);

        // Update output text.
        outputText.text = OUTPUT_PREFIX + output;

        // Check if file exists.
        if (File.Exists(fullFilePath))
        {
            Debug.Log("Deleting file: " + fullFilePath);
            try
            {
                // Delete the file.
                File.Delete(fullFilePath);
            }
            catch (IOException ex)
            {
                Debug.Log("Failed to delete file: " + parameters.OutputAssembly + ". Exception: " + ex);
            }
        }

        // Return the exit code.
        return exitCode;
    }
}
