# Email Search #

This is just a simple project I started to try to create a full text index of archived emails. It is in the *very* early development stage.

The plan will be to scan a directory of email messages (created in MH format using readpst to convert Outlook PST files into text email files), import the messages as objects into a RavenDB database, and then use the Lucene text indexing RavenDB uses to have a fast search for emails.

That's the plan anyway :P

## Building: ##

Only tested building with Visual Studio 2010. Make sure you have the latest version of Nuget installed and be sure to follow these instructions to enable package restore:

http://blog.nuget.org/20120518/package-restore-and-consent.html

That's because the package dependencies (RavenDB et. al.) are not in this repository. The solution is setup to automatically fetch them using Nuget when you build.

## Copyright ##

Copyright (C) 2012 David Prothero

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
