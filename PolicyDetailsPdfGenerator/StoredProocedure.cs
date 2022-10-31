namespace PolicyDetailsPdfGenerator
{
    public class StoredProocedure
	{
//        -- ================================================
//-- Template generated from Template Explorer using:
//-- Create Procedure(New Menu).SQL
//--
//-- Use the Specify Values for Template Parameters
//-- command(Ctrl-Shift-M) to fill in the parameter
//-- values below.
//--
//-- This block of comments will not be included in
//-- the definition of the procedure.
//-- ================================================
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//-- =============================================
//-- Author:		<Danish>
//-- Create date: <27-10-2022>
//-- Description:	<Task Given by Suyash>
//-- =============================================
//CREATE OR ALTER PROCEDURE dbo.spFilledTemplate_GetInformation
//	@PolicyNumber INT
//	-- Add the parameters for the stored procedure here
//AS
//BEGIN

//	SELECT UpdatePersons.Name AS 'personname' ,
//			UpdatePersons.Age AS 'personage',
//			UpdatePersons.Salary AS 'personsalary',
//			UpdatePersons.Occupation AS 'personoccupation',
//			UpdatePersons.ProductCode AS 'personproductcode',
//			UpdatePersons.PolicyNumber as 'Personpolicynumber',
//			UpdatePersons.PolicyExpiryDate as 'personpolicyexpirydate',
//			UpdatePersons.Tenure as 'persontenure' ,
//			UpdatePersons.Email as 'personemail',
//			HtmlTemplates.HtmlTemplateContent as 'htmltemplate'
//	INTO #temp from PolicyPdfGenerator.dbo.UpdatePersons 
//	JOIN HtmlTemplates ON UpdatePersons.Id = HtmlTemplates.Id


//	where PolicyNumber = @PolicyNumber

//	--Select personname, personage, personsalary, personoccupation, personproductcode, Personpolicynumber, personpolicyexpirydate, persontenure, personemail, htmltemplate,
//	--	REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(htmltemplate,
//	--	'{{Name}}', personname),
//	--	'{{PolicyNumber}}',Personpolicynumber),
//	--	'{{PersonAge}}',personage),
//	--	'{{PersonSalary}}',personsalary),
//	--	'{{PersonOccupation}}',personoccupation),
//	--	'{{PersonProductCode}}',personproductcode),
//	--	'{{PersonTenure}}',persontenure),
//	--	'{{PersonAPolicyExpiryDate}}',personpolicyexpirydate),
//	--	'{{PersonEmail}}',personemail) as replacedhtml	
//	--from #temp 

//	DECLARE @dummyDataset
//	TABLE(
//		[PolicyNumber][varchar] (50) NOT NULL,
//		[TransactionCode] [varchar] (8) NULL,
//		[TransactionNumber][int] NULL,
//		[TemplateCode][varchar] (50) NOT NULL,
//		[ChannelCode] [varchar] (50) NOT NULL,
//		[ClientId] [varchar] (100) NOT NULL,
//		[Destination] [varchar] (100) NOT NULL,
//		[Subject] [nvarchar] (500) NULL,
//		[Body][nvarchar] (max) NOT NULL,
//		[CreatedUserId][varchar] (50) NOT NULL
//		)

//	SELECT* from #temp

//	INSERT INTO @dummyDataset
//           --([PolicyNumber]
//           --,[TransactionCode]
//           --,[TransactionNumber]
//           --,[TemplateCode]
//           --,[ChannelCode]
//           --,[ClientId]
//           --,[Destination]
//           --,[Subject]
//           --,[Body]
//           --,[CreatedUserId])
	
//	SELECT
//		fi.Personpolicynumber,
//		null,
//		1,
//		'DS1',
//		'email',
//		'Mobile',
//		fi.personemail,
//		'Your Policy info  is saved ',
//		REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(fi.htmltemplate,'{{Name}}', fi.personname),'{{PolicyNumber}}',fi.Personpolicynumber),'{{PersonAge}}',fi.personage),'{{PersonSalary}}',fi.personsalary),'{{PersonOccupation}}',fi.personoccupation),'{{PersonProductCode}}',fi.personproductcode),'{{PersonTenure}}',fi.persontenure),'{{PersonAPolicyExpiryDate}}',fi.personpolicyexpirydate),'{{PersonEmail}}',fi.personemail) AS 'Body',
//		'Suyash' 
//	FROM #temp fi
//	SELECT * FROM @dummyDataset


//	INSERT INTO dbo.DataSet
//	SELECT* FROM @dummyDataset;

//	DROP Table #temp
//END
//GO


	}
}
