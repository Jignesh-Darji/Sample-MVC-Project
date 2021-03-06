USE [master]
GO
/****** Object:  Database [SampleMVCMasterDetails]    Script Date: 17-08-2018 19:56:14 ******/
CREATE DATABASE [SampleMVCMasterDetails]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SampleMVCMasterDetails', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MYSQLEXPRESS\MSSQL\DATA\SampleMVCMasterDetails.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SampleMVCMasterDetails_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MYSQLEXPRESS\MSSQL\DATA\SampleMVCMasterDetails_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SampleMVCMasterDetails] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SampleMVCMasterDetails].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SampleMVCMasterDetails] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET ARITHABORT OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET RECOVERY FULL 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET  MULTI_USER 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SampleMVCMasterDetails] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SampleMVCMasterDetails] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SampleMVCMasterDetails', N'ON'
GO
USE [SampleMVCMasterDetails]
GO
/****** Object:  StoredProcedure [dbo].[AddUpdateEmployee]    Script Date: 17-08-2018 19:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddUpdateEmployee] 
	@EmpId bigint,
	@Name varchar(6),
	@Designation varchar(50),
	@Email varchar(100),
	@Phone varchar(10),
	@Address varchar(100),
	@UserName varchar(6),
	@Password varchar(50),
	@Age int,
	@p_error int output
AS
BEGIN
DECLARE @l_EmpID		INT
	BEGIN TRY

    SET @p_Error=0
	IF EXISTS(select 1 from Employee WHERE EmpId=@EmpId)
	BEGIN
		BEGIN TRAN  
		
		Update Employee SET Name=@Name,Designation=@Designation,Email=@Email,Phone=@Phone,EmpAddress=@Address,UserName=@UserName,UPassword=@Password,Age=@Age,CreateDate= GETUTCDATE() WHERE EmpId=@EmpId		

		SELECT EmpId,Name,Designation,Email,Phone,EmpAddress,UserName,UPassword,Age FROM Employee WHERE EmpId=@EmpId

		COMMIT TRAN
	END
	ELSE
	BEGIN
		
	BEGIN TRAN  
      
	  INSERT INTO Employee(Name,Designation,Email,Phone,EmpAddress,UserName,UPassword,Age,CreateDate)					
	  SELECT @Name,@Designation,@Email,@Phone,@Address,@UserName,@Password,@Age, GETUTCDATE()

	  SET @l_EmpID=SCOPE_IDENTITY()
	  SELECT EmpId,Name,Designation,Email,Phone,EmpAddress,UserName,UPassword,Age FROM Employee WHERE EmpId=@l_EmpID

	COMMIT TRAN
	END
	
	RETURN @p_Error


  END TRY
  BEGIN CATCH

   --IF @@TRANCOUNT > 0
   --ROLLBACK TRANSACTION;
    
   --THROW ;
        SET @p_Error = 50008--Error while updating Employee
                
		ROLLBACK TRAN
	

  END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployeeById]    Script Date: 17-08-2018 19:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteEmployeeById]
	@EmpId bigint
AS
BEGIN

		SELECT EmpId,Name,Designation,Email,Phone,EmpAddress,UserName,UPassword,Age FROM Employee Where EmpId=@EmpId;
		Delete From Employee Where EmpId=@EmpId;

END

GO
/****** Object:  StoredProcedure [dbo].[GetAllEmployees]    Script Date: 17-08-2018 19:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllEmployees]
	
AS
BEGIN

		SELECT EmpId,Name,Designation,Email,Phone,EmpAddress,UserName,UPassword,Age FROM Employee 

END

GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeById]    Script Date: 17-08-2018 19:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeeById]
	@EmpId bigint
AS
BEGIN

		SELECT EmpId,Name,Designation,Email,Phone,EmpAddress,UserName,UPassword,Age FROM Employee Where EmpId=@EmpId

END

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 17-08-2018 19:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmpId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Designation] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](10) NOT NULL,
	[EmpAddress] [varchar](100) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UPassword] [varchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmpId], [Name], [Designation], [Email], [Phone], [EmpAddress], [UserName], [UPassword], [Age], [CreateDate]) VALUES (1, N'Employ', N'Manager', N'Employee2@gmail.com', N'9876543210', N'Ahmedabd  hhfgh', N'Employ', N'employee@123', 34, CAST(0x0000A93F00E94B23 AS DateTime))
SET IDENTITY_INSERT [dbo].[Employee] OFF
USE [master]
GO
ALTER DATABASE [SampleMVCMasterDetails] SET  READ_WRITE 
GO
