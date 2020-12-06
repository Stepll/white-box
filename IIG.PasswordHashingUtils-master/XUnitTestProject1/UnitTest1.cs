using System;
using Xunit;
using IIG.PasswordHashingUtils;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private static string _salt = "salt";
        private static string _salt2 = "salt2";
        private static string _password = "password";
        private static string _password1 = "password";
        private static string _password2 = "password2";
        private static uint _modAdler32 = 1;
        private static uint _modAdler32_2 = 2;
        [Fact]
        private void TestNotNull()
        {
            Assert.NotNull(PasswordHasher.GetHash(_password));
        }
        [Fact]
        private void TestGetHash1()
        {
            Assert.Equal(PasswordHasher.GetHash(_password1), "E38CC12C4C3BDEC1A9689D0B2198DE1B93BFDB01333CC4EA76000B4730B854EE");
        }
        [Fact]
        private void TestGetHash2()
        {
            Assert.Equal(PasswordHasher.GetHash(_password2), "4D958688FF13DCC70543CFC8F93768BDE6280EACC6C84495DB9E5316D3656F50");
        }
        [Fact]
        private void TestInit()
        {
            PasswordHasher.Init(_salt, _modAdler32);
            Assert.Equal(PasswordHasher.GetHash(_password), "39F5A9915E7D0BF4514A6B0E23EFCC730996979430205EF1B17869DCD65B4C8E");
            PasswordHasher.Init(_salt2, _modAdler32_2);
            Assert.Equal(PasswordHasher.GetHash(_password), "0250B9D09E6F22727941B405816DC43E7B0FD7743E6A412693EA44D56E7DC33C");
        }

    }
}
