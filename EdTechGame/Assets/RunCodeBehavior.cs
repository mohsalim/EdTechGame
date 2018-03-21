using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Runtime.InteropServices;

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
    /// Is this a build run or an editor run? This will determine certain variables.
    /// TODO Consider a config file.
    /// </summary>
    private const bool IS_BUILD = false;

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

        // Debugging to figure out Mono.exe path that the code expects during runtime.
        // This is useful to know during Editor runtime and Build runtime.
        MonoToolsLocator();

        // Build and run code.
        BuildAndRunCode(inputText.text);
    }

    /// <summary>
    /// Needed to check module paths. Original source summary is below:
    /// 
    /// Due to an issue with shadow copying  and app domains in mono, we cannot currently use 
    /// Process.GetCurrentProcess ().MainModule.FileName (which would give the same result)
    /// when running in an AppDomain (eg System.Web hosts).
    ///
    /// Using native Windows API to get current process filename. This will only
    /// be called when running on Windows.
    /// </summary>
    /// <param name="hModule"></param>
    /// <param name="lpFilename"></param>
    /// <param name="nSize"></param>
    /// <returns></returns>
    [DllImport("kernel32.dll")]
    static extern uint GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [In] int nSize);

    /// <summary>
    /// For debugging Mono production issue.
    /// </summary>
    private void MonoToolsLocator()
    {
        var ev = Environment.GetEnvironmentVariables();
        Debug.Log("MONO_PATH = " + ev["MONO_PATH"]);
        Debug.Log("MONO_GC_PARAMS = " + ev["MONO_GC_PARAMS"]);

        string Mono = null;
        string McsCSharpCompiler;
        string VBCompiler;
        string AssemblyLinker;
        var gac = typeof(Environment).GetProperty("GacPath", BindingFlags.Static | BindingFlags.NonPublic);
        var getGacMethod = gac.GetGetMethod(true);
        var GacPath = Path.GetDirectoryName((string)getGacMethod.Invoke(null, null));
        Debug.Log("GacPath = " + GacPath);

        if (Path.DirectorySeparatorChar == '\\')
        {
            Debug.Log("GacPath reached if.");

            StringBuilder moduleName = new StringBuilder(1024);
            GetModuleFileName(IntPtr.Zero, moduleName, moduleName.Capacity);
            string processExe = moduleName.ToString();
            string fileName = Path.GetFileName(processExe);
            if (fileName.StartsWith("mono") && fileName.EndsWith(".exe"))
                Mono = processExe;

            if (!File.Exists(Mono))
                Mono = Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(GacPath)),
                    "bin\\mono.exe");

            if (!File.Exists(Mono))
                Mono = Path.Combine(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(GacPath))),
                    "mono\\mini\\mono.exe");

            //if (!File.Exists (Mono))
            //	throw new FileNotFoundException ("Windows mono path not found: " + Mono);

            McsCSharpCompiler = Path.Combine(GacPath, "4.5", "mcs.exe");
            if (!File.Exists(McsCSharpCompiler))
            {
                // Starting from mono\mcs\class
                McsCSharpCompiler = Path.Combine(Path.GetDirectoryName(GacPath), "lib", "net_4_x", "mcs.exe");
            }

            //if (!File.Exists (CSharpCompiler))
            //	throw new FileNotFoundException ("C# compiler not found at " + CSharpCompiler);

            VBCompiler = Path.Combine(GacPath, "4.5\\vbnc.exe");
            AssemblyLinker = Path.Combine(GacPath, "4.5\\al.exe");

            if (!File.Exists(AssemblyLinker))
            {
                AssemblyLinker = Path.Combine(Path.GetDirectoryName(GacPath), "lib\\net_4_x\\al.exe");
                //	if (!File.Exists (AssemblyLinker))
                //		throw new FileNotFoundException ("Windows al path not found: " + AssemblyLinker);
            }
        }
        else
        {
            Debug.Log("GacPath reached else.");

            Mono = Path.Combine(GacPath, "bin", "mono");
            if (!File.Exists(Mono))
                Mono = "mono";

            var mscorlibPath = new Uri(typeof(object).Assembly.CodeBase).LocalPath;
            McsCSharpCompiler = Path.GetFullPath(Path.Combine(mscorlibPath, "..", "..", "..", "..", "bin", "mcs"));
            if (!File.Exists(McsCSharpCompiler))
                McsCSharpCompiler = "mcs";

            VBCompiler = Path.GetFullPath(Path.Combine(mscorlibPath, "..", "..", "..", "..", "bin", "vbnc"));
            if (!File.Exists(VBCompiler))
                VBCompiler = "vbnc";

            AssemblyLinker = Path.GetFullPath(Path.Combine(mscorlibPath, "..", "..", "..", "..", "bin", "al"));
            if (!File.Exists(AssemblyLinker))
                AssemblyLinker = "al";
        }

        Debug.Log("Mono = " + Mono);
        Debug.Log("McsCSharpCompiler = " + McsCSharpCompiler);
        Debug.Log("VBCompiler = " + VBCompiler);
        Debug.Log("AssemblyLinker = " + AssemblyLinker);
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
        // NOTE: I need this for EXE build vs development build.
        string outputExe = (IS_BUILD ? AppDomain.CurrentDomain.BaseDirectory + "\\" : "" ) + "TestAsm.exe";

        Debug.Log("Setting parameters in order to compile code to: " + outputExe);
        CompilerParameters parameters = new CompilerParameters()
        {
            // Generate EXE file (not DLL).
            GenerateExecutable = true,
            OutputAssembly = outputExe
        };

        Debug.Log("Compiling code.");
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
            UseShellExecute = false,
            RedirectStandardError = true,
            ErrorDialog = false
        };

        // Run the external process & wait for it to finish.
        Debug.Log("About to start process...");
        int? exitCode = null;
        string output = null, error = null, stacktrace = null;

        try
        {
            using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
            {
                // Retrieve the output from the output stream.
                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();

                // Wait for the process to exit.
                process.WaitForExit();

                // Get exit code.
                exitCode = process.ExitCode;
            }
        }
        catch (Exception ex)
        {
            stacktrace = ex.StackTrace;
        }

        if (exitCode.HasValue)
        {
            Debug.Log("Process finished with exit code: " + exitCode);
        }

        if (output != null)
        {
            Debug.Log("Process finished with output: " + output);
        }

        if (error != null)
        {
            Debug.Log("Process finished with error: " + error);
        }

        if (stacktrace != null)
        {
            Debug.Log("Process finished with stacktrace: " + stacktrace);
        }

        // Update output text.
        outputText.text = OUTPUT_PREFIX + output + error + stacktrace;

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
        return exitCode.Value;
    }
}
