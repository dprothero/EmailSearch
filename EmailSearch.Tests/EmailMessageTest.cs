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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailSearch.Tests
{
    
    
    /// <summary>
    ///This is a test class for EmailMessageTest and is intended
    ///to contain all EmailMessageTest Unit Tests
    ///</summary>
  [TestClass()]
  public class EmailMessageTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    private const string testMessageBody = "Received: from [192.168.1.50] by 192.168.1.22 (AppleShare IP Mail Server 6.2) id 23807 via TCP with SMTP; Wed, 07 Mar 2001 16:53:42 -0800\nMessage-id: <1010307105342.1d42bfa.c0a80116.ASIP6.2.23807@192.168.1.22>\nX-Mailer: Microsoft Outlook Express for Macintosh - 4.01 (295) \nDate: Wed, 07 Feb 2001 16:53:46 -0800\nSubject: Domain name\nFrom: \"Joe Executive\" <JExec@company.com>\nTo: Mar Smith <MSmith@company.com>\n	, Kar <KWilson@company.com>\n	, David <DProthero@company.com>\nX-Priority: 3\nStatus: RO\nMIME-Version: 1.0\nContent-Type: multipart/mixed;\n	boundary=\"--boundary-LibPST-iamunique-1178027265_-_-\"\n\n\n----boundary-LibPST-iamunique-1178027265_-_-\nContent-Type: text/plain; charset=\"US-ASCII\"\n\nMar\nshouldn't we have the url\n\ncompany.com\n\nand put info about our organization, publications etc.  \n\nJoe\n\ncopy kar, dav\n\n----boundary-LibPST-iamunique-1178027265_-_---\n";
    
    /// <summary>
    ///A test for LoadFromFile
    ///</summary>
    [TestMethod()]
    public void LoadFromFileTest()
    {
      EmailMessage target = new EmailMessage();
      string fileName = CreateTestEmailFile();

      try
      {
        target.LoadFromFile(fileName);
        Assert.AreEqual(testMessageBody, target.RawMessage);
        Assert.AreEqual("Domain name", target.Subject);
        Assert.AreEqual("Joe Executive", target.From.Name);
        Assert.AreEqual("JExec@company.com", target.From.EmailAddress);
        Assert.AreEqual(3, target.Recipients.Count);
        Assert.AreEqual("Mar Smith", target.Recipients[0].Name);
        Assert.AreEqual("MSmith@company.com", target.Recipients[0].EmailAddress);
        Assert.AreEqual("Kar", target.Recipients[1].Name);
        Assert.AreEqual("KWilson@company.com", target.Recipients[1].EmailAddress);
        Assert.AreEqual("David", target.Recipients[2].Name);
        Assert.AreEqual("DProthero@company.com", target.Recipients[2].EmailAddress);
        Assert.AreEqual(2001, target.Date.Year);
        Assert.AreEqual(2, target.Date.Month);
        Assert.AreEqual(8, target.Date.Day);
        Assert.AreEqual(0, target.Date.Hour);
        Assert.AreEqual(53, target.Date.Minute);
        Assert.AreEqual(46, target.Date.Second);
      }
      finally
      {
        File.Delete(fileName);
      }
    }

    /// <summary>
    ///A test for LoadFromFile - alternate date format
    ///</summary>
    [TestMethod()]
    public void AlternateDateTest()
    {
      EmailMessage target = new EmailMessage();
      string fileName = CreateTestEmailFile(testMessageBody.Replace("\nDate: Wed, 07 Feb 2001 16:53:46 -0800\n", "\nDate: 28 Mar 2002 23:52:14 -0000\n"));

      try
      {
        target.LoadFromFile(fileName);
        Assert.AreEqual(2002, target.Date.Year);
        Assert.AreEqual(3, target.Date.Month);
        Assert.AreEqual(28, target.Date.Day);
        Assert.AreEqual(23, target.Date.Hour);
        Assert.AreEqual(52, target.Date.Minute);
        Assert.AreEqual(14, target.Date.Second);
      }
      finally
      {
        File.Delete(fileName);
      }
    }

    private string CreateTestEmailFile(string messageBody = null)
    {
      string tempFileName = Path.GetTempFileName();
      File.WriteAllText(tempFileName, messageBody == null ? testMessageBody : messageBody);
      return (tempFileName);
    }

    /// <summary>
    ///A test for IdFromFileName
    ///</summary>
    [TestMethod()]
    public void IdFromFileNameTest()
    {
      string fileName = "C:\\path\\to\\file\\filename.ext";
      string expected = "C$/path/to/file/filename.ext";
      string actual;
      actual = EmailMessage.IdFromFileName(fileName);
      Assert.AreEqual(expected, actual);
    }
  }
}
