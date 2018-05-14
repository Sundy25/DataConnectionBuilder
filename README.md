Data Connection Builder is a free and open-source Windows application that provides a convenient way to build connection strings for [OLE DB data providers used with ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/data-providers#net-framework-data-provider-for-ole-db).  Such connection strings are often embedded in configuration files for .NET applications that connect to databases on the back end.  There may also be occasions when end users need to build connection strings themselves for various reasons.  Data Connection Builder is essentially a dialog box that prompts the user for the information necessary to establish a connection to an OLE DB data source and provides the user with an appropriate connection string.

![screenshot](screenshot.png)

Like all of my .NET apps, Data Connection Builder requires the [JBCore library](https://github.com/jeffbourdier/JBCore), also hosted here on GitHub.
