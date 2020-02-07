using System;

namespace globe_webapi
{
    public class CpuTemp
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string cpu0_temp { get; set; }
	public string cpu1_temp { get; set; }
	public string mb_temp { get; set; }
    }
}
