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
using System.Linq;
using System.Text;

namespace EmailSearch.Common
{
  public class Identity
  {
    public string Name { get; set; }
    public string EmailAddress { get; set; }

    public Identity(string headerValue)
    {
      Name = ParseName(headerValue);
      EmailAddress = ParseEmailAddress(headerValue);
    }

    private string ParseName(string headerValue)
    {
      if (Utility.CountOfCharacter(headerValue, '\"') == 2)
        return (headerValue.Substring(headerValue.IndexOf('\"') + 1, headerValue.LastIndexOf('\"') - (headerValue.IndexOf('\"') + 1)));

      if (Utility.CountOfCharacter(headerValue, '<') == 1 && Utility.CountOfCharacter(headerValue, '>') == 1)
        return (headerValue.Substring(0, headerValue.IndexOf('<')).Trim());

      return ("");
    }

    private string ParseEmailAddress(string headerValue)
    {
      if (Utility.CountOfCharacter(headerValue, '<') == 1 && Utility.CountOfCharacter(headerValue, '>') == 1)
        return (headerValue.Substring(headerValue.IndexOf('<') + 1, headerValue.IndexOf('>') - (headerValue.IndexOf('<') + 1)));

      if (headerValue.Contains("@"))
        return headerValue;

      return ("");
    }
  }
}
