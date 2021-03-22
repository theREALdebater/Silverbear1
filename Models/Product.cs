namespace Silverbear1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Memory { get; set; }
        public string Storage { get; set; }
        public string Usb { get; set; }
        public string Gpu { get; set; }
        public string Weight { get; set; }
        public string Psu { get; set; }
        public string Cpu { get; set; }

        public double MemoryInGiB { get; set; }

    }
}