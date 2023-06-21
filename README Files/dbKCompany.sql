/* User */
CREATE DATABASE [dbAuth];

USE [dbAuth];

CREATE TABLE [dbo].[MsUser](
	[UserID] [varchar](50) NOT NULL,
	[FullName] [varchar](200) NULL,
	[AreaNo] [varchar](20) NULL,
	[Role] [varchar](20) NULL, /* ADMIN / CASHIER / SALESMAN / SERVICEAD / WORKSHOPMAN / STOREMAN */
	[Password] [varchar](255) NULL, /* CONVERT(VARCHAR(32), HashBytes('MD5', 'abc@123'), 2) */
	[AccessToken] [varchar](255) NULL,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
 CONSTRAINT [PK_MsUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[MsUser] VALUES
('administrator','Administrator',NULL,'ADMIN','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('cashier1','Cashier 1','A00001','CASHIER','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('cashier2','Cashier 2','A00003','CASHIER','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('salesman1','Salesman 1','A00002','SALESMAN','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('servicead1','Service Advisor 1',NULL,'SERVICEAD','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('workshopman1','Workshop Manager 1',NULL,'WORKSHOPMAN','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('storeman1','Store Manager 1',NULL,'STOREMAN','B24331B1A138CDE62AA1F679164FC62F','','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');


/* K-Company */
CREATE DATABASE [dbKCompany];
USE [dbKCompany];

CREATE TABLE [dbo].[MsPage](
	[PageNo] [varchar](50) NOT NULL, /* ADMIN / CASHIER / SALESMAN / SERVICEAD / WORKSHOPMAN / STOREMAN */
	[Description] [varchar](100) NULL,
	[PageController] [varchar](100) NOT NULL,
	[PageAction] [varchar](100) NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
	 CONSTRAINT [PK_MsPage] PRIMARY KEY CLUSTERED 
(
	[PageNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[MsPage] VALUES
('PG00001','Customer','Customer','Index','Y'),
('PG00002','Product','Product','Index','Y'),
('PG00003','Order','Order','Index','Y'),
('PG00004','Order Details','OrderDetails','Index','N'),
('PG00005','Report','Report','Index','Y');

CREATE TABLE [dbo].[MsRoleAccess](
	[Role] [varchar](20) NOT NULL, 
	[PageNo] [varchar](50) NOT NULL
);

INSERT INTO [dbo].[MsRoleAccess] VALUES
('ADMIN','PG00001'),
('ADMIN','PG00002'),
('ADMIN','PG00003'),
('ADMIN','PG00004'),
('ADMIN','PG00005'),
('CASHIER','PG00001'),
('CASHIER','PG00003'),
('CASHIER','PG00004'),
('SALESMAN','PG00001'),
('SALESMAN','PG00003'),
('SALESMAN','PG00004'),
('SERVICEAD','PG00001'),
('SERVICEAD','PG00003'),
('SERVICEAD','PG00004'),
('WORKSHOPMAN','PG00001'),
('WORKSHOPMAN','PG00003'),
('WORKSHOPMAN','PG00004'),
('WORKSHOPMAN','PG00005'),
('STOREMAN','PG00001'),
('STOREMAN','PG00003'),
('STOREMAN','PG00004'),
('STOREMAN','PG00005');

CREATE TABLE [dbo].[MsProductSparepart](
	[ProductNo] [varchar](50) NOT NULL,
	[ProductDesc] [varchar](100) NULL,
	[ProductType] [varchar](10) NULL, /* PRO or SPA */
	[ProductBrand] [varchar](20) NULL,
	[UoM] [varchar](10) NULL, /* PCS or BOX */
	[COGS] DECIMAL(19,2) DEFAULT 0,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
 CONSTRAINT [PK_MsProductSparepart] PRIMARY KEY CLUSTERED 
(
	[ProductNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[MsProductSparepart] VALUES
('P00001','Pencil','PRO','Brand1','PCS',500,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00002','Table','PRO','Brand2','PCS',50000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00003','Bolt','SPA','Brand3','BOX',9000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00004','Shock Breaker','SPA','Brand4','PCS',1200000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00005','Tire','SPA','Brand5','PCS',700000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');

CREATE TABLE [dbo].[MsMarketingArea](
	[AreaNo] [varchar](50) NOT NULL,
	[AreaDesc] [varchar](100) NULL,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
 CONSTRAINT [PK_MsMarketingArea] PRIMARY KEY CLUSTERED 
(
	[AreaNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[MsMarketingArea] VALUES
('A00001','Area Sumatera','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('A00002','Area Jabodetabek','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('A00003','Area Jawa, Bali dan Indonesia Timur','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');

CREATE TABLE [dbo].[MsStore](
	[StoreNo] [varchar](50) NOT NULL,
	[StoreDesc] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[Phone] [varchar](50) NULL,
	[AreaNo] [varchar](50) NOT NULL,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
 CONSTRAINT [PK_MsStore] PRIMARY KEY CLUSTERED 
(
	[StoreNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[MsStore] VALUES
('S00001','Medan','Jl Semeru','123456','A00001','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('S00002','Palembang','Jl Gajah','123456','A00001','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('S00003','Jakarta','Jl Sudirman','123456','A00002','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('S00004','Surabaya','Jl Sudirman','123456','A00003','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('S00005','Bali','Jl Sudirman','123456','A00003','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');

CREATE TABLE [dbo].[MsPricingConfig](
	[ProductNo] [varchar](50) NOT NULL,
	[StoreNo] [varchar](50) NOT NULL,
	[AreaNo] [varchar](50) NOT NULL,
	[CustomerType] [varchar](10) NULL,
	[ValidFrom] [date] NULL,
	[ValidTo] [date] NULL,
	[Amount] DECIMAL(19,2) DEFAULT 0,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL
);
	
INSERT INTO [dbo].[MsPricingConfig] VALUES
('P00001','S00001','A00001','Retail','2023-01-01','9999-12-31',1200,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00002','S00002','A00001','Retail','2023-01-01','9999-12-31',75000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00003','S00003','A00002','Company','2023-01-01','9999-12-31',11000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00003','S00003','A00002','Retail','2023-01-01','9999-12-31',15000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00004','S00004','A00003','Retail','2023-01-01','9999-12-31',1700000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('P00005','S00005','A00003','Retail','2023-01-01','9999-12-31',1000000,'administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');

CREATE TABLE [dbo].[MsCustomer](
	[CustomerNo] [varchar](50) NOT NULL,
	[CustomerName] [varchar](50) NULL,
	[CustomerType] [varchar](10) NULL,
	[Address] [varchar](200) NULL,
	[Phone] [varchar](50) NULL,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
 CONSTRAINT [PK_MsCustomer] PRIMARY KEY CLUSTERED 
(
	[CustomerNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
	
INSERT INTO [dbo].[MsCustomer] VALUES
('C00001','Kawan Lama','Company','Jakarta Barat','0811111111111','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('C00002','John Doe','Mr','Jakarta Barat','082222222222','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('C00003','Maria','Mrs','Jakarta Barat','083333333333','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');

CREATE TABLE [dbo].[TrOrderHdr](
	[OrderNo] [varchar](50) NOT NULL,
	[OrderDate] [date] NULL,
	[StoreNo] [varchar](50) NULL,
	[AreaNo] [varchar](50) NULL,
	[SalesmanNo] [varchar](50) NULL,
	[CustomerNo] [varchar](50) NULL,
	[GrandTotal] DECIMAL(19,2) DEFAULT 0,
	[Description] [varchar](100) NULL,
	[CrtUsrID] [varchar](25) NOT NULL,
	[TsCrt] [datetime] NOT NULL,
	[ModUsrID] [varchar](25) NOT NULL,
	[TsMod] [datetime] NOT NULL,
	[ActiveFlag] [char](1) NOT NULL,
 CONSTRAINT [PK_TrOrderHdr] PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
	
INSERT INTO [dbo].[TrOrderHdr] VALUES
('ORD00001','2023-06-17','S00001','A00001','cashier1','C00002',6000,'','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('ORD00002','2023-06-18','S00002','A00001','cashier1','C00003',300000,'','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('ORD00003','2023-06-19','S00003','A00002','salesman1','C00001',1584000,'','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('ORD00004','2023-06-20','S00004','A00003','cashier1','C00002',1700000,'','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y'),
('ORD00005','2023-06-21','S00005','A00003','cashier1','C00003',1000000,'','administrator','2023-06-20 00:00:00.000','administrator','2023-06-20 00:00:00.000','Y');

CREATE TABLE [dbo].[TrOrderDtl](
	[OrderDtlID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NOT NULL,
	[ProductNo] [varchar](50) NULL,
	[Quantity] [int] DEFAULT 0,
	[Price] DECIMAL(19,2) DEFAULT 0,
	[Total] DECIMAL(19,2) DEFAULT 0,
	[Description] [varchar](100) NULL,
	[ActiveFlag] [char](1) NOT NULL,
CONSTRAINT [PK_TrOrderDtl] PRIMARY KEY CLUSTERED 
(
	[OrderDtlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
);
SET IDENTITY_INSERT [dbo].[TrOrderDtl]  OFF;

INSERT INTO [dbo].[TrOrderDtl] VALUES
('ORD00001','P00001',5,1200,6000,'','Y'),
('ORD00002','P00002',4,75000,300000,'','Y'),
('ORD00003','P00003',144,11000,1584000,'','Y'),
('ORD00004','P00004',1,1700000,1700000,'','Y'),
('ORD00005','P00005',1,1000000,1000000,'','Y');
