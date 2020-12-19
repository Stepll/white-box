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
            PasswordHasher.Init(_salt, _modAdler32);
            var expected = PasswordHasher.GetHash(_password, _salt, _modAdler32);
            Assert.Equal(PasswordHasher.GetHash(_password), expected);
        }
        
        [Fact]
        private void TestInit()
        {
            PasswordHasher.Init(_salt, _modAdler32);
            Assert.Equal(PasswordHasher.GetHash(_password), "39F5A9915E7D0BF4514A6B0E23EFCC730996979430205EF1B17869DCD65B4C8E");
            PasswordHasher.Init(_salt2, _modAdler32_2);
            Assert.Equal(PasswordHasher.GetHash(_password), "0250B9D09E6F22727941B405816DC43E7B0FD7743E6A412693EA44D56E7DC33C");
        }

        [Fact]
        public void TestGetHash_passwordEmpty()
        {

            Assert.Equal(PasswordHasher.GetHash("", null, 22), PasswordHasher.GetHash("", null, 22));

        }

        [Fact]
        public void TestGetHash_passwordNull()
        {
            Assert.Equal(PasswordHasher.GetHash(null, null, 22), PasswordHasher.GetHash(null, null, 22));

        }

        [Fact]
        public void TestInit_IfSalt_IsNull()
        {
            String a = PasswordHasher.GetHash(_password);
            PasswordHasher.Init(null, 0);
            Assert.Equal(PasswordHasher.GetHash(_password), a);

        }

        [Fact]
        public void TestInit_IfSalt_IsEmpty()
        {
            String a = PasswordHasher.GetHash(_password);
            PasswordHasher.Init("", 0);
            Assert.Equal(PasswordHasher.GetHash(_password), a);

        }

        [Fact]
        public void TestInit_IfAdlerMod32_0AndMoreThan0_NotEqual()
        {
            PasswordHasher.Init(_salt, 0);
            String a = PasswordHasher.GetHash(_password);
            PasswordHasher.Init(_salt, 22);
            Assert.NotEqual(PasswordHasher.GetHash(_password), a);

        }

    }
}
