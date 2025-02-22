﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SimpleIdServer.IdServer.Host.Acceptance.Tests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class BCCallbackErrorsFeature : object, Xunit.IClassFixture<BCCallbackErrorsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "BCCallbackErrors.feature"
#line hidden
        
        public BCCallbackErrorsFeature(BCCallbackErrorsFeature.FixtureData fixtureData, SimpleIdServer_IdServer_Host_Acceptance_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "BCCallbackErrors", "\tCheck errors returned by the /bc-callback endpoint\t", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="access token is required")]
        [Xunit.TraitAttribute("FeatureTitle", "BCCallbackErrors")]
        [Xunit.TraitAttribute("Description", "access token is required")]
        public void AccessTokenIsRequired()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token is required", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table88 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
#line 5
 testRunner.When("execute HTTP POST JSON request \'http://localhost/bc-callback\'", ((string)(null)), table88, "When ");
#line hidden
#line 8
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
 testRunner.Then("JSON \'error\'=\'access_denied\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 11
 testRunner.And("JSON \'error_description\'=\'missing token\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="access token must be valid")]
        [Xunit.TraitAttribute("FeatureTitle", "BCCallbackErrors")]
        [Xunit.TraitAttribute("Description", "access token must be valid")]
        public void AccessTokenMustBeValid()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("access token must be valid", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table89 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table89.AddRow(new string[] {
                            "user",
                            "user"});
#line 14
 testRunner.Given("build JWS by signing with a random RS256 algorithm and store the result into \'acc" +
                        "essToken\'", ((string)(null)), table89, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table90 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table90.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
#line 18
 testRunner.When("execute HTTP POST JSON request \'http://localhost/bc-callback\'", ((string)(null)), table90, "When ");
#line hidden
#line 22
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 24
 testRunner.Then("HTTP status code equals to \'401\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 25
 testRunner.And("JSON \'error\'=\'access_denied\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="authorization request must exists")]
        [Xunit.TraitAttribute("FeatureTitle", "BCCallbackErrors")]
        [Xunit.TraitAttribute("Description", "authorization request must exists")]
        public void AuthorizationRequestMustExists()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("authorization request must exists", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 27
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table91 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table91.AddRow(new string[] {
                            "sub",
                            "user"});
#line 28
 testRunner.Given("build access_token and sign with the key \'keyid\'", ((string)(null)), table91, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table92 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table92.AddRow(new string[] {
                            "Authorization",
                            "Bearer $access_token$"});
                table92.AddRow(new string[] {
                            "auth_req_id",
                            "invalid"});
#line 32
 testRunner.When("execute HTTP POST JSON request \'http://localhost/bc-callback\'", ((string)(null)), table92, "When ");
#line hidden
#line 37
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 39
 testRunner.Then("HTTP status code equals to \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 40
 testRunner.And("JSON \'error\'=\'invalid_request\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 41
 testRunner.And("JSON \'error_description\'=\'the back channel authorization invalid doesn\'t exist\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                BCCallbackErrorsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                BCCallbackErrorsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
