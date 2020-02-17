using System;
using System.ComponentModel;
using System.Diagnostics;

namespace globe_webapi {

	public class TempWorker {
		static bool enabled = true;

		public CpuTemp GetTempFromProcess () {
			var cpu_temp = new CpuTemp ();

			if (enabled) {
				using (Process process = new Process ()) {

					process.StartInfo.FileName = "sensors";
					process.StartInfo.Arguments = "-u";
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.RedirectStandardError = true;

					try {

						process.Start ();

						string line;

						while ((line = process.StandardOutput.ReadLine ()) != null) {
							if (line.Contains ("temp3_input")) {
								cpu_temp.cpu1_temp = line.Split (':') [1]?.Trim ();

							}

							if (line.Contains ("temp2_input")) {
								cpu_temp.cpu0_temp = line.Split (':') [1]?.Trim ();

							}

							if (line.Contains ("temp1_input")) {
								cpu_temp.mb_temp = line.Split (':') [1]?.Trim ();

							}
						}

						process.StandardOutput.ReadToEnd (); // ignore
						string err = process.StandardError.ReadToEnd ();
						Console.WriteLine (err);
						process.WaitForExit ();
					} catch (System.ComponentModel.Win32Exception) {
						Console.WriteLine ($"new Process Failed. Is {process.StartInfo.FileName} Installed?");
						enabled = false;
					}
				}

			}

			return cpu_temp;

		}

	}

}