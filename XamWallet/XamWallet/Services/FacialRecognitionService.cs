﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

using Xamarin.Forms;
using XamWallet.Helpers;
using XamWallet.ViewModels;

namespace FacialRecognitionLogin
{
    public static class FacialRecognitionService
    {
        const string _personGroupId = "persongroupid";
        const string _personGroupName = "Facial Recognition Login Group";
        readonly static Lazy<FaceClient> _faceApiClientHolder = new Lazy<FaceClient>(() =>
        new FaceClient(new ApiKeyServiceClientCredentials(AzureConstants.FacialRecognitionAPIKey))
        { Endpoint = AzureConstants.FaceApiBaseUrl });

        static int _networkIndicatorCount;

        static FaceClient FaceApiClient => _faceApiClientHolder.Value;

        public static async Task RemoveExistingFace(Guid userId)
        {
            UpdateActivityIndicatorStatus(true);

            try
            {
                await FaceApiClient.PersonGroupPerson.DeleteAsync(_personGroupId, userId).ConfigureAwait(false);
            }
            catch (APIErrorException e) when (e.Response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                Debug.WriteLine("Person Does Not Exist");
                DebugService.PrintException(e);
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        public static async Task<Guid> AddNewFace(string username, Stream photo)
        {
            UpdateActivityIndicatorStatus(true);

            try
            {
                await CreatePersonGroup().ConfigureAwait(false);

                var createPersonResult = await FaceApiClient.PersonGroupPerson.CreateAsync(_personGroupId, username).ConfigureAwait(false);

                var faceResult = await FaceApiClient.PersonGroupPerson.AddFaceFromStreamAsync(_personGroupId, createPersonResult.PersonId, photo).ConfigureAwait(false);

                var trainingStatus = await TrainPersonGroup(_personGroupId).ConfigureAwait(false);
                if (trainingStatus.Status is TrainingStatusType.Failed)
                    throw new Exception(trainingStatus.Message);

                return faceResult.PersistedFaceId;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        public static async Task<bool> IsFaceIdentified(string username, Stream photo)
        {
            UpdateActivityIndicatorStatus(true);

            try
            {
                var personGroupListTask = FaceApiClient.PersonGroupPerson.ListAsync(_personGroupId);

                var facesDetected = await FaceApiClient.Face.DetectWithStreamAsync(photo).ConfigureAwait(false);
                var faceDetectedIds = facesDetected.Select(x => x.FaceId ?? new Guid()).ToArray();

                var facesIdentified = await FaceApiClient.Face.IdentifyAsync(faceDetectedIds, _personGroupId).ConfigureAwait(false);

                var candidateList = facesIdentified.SelectMany(x => x.Candidates).ToList();

                var personGroupList = await personGroupListTask.ConfigureAwait(false);

                var matchingUsernamePersonList = personGroupList.Where(x => x.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase));

                return candidateList.Select(x => x.PersonId).Intersect(matchingUsernamePersonList.Select(y => y.PersonId)).Any();
            }
            catch
            {
                return false;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        static void UpdateActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
        {
            var viewModel = GetCurrentViewModel();

            if (isActivityIndicatorDisplayed)
            {
                viewModel.IsInternetConnectionActive = Application.Current.MainPage.IsBusy = true;
                _networkIndicatorCount++;
            }
            else if (--_networkIndicatorCount <= 0)
            {
                viewModel.IsInternetConnectionActive = Application.Current.MainPage.IsBusy = false;
                _networkIndicatorCount = 0;
            }
        }

        static async Task CreatePersonGroup()
        {
            try
            {
                await FaceApiClient.PersonGroup.CreateAsync(_personGroupId, _personGroupName).ConfigureAwait(false);
            }
            catch (APIErrorException e) when (e.Response.StatusCode is HttpStatusCode.Conflict)
            {
                Debug.WriteLine("Person Group Already Exists");
                DebugService.PrintException(e);
            }
        }

        static async Task<TrainingStatus> TrainPersonGroup(string personGroupId)
        {
            TrainingStatus trainingStatus;

            await FaceApiClient.PersonGroup.TrainAsync(personGroupId).ConfigureAwait(false);

            do
            {
                trainingStatus = await FaceApiClient.PersonGroup.GetTrainingStatusAsync(_personGroupId).ConfigureAwait(false);
            }
            while (!(trainingStatus.Status is TrainingStatusType.Failed || trainingStatus.Status is TrainingStatusType.Succeeded));

            return trainingStatus;
        }

        static BaseViewModel GetCurrentViewModel()
        {
            var currentPage = Application.Current.MainPage.Navigation.ModalStack.Any()
                                ? Application.Current.MainPage.Navigation.ModalStack.LastOrDefault()
                                : Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return (BaseViewModel)currentPage.BindingContext;
        }
    }
}
