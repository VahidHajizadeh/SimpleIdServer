﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.6.0.0
//      SpecFlow Generator Version:3.6.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SimpleIdServer.OpenBankingApi.Host.Acceptance.Tests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.6.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AccountAccessContentsFeature : object, Xunit.IClassFixture<AccountAccessContentsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "AccountAccessContents.feature"
#line hidden
        
        public AccountAccessContentsFeature(AccountAccessContentsFeature.FixtureData fixtureData, SimpleIdServer_OpenBankingApi_Host_Acceptance_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "AccountAccessContents", "\tCheck /account-requests endpoint", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Create Account Access Content")]
        [Xunit.TraitAttribute("FeatureTitle", "AccountAccessContents")]
        [Xunit.TraitAttribute("Description", "Create Account Access Content")]
        public virtual void CreateAccountAccessContent()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Account Access Content", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table1.AddRow(new string[] {
                            "token_endpoint_auth_method",
                            "tls_client_auth"});
                table1.AddRow(new string[] {
                            "response_types",
                            "[token]"});
                table1.AddRow(new string[] {
                            "grant_types",
                            "[client_credentials]"});
                table1.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table1.AddRow(new string[] {
                            "redirect_uris",
                            "[http://localhost:8080]"});
                table1.AddRow(new string[] {
                            "tls_client_auth_san_dns",
                            "mtlsClient"});
                table1.AddRow(new string[] {
                            "id_token_signed_response_alg",
                            "PS256"});
                table1.AddRow(new string[] {
                            "token_signed_response_alg",
                            "PS256"});
#line 5
 testRunner.When("execute HTTP POST JSON request \'https://localhost:8080/register\'", ((string)(null)), table1, "When ");
#line hidden
#line 16
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 17
 testRunner.And("extract parameter \'client_id\' from JSON body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table2.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table2.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table2.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
                table2.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
#line 19
 testRunner.And("execute HTTP POST request \'https://localhost:8080/mtls/token\'", ((string)(null)), table2, "And ");
#line hidden
#line 26
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
 testRunner.And("extract parameter \'access_token\' from JSON body into \'accessToken\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table3.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
                table3.AddRow(new string[] {
                            "x-fapi-interaction-id",
                            "guid"});
                table3.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table3.AddRow(new string[] {
                            "data",
                            "{ \"permissions\" : [ \"ReadAccountsBasic\", \"ReadAccountsDetail\" ] }"});
#line 29
 testRunner.And("execute HTTP POST JSON request \'https://localhost:8080/v3.1/account-access-consen" +
                        "ts\'", ((string)(null)), table3, "And ");
#line hidden
#line 36
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 37
 testRunner.And("extract HTTP headers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 39
 testRunner.Then("HTTP status code equals to \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 40
 testRunner.Then("JSON exists \'Data.ConsentId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 41
 testRunner.Then("JSON exists \'Links.Self\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 42
 testRunner.Then("JSON \'Data.Permissions[0]\'=\'ReadAccountsBasic\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 43
 testRunner.Then("JSON \'Data.Permissions[1]\'=\'ReadAccountsDetail\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 44
 testRunner.Then("JSON \'Data.Status\'=\'AwaitingAuthorisation\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 45
 testRunner.Then("JSON \'Meta.TotalPages\'=\'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 46
 testRunner.Then("HTTP Header \'x-fapi-interaction-id\'=\'guid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.6.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AccountAccessContentsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AccountAccessContentsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
