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
    public InputField codeInputField = null;
    public Text outputText = null;

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
    void RunCode()
    {
        Debug.Log("Run Code button clicked.");

        if (codeInputField == null)
        {
            GameObject gameObjectCodeInputField = GameObject.FindWithTag("CodeInputField");
            codeInputField = gameObjectCodeInputField.gameObject.GetComponent(typeof(InputField)) as InputField;

            if (codeInputField == null)
            {
                Debug.LogError("Could not find CodeInputField");
                return;
            }
        }

        // TODO: Should I store the text component into a variable for optimization?
        Debug.Log("Input text: " + codeInputField.textComponent.text);

        // Build and run code.
        BuildAndRunCode(codeInputField.textComponent.text);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="source"></param>
    public void RunCode1(string source)
    {
        /*
        // Setup for compiling.
        Dictionary<string, string> providerOptions = new Dictionary<string, string>
        {
            { "CompilerVersion","v3.5"}
        };
        CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);
        CompilerParameters compilerParameters = new CompilerParameters();
        string outfile = "D:\\TestInputCode.EXE";
        compilerParameters.OutputAssembly = outfile;
        compilerParameters.GenerateExecutable = true;

        // Compile.
        CompilerResults results = provider.CompileAssemblyFromSource(compilerParameters, source);

        // Print out any errors.
        Debug.Log("Output file: " + outfile);
        Debug.Log("Number of Errors: " + results.Errors.Count);
        foreach (CompilerError compileError in results.Errors)
        {
            Debug.Log("Error: " + compileError.ErrorText);
        }
        */
    }

    /// <summary>
    /// https://stackoverflow.com/a/11779234/2498729
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="args"></param>
    private void RunPythonCode(string cmd, string args)
    {
        System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
        start.FileName = "my/full/path/to/python.exe";
        start.Arguments = string.Format("{0} {1}", cmd, args);
        //start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Debug.Log("Result: " + result);
            }
        }
    }

    /// <summary>
    /// Compile and run code on the fly. Delete executable after execution is complete.
    /// Based on my code:
    /// https://www.linkedin.com/pulse/compile-run-c-code-string-variable-runtime-ahmed-karim/
    /// </summary>
    /// <param name="code"></param>
    public int BuildAndRunCode(string code)
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
        outputText.text = "Output:\n" + output;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public int RunCode2(string code)
    {
        // Use the current domain.
        AppDomain appDomain = AppDomain.CurrentDomain;

        // Create the assembly name.
        AssemblyName assemblyName = new AssemblyName()
        {
            Name = "TestAsm"
        };

        // Construct the executable name: "TestAsm.exe".
        string executableName = assemblyName.Name + ".exe";

        // Build the assembly.
        AssemblyBuilder assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);

        // Build the modeul.
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("TestModule", executableName);

        // Build the type.
        TypeBuilder typeBuilder = moduleBuilder.DefineType("int", TypeAttributes.Public);

        // Build the method.
        MethodBuilder methodBuilder = typeBuilder.DefineMethod("RunUserInput", MethodAttributes.Public | MethodAttributes.Static, null, null);
        assemblyBuilder.SetEntryPoint(methodBuilder);

        // Generate code in-line.
        ILGenerator ilGenerator = methodBuilder.GetILGenerator();
        ilGenerator.EmitWriteLine("Hello World");
        ilGenerator.Emit(OpCodes.Ret);

        // Create the type.
        typeBuilder.CreateType();

        // Save the executable.
        assemblyBuilder.Save(executableName);

        // Prepare the process to run.
        System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo()
        {
            // Enter the executable to run including the complete path.
            FileName = executableName,
            WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
            CreateNoWindow = true
        };

        // Run the external process & wait for it to finish.
        Debug.Log("About to start process...");
        int exitCode;
        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
        {
            process.WaitForExit();

            // Retrieve the app's exit code
            exitCode = process.ExitCode;
        }

        Debug.Log("Process finished with exit code: " + exitCode);
        return exitCode;
    }
}
