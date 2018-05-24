using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace hw_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathXML = @"..\..\Files\contacts.xml";
            string pathPhones= @"..\..\Files\phones.txt";
            string pathEmails = @"..\..\Files\emails.txt";
            var documentXML = LoadXMLDocument(pathXML);
            Console.WriteLine("Исходный XML документ {0}:", pathXML);
            if (documentXML != null)
            {
                Console.WriteLine(documentXML);
                Console.WriteLine("Выгрузка списка телефонов в файл {0}", pathPhones);
                if (WriteList(GetListOfPhones(documentXML), pathPhones))
                    Console.WriteLine("Выгрузка завершена");
                else
                    Console.WriteLine("При выгрузке в файл произошла ошибка");
                Console.WriteLine("Выгрузка списка почтовых адресов в файл {0}", pathEmails);
                if (WriteList(GetListOfEmails(documentXML), pathEmails))
                    Console.WriteLine("Выгрузка завершена");
                else
                    Console.WriteLine("При выгрузке в файл произошла ошибка");
            }
            else
                Console.WriteLine("При чтении файла произошла ошибка или он пустой");
        }

        static XDocument LoadXMLDocument(string fileName)
        {
            XDocument document = null;
            using (var resource = new StreamReader(fileName))
            {
                document = XDocument.Load(resource);
            }
            return document;
        }

        static List<string> GetListOfPhones(XDocument document)
        {
            var phones = new List<string>();
            if(document!=null)
            {
                List<XElement> xcontacts = document.Root.Elements("Contact").ToList();
                List<XElement> xphones = xcontacts.FindAll(element => element.Attribute("Type").Value == "Phone");
                foreach (var phone in xphones)
                    phones.Add(phone.Attribute("Name").Value.ToString() + " " + phone.Value.ToString());
            }
            return phones;
        }

        static List<string> GetListOfEmails(XDocument document)
        {
            var emails = new List<string>();
            if (document != null)
            {
                List<XElement> xcontacts = document.Root.Elements("Contact").ToList();
                List<XElement> xemails = xcontacts.FindAll(element => element.Attribute("Type").Value == "Email");
                foreach (var email in xemails)
                    emails.Add(email.Attribute("Name").Value.ToString() + " " + email.Value.ToString());
            }
            return emails;
        }

        static bool WriteList(List<string> records, string fileName)
        {
            bool result = false;
            using (var stream = new StreamWriter(fileName))
            {
                foreach (var record in records)
                    stream.WriteLine(record.ToString());
                result = true;
            }
            return result;
        }
    }
}
