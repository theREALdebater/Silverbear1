use Silverbear1;

if exists(select * from sys.tables t where t.name = 'Storage_Device') drop table Storage_Device;
if exists(select * from sys.tables t where t.name = 'CPU_Model') drop table CPU_Model;
if exists(select * from sys.tables t where t.name = 'GPU_Model') drop table GPU_Model;
if exists(select * from sys.tables t where t.name = 'Product') drop table Product;

create table Storage_Device (
    Storage_Device_ID int 
        constraint Storage_Device_PK primary key,
    Capacity nvarchar(15) not null,
    Storage_Type nvarchar(10) not null
);

create table CPU_Model (
    CPU_Model_ID int 
        constraint CPU_Model_PK primary key,
    Maker nvarchar(30) not null,
    "Name" nvarchar(40) not null,
    Clock_Speed nvarchar(10) null
);

create table GPU_Model (
    GPU_Model_ID int 
        constraint GPU_Model_PK primary key,
    Maker nvarchar(30) not null,
    "Name" nvarchar(40) not null
);

create table Product (
    Product_ID int 
        constraint Product_PK primary key,
    Memory_in_MiB int,
    Storage int 
        constraint Product_Storage_Device_FK 
        references Storage_Device (Storage_Device_ID),
    USB_2 int 
        constraint Product_USB_2_NN not null 
        constraint Product_USB_2_Def default 0,
    USB_3 int
        constraint Product_USB_3_NN not null 
        constraint Product_USB_3_Def default 0,
    USB_C int 
        constraint Product_USB_C_NN not null 
        constraint Product_USB_C_Def default 0,
    GPU int
        constraint Product_GPU_Model_FK
        references GPU_Model (GPU_Model_ID),
    Weight_in_Kg float,
    PSU_in_W int,
    CPU int
        constraint Product_CPU_Model_FK
        references CPU_Model (CPU_Model_ID),
);

insert into Storage_Device values
    ( 1, '1 TB', 'SSD'),
    ( 2, '2 TB', 'HDD'),
    ( 3, '3 TB', 'HDD'),
    ( 4, '4 TB', 'HDD'),
    ( 5, '750 GB', 'SDD'),
    ( 6, '2 TB', 'SDD'),
    ( 7, '500 GB', 'SDD'),
    ( 8, '80 GB', 'SDD');

insert into CPU_Model values
    ( 1, 'Intel', 'Celeron N3050', null),
    ( 2, 'AMD', 'FX 4300', null),
    ( 3, 'AMD', 'Athlon 5150 Quad-Core APU', null),
    ( 4, 'AMD', 'FX 8-Core Black Edition FX-8350', null),
    ( 5, 'AMD', 'FX 8-Core Black Edition FX-8370', null),
    ( 6, 'Intel', 'Core i7-6700K', '4 GHz'),
    ( 7, 'Intel', 'Core i5-6400', null),
    ( 8, 'Intel', 'Core i7 Extreme Edition', '3 GHz');

insert into GPU_Model values
    ( 1, 'NVIDIA', 'GeForce GTX 770'),
    ( 2, 'NVIDIA', 'GeForce GTX 960'),
    ( 3, 'NVIDIA', 'GeForce GTX 1080'),
    ( 5, 'AMD', 'Radeon R7360'),
    ( 6, 'AMD', 'Radeon RX 480'),
    ( 7, 'AMD', 'Radeon R9 380'),
    ( 8, 'AMD', 'FirePro W4100');

insert into Product values
    (  1,  8 * 1024,  1,  4,  2,  0,  1,  8.1,  500,  1),
    (  2, 16 * 1024,  2,  4,  3,  0,  2,  8.1,  500,  2),
    (  3,  8 * 1024,  3,  4,  4,  0,  5,  8.1,  450,  3),
    (  4, 16 * 1024,  4,  5,  4,  0,  3,  8.1,  500,  4),
    (  5, 32 * 1024,  5,  2,  2,  1,  6,  8.1, 1000,  5),
    (  6, 32 * 1024,  6,  0,  4,  0,  7,  8.1,  450,  6),
    (  7,  8 * 1024,  7,  0,  8,  0,  3,  8.1, 1000,  7),
    (  8, 16 * 1024,  2,  4,  0,  0,  1,  8.1,  750,  7),
    (  9,  2 * 1024,  2, 10, 10, 10,  8,  8.1,  508,  8),
    ( 10,       512,  8,  4, 19,  0,  6,  8.1,  700,  7);

-- end

