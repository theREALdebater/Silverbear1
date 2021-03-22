using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Silverbear1.Database
{
    public class SilverbearContext
    {
        public List<StorageDevice> StorageDevices { get; set; } = new List<StorageDevice>();
        public List<GpuModel> GpuModels { get; set; } = new List<GpuModel>();
        public List<CpuModel> CpuModels { get; set; } = new List<CpuModel>();
        public List<Product> Products { get; set; } = new List<Product>();

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //     => options.UseSqlServer(@"Data Source=DESKTOP-BN573MK;Database=Silverbear1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public void FetchAll()
        {
            string connectionString =
                @"Data Source=DESKTOP-BN573MK;Database=Silverbear1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var connection = new SqlConnection(connectionString);
            connection.Open();

            string queryString;
            SqlCommand command;
            SqlDataReader reader;
            
            // Storage Devices
            
            queryString = "select Storage_Device_ID, Capacity, Storage_Type from Storage_Device";
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                StorageDevices.Add(new StorageDevice
                {
                    Id = (int)reader[0], 
                    Capacity = (string)reader[1],
                    StorageType = (string)reader[2]
                });
            }
            reader.Close();
            connection.Close();

            // GPU Models

            connection = new SqlConnection(connectionString);
            connection.Open();
            queryString = "select GPU_Model_ID, Maker, Name from GPU_Model";
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                GpuModels.Add(new GpuModel
                {
                    Id = (int)reader[0], 
                    Maker = (string)reader[1],
                    Name = (string)reader[2]
                });
            }
            reader.Close();
            connection.Close();

            // CPU Models

            connection = new SqlConnection(connectionString);
            connection.Open();
            queryString = "select CPU_Model_ID, Maker, Name, Clock_Speed from CPU_Model";
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                CpuModels.Add(new CpuModel
                {
                    Id = (int)reader[0], 
                    Maker = (string)reader[1],
                    Name = (string)reader[2],
                    ClockSpeed = reader[3] is string ? (string)reader[3] : ""
                });
            }
            reader.Close();
            connection.Close();

            // Products

            connection = new SqlConnection(connectionString);
            connection.Open();
            queryString = "select Product_ID, Memory_in_MiB, Storage, USB_2, USB_3, USB_C, GPU, Weight_in_Kg, PSU_in_W, CPU from Product";
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                var storageId = (int)reader[2];
                var gpuId = (int)reader[6];
                var cpuId = (int)reader[9];
                
                Products.Add(new Product
                {
                    Id = (int)reader[0],
                    MemoryInMib = (int)reader[1], 
                    Storage = StorageDevices.SingleOrDefault(d => d.Id == storageId), 
                    Usb2 = (int)reader[3], 
                    Usb3 = (int)reader[4], 
                    UsbC = (int)reader[5], 
                    Gpu = GpuModels.SingleOrDefault(m => m.Id == gpuId), 
                    WeightInKg = (double)reader[7], 
                    PsuInW = (int)reader[8], 
                    Cpu = CpuModels.SingleOrDefault(m => m.Id == cpuId)
                });
            }
            reader.Close();
            connection.Close();
        }
    }
}
