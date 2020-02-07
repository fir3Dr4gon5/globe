using System;
using System.Diagnostics;
using System.ComponentModel;

namespace globe_webapi
{

public class TempWorker
{
    public CpuTemp  GetTempFromProcess()
    {
    
        using (Process process = new Process())
	{
	    var cpu_temp = new CpuTemp();
	    
	    process.StartInfo.FileName = "sensors";
	    process.StartInfo.Arguments = "-u"; 
	    process.StartInfo.UseShellExecute = false;
	    process.StartInfo.RedirectStandardOutput = true;
	    process.StartInfo.RedirectStandardError = true;
	    process.Start();
	
	    string line;

            while ((line = process.StandardOutput.ReadLine()) != null)
	    {
	        if  (line.Contains("temp3_input")) 
	        {
	            cpu_temp.cpu1_temp  = line.Split(':')[1]?.Trim();
		    
	        }
		
		if  (line.Contains("temp2_input")) 
	        {
	            cpu_temp.cpu0_temp  = line.Split(':')[1]?.Trim();
		    
	        }
		
		if  (line.Contains("temp1_input")) 
	        {
	            cpu_temp.mb_temp  = line.Split(':')[1]?.Trim();
		    
	        }
            }
	
	    process.StandardOutput.ReadToEnd(); // ignore
	    string err = process.StandardError.ReadToEnd();
	    Console.WriteLine(err);
	    process.WaitForExit();
	    
            return cpu_temp;
	}
	    

	
    }    
    
}

}