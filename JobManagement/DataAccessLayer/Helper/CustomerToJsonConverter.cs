using DataAccessLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessLayer.Helper
{
    public class CustomerToJsonConverter : JsonConverter<Customer>
    {
        public override Customer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            Customer customer = new Customer();
            customer.Address = new Address();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return customer;
                }

                // Get the key.
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string? propertyName = reader.GetString();

                if (propertyName == "customerNr")
                {
                    if (reader.Read())
                    {
                        var result = reader.GetString();
                        customer.CustomerNumber = result;
                    }
                }
                else if (propertyName == "name")
                {
                    if (reader.Read())
                    {
                        var result = reader.GetString();
                        customer.Firstname = result.Split(' ')[0];
                        customer.Lastname = result.Split(' ')[1];
                    }
                }
                else if (propertyName == "address")
                {
                    if (reader.Read()) {
                        if (reader.TokenType != JsonTokenType.StartObject)
                        {
                            throw new JsonException();
                        }

                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                            {
                                break;
                            }

                            // Get the key.
                            if (reader.TokenType != JsonTokenType.PropertyName)
                            {
                                throw new JsonException();
                            }

                            propertyName = reader.GetString();

                            if (propertyName == "street")
                            {
                                if (reader.Read())
                                {
                                    var result = reader.GetString();
                                    customer.Address.Street = result.Split(' ')[0];
                                    customer.Address.StreetNumber = result.Split(' ')[1];
                                }
                            }
                            else if (propertyName == "postalCode")
                            {
                                if (reader.Read())
                                {
                                    var result = reader.GetString();
                                    customer.Address.Zip = result;
                                }
                            }
                        }
                    }
                }
                else if (propertyName == "email")
                {
                    if (reader.Read())
                    {
                        var result = reader.GetString();
                        customer.EMail = result;
                    }
                }
                else if (propertyName == "website")
                {
                    if (reader.Read())
                    {
                        var result = reader.GetString();
                        customer.Website = result;
                    }
                }
                else if (propertyName == "password")
                {
                    if (reader.Read())
                    {
                        var result = reader.GetString();
                        customer.Password = result;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Customer customer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
                writer.WriteString("customerNr", customer.CustomerNumber);
                writer.WriteString("name", $"{customer.Firstname} {customer.Lastname}");
                writer.WritePropertyName("address");
                    writer.WriteStartObject();
                        writer.WriteString("street", $"{customer.Address.Street} {customer.Address.StreetNumber}");
                        writer.WriteString("postalCode", customer.Address.Zip);
                    writer.WriteEndObject();
                writer.WriteString("email", customer.EMail);
                writer.WriteString("website", customer.Website);
                writer.WriteString("password", customer.Password);
            writer.WriteEndObject();
        }
    }
}
