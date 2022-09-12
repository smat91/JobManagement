using Castle.Core.Resource;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataAccessLayer.Helper
{
    public class CustomerXmlConverter
    {
        public List<Customer> XDocumentToCustomerList(XDocument xDocument)
        {
            List<Customer> customerList = new List<Customer>();

            foreach (XElement xElement in xDocument.Element("Kunden").Elements())
            {
                if (xElement != null)
                {
                    Customer customer = new Customer();
                    customer.Address = new Address();

                    customer.CustomerNumber = xElement.Attribute("CustomerNr").Value;
                    customer.Firstname = xElement.Element("Name").Value.Split(' ')[0];
                    customer.Lastname = xElement.Element("Name").Value.Split(' ')[1];
                    customer.Address.Street = xElement.Element("Address")
                        .Element("Street").Value.Split(' ')[0];
                    customer.Address.StreetNumber = xElement.Element("Address")
                        .Element("Street").Value.Split(' ')[1];
                    customer.Address.Zip = xElement.Element("Address")
                        .Element("PostalCode").Value;
                    customer.EMail = xElement.Element("EMail").Value;
                    customer.Website = xElement.Element("Website").Value;
                    customer.Password = xElement.Element("Password").Value;

                    customerList.Add(customer);
                }
            }
            return customerList;
        }

        public XDocument CustomerListToXDocument(List<Customer> customers) { 
            XDocument xml = new XDocument(new XElement("Kunden"));

            foreach (Customer customer in customers)
            {
                if (customer != null)
                {
                    xml.Element("Kunden").Add(CustomerToXElement(customer));
                }
            }
            return xml;
        }

        private XElement CustomerToXElement(Customer customer) {
            var xElement = XElementTemplateCustomer();

            xElement.SetAttributeValue("CustomerNr", customer.CustomerNumber);
            xElement.SetElementValue("Name", $"{customer.Firstname} {customer.Lastname}");
            xElement.Element("Address").
                SetElementValue("Street", $"{customer.Address.Street} {customer.Address.StreetNumber}");
            xElement.Element("Address").
                SetElementValue("PostalCode", customer.Address.Zip);
            xElement.SetElementValue("EMail", customer.EMail);
            xElement.SetElementValue("Website", customer.Website);
            xElement.SetElementValue("Password", customer.Password);
            return xElement;
        }

        private XElement XElementTemplateCustomer()
        {
            var customer = new XElement("Kunde",
                            new XElement("Name", ""),
                            new XElement("Address",
                                new XElement("Street", ""),
                                new XElement("PostalCode", "")
                                ),
                            new XElement("EMail", ""),
                            new XElement("Website", ""),
                            new XElement("Password", ""));
            customer.SetAttributeValue("CustomerNr", "");

            return customer;
        }
    }
}
