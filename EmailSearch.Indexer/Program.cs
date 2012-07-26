/*
    EmailSearch - Full text index for email files.
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
 
 */

using System;
using System.IO;
using EmailSearch.Common;
using Raven.Client.Embedded;

namespace EmailSearch.Indexer
{
  class Program
  {
    static void Main(string[] args)
    {
      var documentStore = new EmbeddableDocumentStore { ConnectionStringName = "Local" };
      documentStore.Initialize();

      foreach (string fileName in Directory.GetFiles("D:\\UserData\\Mboxes\\Archive1999-2002\\Deleted Items", "*.*", SearchOption.AllDirectories))
      {
        var message = new EmailMessage();
        message.LoadFromFile(fileName);

        using (var session = documentStore.OpenSession())
        {
          if (session.Load<EmailMessage>(message.Id) == null)
          {
            session.Store(message);
            session.SaveChanges();
            Console.WriteLine("Stored: " + message.Id);
          }
        }
      }

      Console.Write("Press any key.");
      Console.ReadKey();
    }
  }
}
