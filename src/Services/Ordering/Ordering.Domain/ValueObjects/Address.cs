using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? EmailAddres { get; set; } = default!;
        public string Addressline { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;

        protected Address() { }
        private  Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddres = emailAddress;
            Addressline = addressLine;
            Country = country;
            State = state;
            ZipCode = zipcode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipcode)
        {
            ArgumentException.ThrowIfNullOrEmpty(emailAddress);
            ArgumentException.ThrowIfNullOrEmpty(addressLine);

            return new(firstName, lastName, emailAddress, addressLine, country, state, zipcode);
        }
    }
}
