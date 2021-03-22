using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Silverbear1.Database
{
    public class Product
    {
        public int Id { get; set; }
        public int MemoryInMib { get; set; }
        public StorageDevice Storage { get; set; }
        public int Usb2 { get; set; }
        public int Usb3 { get; set; }
        public int UsbC { get; set; }
        public GpuModel Gpu { get; set; }
        public double WeightInKg { get; set; }
        public int PsuInW { get; set; }
        public CpuModel Cpu { get; set; }
    }
}
