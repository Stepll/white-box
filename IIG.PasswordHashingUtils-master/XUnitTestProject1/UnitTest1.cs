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
        public void InitEqualCheck()
        {
            string hashed_1 = PasswordHasher.GetHash(_password);
            PasswordHasher.Init("", 0);
            string hashed_2 = PasswordHasher.GetHash(_password);
            Assert.Equal(hashed_1, hashed_2);

        }

        [Fact]
        public void InitNotEqualCheck()
        {
            string hashed_1 = PasswordHasher.GetHash(_password);
            PasswordHasher.Init(_salt, _modAdler32);
            string hashed_2 = PasswordHasher.GetHash(_password);
            Assert.NotEqual(hashed_1, hashed_2);

        }

        [Fact]
        public void PasswordHasherSaltCheck()
        {
            string hashed_1 = PasswordHasher.GetHash(_password, _salt);
            string hashed_2 = PasswordHasher.GetHash(_password, _salt);
            Assert.Equal(hashed_1, hashed_2);
        }

        [Fact]
        public void PasswordHasherSaltNotEqualCheck()
        {
            string pass = "password";
            string hashed_1 = PasswordHasher.GetHash(pass, _salt);
            string hashed_2 = PasswordHasher.GetHash(pass, _salt2);
            Assert.NotEqual(hashed_1, hashed_2);
        }

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
