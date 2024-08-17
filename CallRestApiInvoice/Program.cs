﻿// See https://aka.ms/new-console-template for more information


using CallRestApiInvoice;
using System.Net;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

HttpClient http=new HttpClient();

try
{
    HttpResponseMessage response = null;

    Invoices invoices = new Invoices();

    invoices.Token = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI1Vy1ka2tQaDd4MGxEOVdqbTF5TXJKY0s4SldlSUtFQ1NiQ3k0SU9HSG1FIn0.eyJleHAiOjE3NDk1NDc2NDksImlhdCI6MTcxODAxMTY0OSwianRpIjoiNzlmZDIxZTctMDA1Zi00NzVhLTkxODYtYjllYTY0MjNjMjRjIiwiaXNzIjoiaHR0cHM6Ly9zc28udGlzc3RzcC5pci9yZWFsbXMvVFNQIiwiYXVkIjpbInRzcC1wb3J0YWwiLCJhY2NvdW50Il0sInN1YiI6IjgwNjE2YmYxLTcwZjItNDc5NS05ZjJhLTViNThhN2U5MGJhMSIsInR5cCI6IkJlYXJlciIsImF6cCI6ImFwaS1rZXkiLCJzZXNzaW9uX3N0YXRlIjoiY2U5MzFhOGEtOTMyNC00ZWJiLTkzYWEtNzc2YTRlZDQxNDY5IiwiYWNyIjoiMSIsImFsbG93ZWQtb3JpZ2lucyI6WyIqIl0sInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJvZmZsaW5lX2FjY2VzcyIsInVtYV9hdXRob3JpemF0aW9uIiwiZGVmYXVsdC1yb2xlcy10c3AiXX0sInJlc291cmNlX2FjY2VzcyI6eyJ0c3AtcG9ydGFsIjp7InJvbGVzIjpbIm93bmVyIl19LCJhY2NvdW50Ijp7InJvbGVzIjpbIm1hbmFnZS1hY2NvdW50IiwibWFuYWdlLWFjY291bnQtbGlua3MiLCJ2aWV3LXByb2ZpbGUiXX19LCJzY29wZSI6Im9wZW5pZCBvcmdhbml6YXRpb24gb2ZmbGluZV9hY2Nlc3MgcHJvZmlsZSIsInNpZCI6ImNlOTMxYThhLTkzMjQtNGViYi05M2FhLTc3NmE0ZWQ0MTQ2OSIsInByZWZlcnJlZF91c2VybmFtZSI6IjAwNTU2OTM4NDkiLCJmaXNjYWxJZCI6IkEyRDU4MiJ9.g9BUf75lSSgcEdhpnc0mQVSULNxCnwsOXxy572VT_gxyrCodd49XubYPklIQQMImWu1bbHEEQDXP_gjefsCxQxEjE8GZJbi7gJX1FVqCyLmNCgBIkbz1brRZ2XMce-2ndWQnxBKz3rTMHBe58uHTRoPyEZ2fZVAyePxW8N64FaLaOKEKsz_-_8tphaJxZLZ1GcwfbSurqh5TbhEyF-oNtzzvusZKII9cMGYIXVg49P2Wxohet5kQJZR43ZZSP44t6HKBGTU2RYPApdM3uRTuo6lhDHchi6W-nMWQpd1Kb-8iL_-8WfRi_aJ3SIBgOlxunOrsJnDkd2DL1Xn6zOVzGA";
    invoices.XOrgId = "10103858742";
   

    var result= await http.PostAsJsonAsync(requestUri: " http://localhost:8084/api/Invoices", invoices);

    while (result.StatusCode ==HttpStatusCode.OK)
    {
        Console.WriteLine("200 row affected");
        result = await http.PostAsJsonAsync(requestUri: " http://localhost:8084/api/Invoices", invoices);
    }
   
}
catch (Exception)
{
    System.Environment.Exit(1);
    throw;
}


Console.WriteLine("Hello, World!");