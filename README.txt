*&---------------------------------------------------------------------*
*& Project K-Company Guidelines                                        *
*&---------------------------------------------------------------------*
*& Description      : Program For Sales                                *
*& Version No.      : 001                                              *
*& Author           : Alexander Gunawan                                *
*& Date             : 20.06.2023                                       *
*----------------------------------------------------------------------*
* REVISION LOG                                                         *
*----------------------------------------------------------------------*
*----------------------------------------------------------------------*

1. Please check Folder "README Files", to see Architecture & ERD Pictures to get better understanding about the app

2. There are 2 connection strings to simulate different server, you need to configure it first
"DefaultConnection": "Server=.\\MSSQLSERVER02;Database=dbKCompany;UID=sa;PWD=P0w3rfull;TrustServerCertificate=True;",
"SSOConnection": "Server=.\\SQLEXPRESS;Database=dbAuth;Trusted_Connection=True;TrustServerCertificate=True"

3. After that, configure the database contents, execute the sql file = dbKCompany.sql
-dbKCompany using DefaultConnection
-dbAuth using SSOConnection

4. Default password (encrypted) for every role is = abc@123

5. SOAP API Structure (Tested by Chrome Ext Wizdler)
{address}/api/v1/login
{address}/api/v1/product
{address}/api/v1/pricelist
{address}/api/v1/customer
{address}/api/v1/order