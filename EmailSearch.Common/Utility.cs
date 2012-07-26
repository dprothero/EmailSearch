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

namespace EmailSearch.Common
{
  public class Utility
  {
    public static int CountOfCharacter(string source, char characterToFind)
    {
      int count = 0;
      foreach (char c in source)
        if (c == characterToFind) count++;
      return (count);
    }
  }
}
