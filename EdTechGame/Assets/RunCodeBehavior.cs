//using Microsoft.CSharp;
//using System.CodeDom.Compiler;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class RunCodeBehavior : MonoBehaviour
{
    public InputField codeInputField = null;

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

        // TODO: Test run.
        RunCode2(null);
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
