using DriverRegistration.Application.DTOs.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DriverRegistration.UnitTest.Mock
{
    public static class MockDriverPostRequest
    {
        public static DriverPostRequest Get(string firstName, string lastName)
        {
            return new DriverPostRequest()
            {
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
