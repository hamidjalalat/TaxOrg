using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using ViewModels.Invoices;
using FluentResults;
using System.Net;

namespace Application.Common
{
    public abstract class BaseService
    {
        public BaseService(HttpClient http)
        {
            Http = http;
            RequestUri =$"{BaseUrl}/{GetApiUrl()}";
           
        }

        protected string? BaseUrl { get; set; }

        protected string RequestUri { get; set; }
    

        protected abstract string GetApiUrl();

        protected HttpClient Http { get; set; }

        protected virtual async Task<O> GetAsync<O>()
        {
            HttpResponseMessage response = null;

            try
            {
                response = await Http.GetAsync(requestUri: RequestUri);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        O result =
                            await response.Content.ReadFromJsonAsync<O>();


                        return result;
                    }

                    catch (NotSupportedException)
                    {
                        //insert log
                    }

                    catch (JsonException)
                    {
                        //insert log
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                //insert log
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }

        protected virtual async Task<O> GetAsyncById<O>(string id)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await Http.GetAsync(requestUri: RequestUri + $"/{id}");

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        O result = await response.Content.ReadFromJsonAsync<O>();

                        return result;
                    }

                    catch (System.NotSupportedException)
                    {
                        //inserlog
                    }

                    catch (System.Text.Json.JsonException)
                    {
                        //inserlog
                    }
                }
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                //inserlog
            }
            finally
            {
                response.Dispose();
            }

            return default;
        }

        protected virtual async Task<O> PostAsync<I, O>(I viewModel,string token,string xOrgId)
        {

            HttpResponseMessage response = null;

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Http.DefaultRequestHeaders.Add("X-OrgId", xOrgId);

            try
            {
                response = await Http.PostAsJsonAsync(requestUri: RequestUri, value: viewModel);
                
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        O  result = await response.Content.ReadFromJsonAsync<O>();

                        return result;
                    }
              
                    catch (NotSupportedException )
                    {
                       
                        //insert log When content type is not valid
                    }
                  
                    catch (JsonException )
                    {
                        //insert log Invalid JSON
                    }

                }
            }

            catch (System.Text.Json.JsonException ex)
            {
                response.StatusCode = HttpStatusCode.OK;
                //insert log bad request
            }
            
            catch (HttpRequestException ex)
            {
                response.StatusCode = HttpStatusCode.OK;
                //insert log bad request
            }

            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.OK;
                //insert log bad request
            }

            finally
            {
                response.Dispose();
            }

            return default;
        }
    }
}
