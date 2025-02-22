﻿using Fido2NetLib;
using Microsoft.Extensions.Options;
using Plugin.Firebase.CloudMessaging;
using SimpleIdServer.IdServer.U2FClient;
using SimpleIdServer.Mobile.DTOs;
using SimpleIdServer.Mobile.Models;
using SimpleIdServer.Mobile.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows.Input;

namespace SimpleIdServer.Mobile.ViewModels
{
    public class BaseQRCodeViewModel : INotifyPropertyChanged
    {
        private bool _isLoading = false;
        private readonly MobileOptions _options;

        public BaseQRCodeViewModel(IPromptService promptService, IOptions<MobileOptions> options)
        {
            PromptService = promptService;
            _options = options.Value;
            CloseCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
        }

        public IPromptService PromptService { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CloseCommand { get; private set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected async Task ScanQRCode(string qrCodeValue)
        {
            if (IsLoading) return;
            IsLoading = true;
            try
            {
                if (string.IsNullOrWhiteSpace(qrCodeValue)) return;
                var qrCodeResult = JsonSerializer.Deserialize<QRCodeResult>(qrCodeValue);
                if (qrCodeResult.Action == "register") await Register(qrCodeResult);
                else await Authenticate(qrCodeResult);
            }
            catch
            {
                IsLoading = false;
                await PromptService.ShowAlert("Error", "An error occured while trying to parse the QR Code");
            }
            finally
            {
                IsLoading = false;
            }

            #region Register

            async Task Register(QRCodeResult qrCodeResult)
            {
                var beginResult = await ReadRegisterQRCode(qrCodeResult);
                var attestationBuilder = new FIDOU2FAttestationBuilder();
                var enrollResponse = attestationBuilder.BuildEnrollResponse(new EnrollParameter
                {
                    Challenge = beginResult.CredentialCreateOptions.Challenge,
                    Rp = beginResult.CredentialCreateOptions.Rp.Id,
                    Origin = qrCodeResult.GetOrigin()
                });
                var endRegisterResult = await EndRegister(beginResult, enrollResponse);

                var credentialRecord = new CredentialRecord(enrollResponse.CredentialId, enrollResponse.AttestationCertificate.AttestationCertificate, enrollResponse.AttestationCertificate.PrivateKey, endRegisterResult.SignCount);
                await App.Database.AddCredentialRecord(credentialRecord);
                IsLoading = false;
                await PromptService.ShowAlert("Success", "Your mobile device has been enrolled");
                await Shell.Current.GoToAsync("..");
            }

            async Task<BeginU2FRegisterResult> ReadRegisterQRCode(QRCodeResult qrCodeResult)
            {
                var handler = new HttpClientHandler();
                if (_options.IgnoreHttps) handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                };
                using (var httpClient = new HttpClient(handler))
                {
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(SanitizeEndRegisterUrl(qrCodeResult.ReadQRCodeURL))
                    };
                    var httpResponse = await httpClient.SendAsync(requestMessage);
                    httpResponse.EnsureSuccessStatusCode();
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BeginU2FRegisterResult>(json);
                }
            }

            async Task<EndU2FRegisterResult> EndRegister(BeginU2FRegisterResult beginResult, EnrollResult enrollResponse)
            {
                var fcmToken = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
                var handler = new HttpClientHandler();
                if (_options.IgnoreHttps) handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                };
                using (var httpClient = new HttpClient(handler))
                {
                    var deviceInfo = DeviceInfo.Current;
                    var deviceData = new Dictionary<string, string>
                {
                    { "device_type", deviceInfo.Platform.ToString() },
                    { "model", deviceInfo.Model },
                    { "manufacturer", deviceInfo.Manufacturer },
                    { "name", deviceInfo.Name },
                    { "version", deviceInfo.VersionString },
                    { "push_token", fcmToken },
                    { "push_type", _options.PushType }
                };
                    var dic = new Dictionary<string, object>
                {
                    { "login", beginResult.Login },
                    { "session_id", beginResult.SessionId },
                    { "attestation", enrollResponse.Response },
                    { "device_data", deviceData }
                };
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(SanitizeEndRegisterUrl(beginResult.EndRegisterUrl)),
                        Content = new StringContent(JsonSerializer.Serialize(dic), Encoding.UTF8, "application/json")
                    };
                    var httpResponse = await httpClient.SendAsync(requestMessage);
                    httpResponse.EnsureSuccessStatusCode();
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<EndU2FRegisterResult>(json);
                }
            }

            #endregion

            #region Authenticate

            async Task Authenticate(QRCodeResult qrCodeResult)
            {
                var credentialRecords = await App.Database.GetCredentialRecord();
                var beginResult = await ReadAuthenticateQRCode(qrCodeResult);
                var attestationBuilder = new FIDOU2FAttestationBuilder();
                var allowCredentials = beginResult.Assertion.AllowCredentials;
                var selectedCredential = credentialRecords.FirstOrDefault(c => allowCredentials.Any(ac => ac.Id.SequenceEqual(c.IdPayload)));
                var authResponse = attestationBuilder.BuildAuthResponse(new AuthenticationParameter
                {
                    Challenge = beginResult.Assertion.Challenge,
                    Rp = beginResult.Assertion.RpId,
                    Certificate = new AttestationCertificateResult(selectedCredential.Certificate, selectedCredential.PrivateKey),
                    CredentialId = selectedCredential.IdPayload,
                    Signcount = selectedCredential.SigCount,
                    Origin = qrCodeResult.GetOrigin()
                });
                await EndAuthenticate(beginResult, authResponse);
                selectedCredential.SigCount++;
                await App.Database.UpdateCredentialRecord(selectedCredential);
                IsLoading = false;
                await PromptService.ShowAlert("Success", "You are authenticated");
                await Shell.Current.GoToAsync("..");
            }

            async Task<BeginU2FAuthenticateResult> ReadAuthenticateQRCode(QRCodeResult qrCodeResult)
            {
                var handler = new HttpClientHandler();
                if (_options.IgnoreHttps) handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                };
                using (var httpClient = new HttpClient(handler))
                {
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(SanitizeEndRegisterUrl(qrCodeResult.ReadQRCodeURL))
                    };
                    var httpResponse = await httpClient.SendAsync(requestMessage);
                    httpResponse.EnsureSuccessStatusCode();
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<BeginU2FAuthenticateResult>(json);
                }
            }

            async Task EndAuthenticate(BeginU2FAuthenticateResult beginAuthenticate, AuthenticatorAssertionRawResponse assertion)
            {
                var handler = new HttpClientHandler();
                if (_options.IgnoreHttps) handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                };
                using (var httpClient = new HttpClient(handler))
                {
                    var deviceInfo = DeviceInfo.Current;
                    var endLoginRequest = new EndU2FLoginRequest
                    {
                        Login = beginAuthenticate.Login,
                        Assertion = assertion,
                        SessionId = beginAuthenticate.SessionId
                    };
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(SanitizeEndRegisterUrl(beginAuthenticate.EndLoginUrl)),
                        Content = new StringContent(JsonSerializer.Serialize(endLoginRequest), Encoding.UTF8, "application/json")
                    };
                    var httpResponse = await httpClient.SendAsync(requestMessage);
                    httpResponse.EnsureSuccessStatusCode();
                }
            }

            #endregion

            string SanitizeEndRegisterUrl(string url) => _options.IsDev ? url.Replace("localhost", "192.168.50.250") : url;
        }
    }
}
