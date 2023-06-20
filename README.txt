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

1. Please check Architecture & ERD Pictures to get better understanding about the app

2. There are 2 connection strings to simulate different server, you need to configure it first
"DefaultConnection": "Server=.\\MSSQLSERVER02;Database=dbKCompany;UID=sa;PWD=P0w3rfull;TrustServerCertificate=True;",
"SSOConnection": "Server=.\\SQLEXPRESS;Database=dbAuth;Trusted_Connection=True;TrustServerCertificate=True"

3. After that, configure the database contents, execute the sql file = dbKCompany.sql

4. Default password for every role is = abc@123