## Persistance Layer

### ORM
Entity framework was used as the Object Relational Mapper framework.  It has the benefit of working well with the chosen data store.  The entity context was generated manually.  In larger schemas it is recomended to use a helper utility to manage schema changes via a [Code-First](https://msdn.microsoft.com/en-us/library/jj200620(v=vs.113).aspx) approach or a [Database-First](https://msdn.microsoft.com/en-us/library/jj206878(v=vs.113).aspx) approach.

### Data Store Selection
SQL Server was chosen as the data store.  It is recomended for relational data that is normalized.  If your data is non-relational or the schema is highly variable there are Document databases or no-SQL alternatives such as Mongo, Cosmo, CouchDB, or simple Blob or table/Key Value Pair storage techniques.  Each approach is appropriate for different scenarios, depending on which features are critical (scale, speed, ACID adherance, etc.)

Azure SQL Server is used to persist items in the reference Application.  The following basic schema is being used:

Field       | Data Type         | Notes
--------    | -----------       | ---------
Id          | nvarchar (128)    | EF Generated GUID
Text        | nvarchar (MAX)    | 
Description | nvarchar (MAX)    |

or, as a SQL Script:
~~~~
CREATE TABLE [dbo].[Items]
([Id]          [NVARCHAR](128) NOT NULL,
 [Text]        [NVARCHAR](MAX) NULL,
 [Description] [NVARCHAR](MAX) NULL,
 CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED([Id] ASC)
 WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
~~~~