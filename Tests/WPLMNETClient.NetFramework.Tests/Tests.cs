﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressLicenseManagerNETClient;
using WordPressLicenseManagerNETClient.Models;

namespace WPLMNETClient.NetFramework.Tests
{
    [TestClass]
    public class Tests
    {

        Configuration configuration = default(Configuration);
        ILicenseManager licenseManager = default(ILicenseManager);
        License license = default(License);
        [TestInitialize]
        public void OnStartUp()
        {

            configuration = new Configuration();
            configuration.PostURL = "https://bluebyte.biz";
            configuration.ActivationKey = "5dac72c4d41999.62508674";
            configuration.SecretKey = "5dac72c4d41910.86584044";

            licenseManager = LicenseManagerFactory.New(configuration);


            license = new License();
            license.Email = "amen@bluebyte.biz";
            license.Key = "5db3244aef396";
            license.FirstName = "Amen";
            license.LastName = "Jlili";
            license.CompanyName = "Blue Byte LLC";
            license.MaximumDomainAllowed = 1;
        }


        [TestMethod]
        public void ActivateLicenseKey()
        {
            var licenseResponse = licenseManager.PerformAction(WordPressLicenseManagerNETClient.Consts.Action.Activate, new License());
            if (licenseResponse.Success == false)
                throw new Exception(licenseResponse.Message);
            else
                Assert.IsTrue(licenseResponse.Success);
        }
        [TestMethod]
        public void DeactivateLicenseKey()
        {
            var licenseResponse = licenseManager.PerformAction(WordPressLicenseManagerNETClient.Consts.Action.Deactivate, license);
            if (licenseResponse.Success == false)
                throw new Exception(licenseResponse.Message);
            else
                Assert.IsTrue(licenseResponse.Success);
        }
        [TestMethod]
        public void CheckLicenseKey()
        {
            var licenseResponse = licenseManager.PerformAction(WordPressLicenseManagerNETClient.Consts.Action.Check, license);
            if (licenseResponse.Success == false)
                throw new Exception(licenseResponse.Message);
            else
                Assert.IsTrue(licenseResponse.Success, $"Key {licenseResponse.Key}: Domains: {string.Join(", ", licenseResponse.RegisteredDomains.Select(x=> x.Value).ToArray())}");
        }

        [TestMethod]
        public void CreateLicenseKey()
        {
            var licenseResponse = licenseManager.PerformAction(WordPressLicenseManagerNETClient.Consts.Action.Create, license);
            if (licenseResponse.Success == false)
                throw new Exception(licenseResponse.Message);
            else
                Assert.IsTrue(licenseResponse.Success);
        }
    }
}
