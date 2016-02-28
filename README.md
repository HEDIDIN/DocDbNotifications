# DocDbNotifications
DocumentDB Notifications Tutorial

# Notifications for new or modified DocumentDB Resources

This article came about from a question I saw posted.  The question was “Does DocumentDB support notifications for modified resources?” 

I have worked with BizTalk Server for many years, and this is a very common scenario when using the [WCF LOB Adapter](https://msdn.microsoft.com/en-us/library/bb798128.aspx) .  So I decided to see if I could duplicate this functionality in DocumentDB for new and/ or modified documents. 

## You start off with a Use Cases.  
The following story is the Use Case for this article.

> DocumentDB is the repository for HL7 FHIR documents.  Let’s assume that your DocumentDB Database combined with API and Logic App make up an HL7 FHIR Server.  A Healthcare facility is storing Patient data in the DocumentDB ‘Patients’ database. 
There are several Collections within the Patient database; Clinical, Identification, etc.. Patient information falls under Identification.  You have a collection named “Patient”.

> The Cardiology department is tracking personal heath and exercise data.  Searching for new or modified Patient records is time consuming.  They asked the IT department if there was a way that they could receive a notification for new or modified Patient records.  

> The IT department said that they could easily provide this.
