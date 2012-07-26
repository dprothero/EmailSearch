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
using System.Collections.Generic;
using System.IO;

namespace EmailSearch.Common
{
  public class EmailMessage
  {
    private List<Identity> _Recipients;
    
    public string Id { get; set; }
    public string RawMessage { get; set; }
    public string Subject { get; set; }
    public Identity From { get; set; }
    public List<Identity> Recipients { get { return (_Recipients); } }
    public DateTime Date { get; set; }

    public void LoadFromFile(string fileName)
    {
      Id = IdFromFileName(fileName);
      RawMessage = File.ReadAllText(fileName);
      Subject = ParseSubject();
      From = ParseFrom();
      _Recipients = ParseRecipients();
      Date = ParseDate();
    }

    public static string IdFromFileName(string fileName)
    {
      return (fileName.Replace(":\\", "$/").Replace("\\", "/"));
    }

    private string ParseSubject()
    {
      return (ParseHeaderItem("\nSubject:"));
    }

    private Identity ParseFrom()
    {
      string emailFrom = ParseHeaderItem("\nFrom:");
      return (new Identity(emailFrom));
    }

    private List<Identity> ParseRecipients()
    {
      var newList = new List<Identity>();
      string header = ParseHeaderItem("\nTo:");
      foreach (string recipient in header.Split(','))
        newList.Add(new Identity(recipient.Trim()));
      return (newList);
    }

    private DateTime ParseDate()
    {
      var date = ParseHeaderItem("\nDate:");
      return (DateTime.ParseExact(date, "ddd, dd MMM yyyy hh:mm:ss zzzz", System.Globalization.DateTimeFormatInfo.InvariantInfo));
    }

    private string ParseHeaderItem(string header)
    {
      int positionOfHeader = RawMessage.IndexOf(header);
      string headerText = "";
      if (positionOfHeader > -1)
      {
        headerText = RawMessage.Substring(positionOfHeader + header.Length, RawMessage.IndexOf("\n", positionOfHeader + header.Length) - (positionOfHeader + header.Length)).Trim();
        string remainingText = RawMessage.Substring(RawMessage.IndexOf("\n", positionOfHeader + header.Length) + 1);
        foreach (string s in remainingText.Split('\n'))
          if (s.StartsWith("\t"))
            headerText += "\n" + s;
          else
            break;
      }
      return headerText;
    }
  }
}
