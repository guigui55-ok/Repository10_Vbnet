Oracle.ManagedDataAccess.Core NuGet Package 2.19.280 README
===========================================================
Release Notes: Oracle Data Provider for .NET Core

June 2025

This provider supports .NET 8 and 9.

This document provides information that supplements the Oracle Data Provider for .NET (ODP.NET) documentation.

Bug Fixes since Oracle.ManagedDataAccess.Core NuGet Package 2.19.270
====================================================================
Bug 37915391 - POOR DATA RETRIEVAL PERFORMANCE FOR HIGH PRECISION NUMBERS
Bug 37873494 - OPENWITHNEWPASSWORD FOR SYS USER AS SYSDBA GETS ORA-28009 
Bug 37834607 - CONNECTIVITY ISSUES ARE ENCOUNTERED WHEN SOURCE_ROUTE IS USED AND HOSTNAME RESOLVES TO AN INVALID IP ADDRESS
Bug 37574769 - PERFORMANCE DEGRADATION; ENCOUNTERS ORA-50000: CONNECTION REQUEST TIMED OUT

Known Issues and Limitations
============================
1) BindToDirectory throws NullReferenceException on Linux when LdapConnection AuthType is Anonymous

https://github.com/dotnet/runtime/issues/61683

This issue is observed when using System.DirectoryServices.Protocols, version 6.0.0.
To workaround the issue, use System.DirectoryServices.Protocols, version 5.0.1.

 Copyright (c) 2021, 2025, Oracle and/or its affiliates. 
